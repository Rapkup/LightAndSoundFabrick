using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs.ProductItemDtos;
using SemataryFabrick.Application.Entities.DTOs;
using System.Text.Json;
using SemataryFabrick.Application.Entities.Exceptions;
using SemataryFabrick.Application.Entities.DTOs.CartDtos;

namespace SemataryFabrikUI.Pages;

public class IndividualOrderConfiguratorModel : PageModel
{
    private readonly IProductCategoryService _productCategoryService;
    private readonly ISubCategoryService _subCategoryService;
    private readonly IItemService _itemService;
    private readonly ICartService _cartService;

    public ConfiguratorState State { get; set; } = new();

    public IEnumerable<ProductCategoryDto> Categories { get; private set; }
    public IEnumerable<SubCategoryDto> SubCategories { get; private set; }
    public IEnumerable<ItemDto> Items { get; private set; }

    public Dictionary<Guid, string> CategoryNames { get; set; } = new();
    public Dictionary<Guid, Guid> SubCategoryToCategoryMap { get; set; } = new();

    public decimal CartTotal => CalculateCartTotal();

    public IndividualOrderConfiguratorModel(
        IProductCategoryService productCategoryService,
        ISubCategoryService subCategoryService,
        IItemService itemService,
        ICartService cartService)
    {
        _productCategoryService = productCategoryService;
        _subCategoryService = subCategoryService;
        _itemService = itemService;
        _cartService = cartService;
    }

    public async Task<IActionResult> OnGet()
    {
        await InitializeState();
        await LoadDataBasedOnStep();
        await LoadCategoryRelations();

        /* if (State.CartItems.Any())
             await LoadItemsForCart();*/

        //Тут подгружаются товары по не существующим cartItemId

        return Page();
    }


    private async Task LoadCategoryRelations()
    {
        var allSubCategories = await _subCategoryService.GetAllSubCategoriesAsync();
        var allCategories = await _productCategoryService.GetAllProductCategoriesAsync();

        foreach (var sub in allSubCategories)
        {
            SubCategoryToCategoryMap[sub.Id] = sub.ParentCategoryId;
            var category = allCategories.FirstOrDefault(c => c.Id == sub.ParentCategoryId);
            if (category != null)
            {
                CategoryNames[sub.ParentCategoryId] = category.Name;
            }
        }
    }

    public async Task<IActionResult> OnPostSelectCategories(List<Guid> selectedCategoryIds)
    {
        await InitializeState();
        State.SelectedCategoryIds = selectedCategoryIds;

        if (State.SelectedCategoryIds.Count == 0)
        {
            ModelState.AddModelError("", "Выберите минимум одну категорию");
            await LoadCategories();
            return Page();
        }

        State.CurrentStep = 2;
        SaveState();
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostBackToStep(int targetStep)
    {
        await InitializeState();

        if (targetStep < 2) State.SelectedSubCategoryIds.Clear();
        if (targetStep < 3) State.EventDate = default;

        State.CurrentStep = targetStep;
        SaveState();
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostSelectSubcategories(List<Guid> selectedSubCategoryIds)
    {
        await InitializeState();
        State.SelectedSubCategoryIds = selectedSubCategoryIds;

        if (State.SelectedSubCategoryIds.Count == 0)
        {
            ModelState.AddModelError("", "Выберите минимум одну подкатегорию");
            await LoadSubCategories();
            return Page();
        }

        State.CurrentStep = 3;
        SaveState();
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostSetDate(DateOnly eventDate)
    {
        await InitializeState();
        State.EventDate = eventDate;

        if (State.EventDate < DateOnly.FromDateTime(DateTime.Today) && State.EventDate != default)
        {
            ModelState.AddModelError("State.EventDate", "Некорректная дата");
            await LoadDataBasedOnStep();
            return Page();
        }

        State.CurrentStep = 4;
        SaveState();
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostAddItem(Guid itemId, int quantity)
    {
        await InitializeState();

        if (quantity > 0)
            State.CartItems[itemId] = quantity;

        SaveState();
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostRemoveItem(Guid itemId)
    {
        await InitializeState();
        State.CartItems.Remove(itemId);
        SaveState();
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostComplete()
    {
        await InitializeState();

        if (HttpContext.Session.GetString("IsLoggedIn") != "true")
        {
            TempData["ShowAuthModal"] = true;
            SaveState();
            return RedirectToPage();
        }

        var userId = Guid.Parse(HttpContext.Session.GetString("UserId"));

        try
        {
            var cart = await _cartService.GetOrCreateCartAsync(userId, State.EventDate);
            var cartItems = State.CartItems.Select(x => new CartItemDto
            {
                ProductId = x.Key,
                Quantity = x.Value
            });

            await _cartService.AddCartItemsAsync(cart.Id, cartItems);

            HttpContext.Session.Remove("ConfiguratorState");
            return RedirectToPage("/Cart");
        }
        catch (EntityNotFoundException ex)
        {
            ModelState.AddModelError("", $"Ошибка: {ex.Message}");
            return Page();
        }
    }

    private async Task InitializeState()
    {
        var stateJson = HttpContext.Session.GetString("ConfiguratorState");
        if (stateJson != null)
            State = JsonSerializer.Deserialize<ConfiguratorState>(stateJson);
    }

    private void SaveState() =>
        HttpContext.Session.SetString("ConfiguratorState", JsonSerializer.Serialize(State));

    private async Task LoadDataBasedOnStep()
    {
        await LoadCategories();

        if (State.CurrentStep >= 2)
            await LoadSubCategories();

        if (State.CurrentStep >= 3)
            await LoadItems();
    }

    private async Task LoadCategories() =>
        Categories = await _productCategoryService.GetAllProductCategoriesAsync() ?? new List<ProductCategoryDto>();

    private async Task LoadSubCategories()
    {
        if (State.CurrentStep == 2)
        {
            if (State.SelectedCategoryIds?.Count > 0)
                SubCategories = await _subCategoryService.GetSubCategoriesByParentIdsAsync(State.SelectedCategoryIds);
        }
        else if (State.CurrentStep >= 4)
        {
            if (State.SelectedSubCategoryIds?.Count > 0)
                SubCategories = await _subCategoryService.GetSubCategoriesByIdsAsync(State.SelectedSubCategoryIds);
        }
    }
    private async Task LoadItems()
    {
        if (State.SelectedSubCategoryIds?.Count > 0)
            Items = await _itemService.GetItemsBySubCategoriesAsync(State.SelectedSubCategoryIds);
    }

    public async Task<IActionResult> OnPostResetCart()
    {
        HttpContext.Session.Remove("ConfiguratorState");
        return RedirectToPage();
    }

    private decimal CalculateCartTotal()
    {
        decimal total = 0;
        foreach (var (itemId, qty) in State.CartItems)
        {
            var item = Items?.FirstOrDefault(i => i.Id == itemId);
            if (item != null) total += item.Price * qty;
        }
        return total;
    }

    public Dictionary<string, string> ParseItemParameters(ItemDto item)
    {
        try
        {
            return JsonSerializer.Deserialize<Dictionary<string, string>>(item.Description)
                   ?? new Dictionary<string, string>();
        }
        catch
        {
            return new Dictionary<string, string>();
        }
    }

    private async Task LoadItemsForCart()
    {
        var cartItemIds = State.CartItems.Keys;
        Items = await _itemService.GetItemsByCartItemIds(cartItemIds.ToList());
    }
}



public class ConfiguratorState
{
    public int CurrentStep { get; set; } = 1;
    public List<Guid> SelectedCategoryIds { get; set; } = new();
    public List<Guid> SelectedSubCategoryIds { get; set; } = new();
    public DateOnly EventDate { get; set; }
    public Dictionary<Guid, int> CartItems { get; set; } = new();
    public Dictionary<Guid, ItemDto> LoadedItems { get; set; } = new();
}