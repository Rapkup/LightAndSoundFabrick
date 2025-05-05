using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.CartModels;
using SemataryFabrick.Domain.Entities.Models.Items;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using SemataryFabrick.Domain.Entities.Models;
using SemataryFabrick.Infrastructure.Implementations.Contexts;
using Microsoft.EntityFrameworkCore;

namespace SemataryFabrick.Infrastructure.Extensions;

public class DataSeederExtension(ApplicationContext _context)
{
    public async Task SeedDataAsync()
    {
        //await ClearExistingData();
        await SeedUsers();
        await SeedDiscounts();
        await SeedCategoriesAndSubcategories();
        await SeedInventoryAndItems();
        await SeedOrdersWithRelatedData();
        // await SeedCarts(); // Раскомментировать при необходимости
    }

    //private async Task ClearExistingData()
    //{
    //    // Порядок удаления важен из-за ограничений внешних ключей
    //    _context.WorkTaskAssignments.RemoveRange(_context.WorkTaskAssignments);
    //    _context.OrderCrewWorker.RemoveRange(_context.OrderCrewWorker);
    //    _context.OrderCrews.RemoveRange(_context.OrderCrews);
    //    _context.WorkTasks.RemoveRange(_context.WorkTasks);
    //    _context.OrderItems.RemoveRange(_context.OrderItems);
    //    _context.Orders.RemoveRange(_context.Orders);
    //    _context.CartItems.RemoveRange(_context.CartItems);
    //    _context.Carts.RemoveRange(_context.Carts);
    //    _context.Items.RemoveRange(_context.Items);
    //    _context.SubCategories.RemoveRange(_context.SubCategories);
    //    _context.ProductCategories.RemoveRange(_context.ProductCategories);
    //    _context.Discounts.RemoveRange(_context.Discounts);
    //    _context.Users.RemoveRange(_context.Users);
    //    _context.Inventories.RemoveRange(_context.Inventories);

    //    await _context.SaveChangesAsync();
    //}

    private async Task SeedUsers()
    {
        var users = new List<ApplicationUser>();

        // Director
        users.Add(new Director
        {
            Id = Guid.NewGuid(),
            UserName = "director_main",
            Email = "director@company.com",
            PhoneNumber = "+79990001122",
            FirstName = "Иван",
            LastName = "Петров",
            UserType = UserType.Director,
            Password = "SecurePass123!"
        });

        // Order Manager
        var orderManager = new OrderManager
        {
            Id = Guid.NewGuid(),
            UserName = "order_manager",
            Email = "manager@company.com",
            PhoneNumber = "+79990001133",
            FirstName = "Алексей",
            LastName = "Смирнов",
            UserType = UserType.OrderManager,
            Password = "ManagerPass456!"
        };
        users.Add(orderManager);

        // Tech Order Leads
        var techLead1 = new TechOrderLead
        {
            Id = Guid.NewGuid(),
            UserName = "tech_lead_1",
            Email = "techlead1@company.com",
            PhoneNumber = "+79990001144",
            FirstName = "Дмитрий",
            LastName = "Васильев",
            UserType = UserType.TechOrderLead,
            Password = "TechLeadPass789!"
        };

        var techLead2 = new TechOrderLead
        {
            Id = Guid.NewGuid(),
            UserName = "tech_lead_2",
            Email = "techlead2@company.com",
            PhoneNumber = "+79990001155",
            FirstName = "Сергей",
            LastName = "Николаев",
            UserType = UserType.TechOrderLead,
            Password = "TechLeadPass012!"
        };
        users.Add(techLead1);
        users.Add(techLead2);

        // Workers
        for (int i = 1; i <= 4; i++)
        {
            users.Add(new Worker
            {
                Id = Guid.NewGuid(),
                UserName = $"worker_{i}",
                Email = $"worker{i}@company.com",
                PhoneNumber = $"+79990001{100 + i}",
                FirstName = $"Рабочий {i}",
                LastName = "Команды",
                UserType = UserType.Worker,
                Password = $"WorkerPass{i}!"
            });
        }

        // Individual Customers
        var indCustomer1 = new IndividualCustomer
        {
            Id = Guid.NewGuid(),
            UserName = "ind_customer1",
            Email = "individual1@client.com",
            PhoneNumber = "+79991112233",
            FirstName = "Андрей",
            LastName = "Иванов",
            PassportIdNumber = "1234567890",
            UserType = UserType.IndividualCustomer,
            Password = "IndPass123!"
        };

        var indCustomer2 = new IndividualCustomer
        {
            Id = Guid.NewGuid(),
            UserName = "ind_customer2",
            Email = "individual2@client.com",
            PhoneNumber = "+79991114455",
            FirstName = "Мария",
            LastName = "Сидорова",
            PassportIdNumber = "0987654321",
            UserType = UserType.IndividualCustomer,
            Password = "IndPass456!"
        };
        users.Add(indCustomer1);
        users.Add(indCustomer2);

        // Legal Customers
        var legalCustomer1 = new LegalCustomer
        {
            Id = Guid.NewGuid(),
            UserName = "legal_customer1",
            Email = "company1@client.com",
            PhoneNumber = "+79992223344",
            CompanyName = "ООО 'Ивент Про'",
            TaxIdNumber = "7701234567",
            LegalAddress = "Москва, ул. Тверская, 1",
            ContactPersonFullName = "Петр Васильев",
            isGovernment = false,
            UserType = UserType.LegalCustomer,
            Password = "LegalPass123!"
        };

        var legalCustomer2 = new LegalCustomer
        {
            Id = Guid.NewGuid(),
            UserName = "legal_customer2",
            Email = "gov@client.com",
            PhoneNumber = "+79993334455",
            CompanyName = "Министерство Культуры",
            TaxIdNumber = "7707654321",
            LegalAddress = "Москва, Красная площадь, 1",
            ContactPersonFullName = "Ольга Иванова",
            isGovernment = true,
            GovernmentCode = "GOV-001",
            UserType = UserType.LegalCustomer,
            Password = "LegalPass456!"
        };
        users.Add(legalCustomer1);
        users.Add(legalCustomer2);

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();
    }

    private async Task SeedDiscounts()
    {
        var discounts = new List<Discount>
        {
            new() { Id = Guid.NewGuid(), Name = "Скидка новичка", Description = "Первая покупка", DiscountPercent = 10 },
            new() { Id = Guid.NewGuid(), Name = "Оптовая скидка", Description = "Для крупных заказов", DiscountPercent = 15 }
        };

        await _context.Discounts.AddRangeAsync(discounts);
        await _context.SaveChangesAsync();
    }

    private async Task SeedCategoriesAndSubcategories()
    {
        var categories = new List<ProductCategory>
        {
            new() { Id = Guid.NewGuid(), Name = "Звуковое оборудование", Description = "Профессиональное аудио оборудование" },
            new() { Id = Guid.NewGuid(), Name = "Световое оборудование", Description = "Сценическое освещение и эффекты" },
            new() { Id = Guid.NewGuid(), Name = "Видеоэкраны и панели", Description = "Экраны для видеотрансляций" },
            new() { Id = Guid.NewGuid(), Name = "Сцена и сценические конструкции", Description = "Конструкции для сцены" }
        };

        await _context.ProductCategories.AddRangeAsync(categories);
        await _context.SaveChangesAsync();

        var subcategories = new List<SubCategory>();

        // Звуковое оборудование
        var soundCategory = categories[0];
        subcategories.AddRange(new[]
        {
            new SubCategory { Id = Guid.NewGuid(), Name = "Колонки", ParentCategoryId = soundCategory.Id },
            new SubCategory { Id = Guid.NewGuid(), Name = "Микрофоны", ParentCategoryId = soundCategory.Id },
            new SubCategory { Id = Guid.NewGuid(), Name = "Мониторы", ParentCategoryId = soundCategory.Id },
            new SubCategory { Id = Guid.NewGuid(), Name = "Стойки", ParentCategoryId = soundCategory.Id }
        });

        // Световое оборудование
        var lightCategory = categories[1];
        subcategories.AddRange(new[]
        {
            new SubCategory { Id = Guid.NewGuid(), Name = "Стробоскоп", ParentCategoryId = lightCategory.Id },
            new SubCategory { Id = Guid.NewGuid(), Name = "Моторизированные головы", ParentCategoryId = lightCategory.Id },
            new SubCategory { Id = Guid.NewGuid(), Name = "Светодиодные панели", ParentCategoryId = lightCategory.Id },
            new SubCategory { Id = Guid.NewGuid(), Name = "Генератор эффектов", ParentCategoryId = lightCategory.Id }
        });

        // Видеоэкраны
        var videoCategory = categories[2];
        subcategories.Add(new SubCategory { Id = Guid.NewGuid(), Name = "Светодиодные экраны", ParentCategoryId = videoCategory.Id });

        // Сценические конструкции
        var stageCategory = categories[3];
        subcategories.AddRange(new[]
        {
            new SubCategory { Id = Guid.NewGuid(), Name = "Подиумы", ParentCategoryId = stageCategory.Id },
            new SubCategory { Id = Guid.NewGuid(), Name = "Тотемные Стойки", ParentCategoryId = stageCategory.Id },
            new SubCategory { Id = Guid.NewGuid(), Name = "Сценические комплексы", ParentCategoryId = stageCategory.Id }
        });

        await _context.SubCategories.AddRangeAsync(subcategories);
        await _context.SaveChangesAsync();
    }

    private async Task SeedInventoryAndItems()
    {
        var inventories = new List<ItemInventory>();
        var items = new List<Item>();
        var discounts = await _context.Discounts.ToListAsync();
        var subcategories = await _context.SubCategories.ToListAsync();

        // Звуковое оборудование
        var soundSubs = new Dictionary<string, (int count, string[] names, string[] descs, decimal[] prices)>
        {
            ["Колонки"] = (2,
                ["Yamaha DZR315", "JBL EON615"],
                ["{\"Мощность\":\"2000Вт\", \"Частотный диапазон\":\"45Hz-20kHz\"}",
             "{\"Мощность\":\"600Вт\", \"Вес\":\"15кг\"}"],
                [150000m, 75000m]),

            ["Микрофоны"] = (2,
                ["Shure SM58", "Sennheiser e935"],
                ["{\"Тип\":\"Динамический\", \"Частотный диапазон\":\"50Hz-15kHz\"}",
             "{\"Тип\":\"Конденсаторный\", \"Чувствительность\":\"-54dB\"}"],
                [12000m, 15000m]),

            ["Мониторы"] = (1,
                ["LD Systems MEI 1000 G2"],
                ["{\"Активный монитор 1000Вт\", \"2-полосный\"}"],
                [45000m]),

            ["Стойки"] = (2,
                ["K&M 210/9", "Ultimate Support TS-90B"],
                ["{\"Высота\":\"1.5м\", \"Грузоподъемность\":\"50кг\"}",
             "{\"Высота\":\"2.1м\", \"Телескопическая\"}"],
                [3500m, 4200m])
        };

        // Световое оборудование
        var lightSubs = new Dictionary<string, (int count, string[] names, string[] descs, decimal[] prices)>
        {
            ["Стробоскоп"] = (2,
                ["American DJ Mega Strobe", "Chauvet STRIKE 4"],
                ["{\"Мощность\":\"1000Вт\", \"Скорость\":\"0-20Гц\"}",
             "{\"LED строб\", \"RGB цвет\"}"],
                [28000m, 45000m]),

            ["Моторизированные головы"] = (2,
                ["Martin MAC Aura XB", "Clay Paky Sharpy"],
                ["{\"7-цветный LED\", \"Пан-тилт\"}",
             "{\"229Вт\", \"Луч 0.65°\"}"],
                [210000m, 185000m]),

            ["Светодиодные панели"] = (2,
                ["Chauvet COLORband PiX", "Blizzard Pucks Hex6"],
                ["{\"144 пикселя\", \"IP65\"}",
             "{\"6-гранные кластеры\", \"Беспроводные\"}"],
                [78000m, 92000m]),

            ["Генератор эффектов"] = (3,
                ["Look Solutions Viper NT", "Antari Z-350F", "MDG Atmosphere ION"],
                ["{\"Тип\":\"Дым-машина\", \"Производительность\":\"15000куб.фут/мин\"}",
             "{\"Тип\":\"Туман\", \"Объем бака\":\"1л\"}",
             "{\"Тип\":\"Вентилятор\", \"Скорость\":\"12м/с\"}"],
                [65000m, 23000m, 45000m])
        };

        // Видеоэкраны
        var videoSubs = new Dictionary<string, (int count, string[] names, string[] descs, decimal[] prices)>
        {
            ["Светодиодные экраны"] = (1,
                ["Absen A3 Pro"],
                ["{\"Шаг пикселя\":\"3мм\", \"Яркость\":\"1500нит\"}"],
                [2500000m])
        };

        // Сценические конструкции
        var stageSubs = new Dictionary<string, (int count, string[] names, string[] descs, decimal[] prices)>
        {
            ["Подиумы"] = (2,
                ["Global Truss PDS-210", "Stagemaker PT-3x3"],
                ["{\"Размер\":\"2x1м\", \"Нагрузка\":\"500кг/м²\"}",
             "{\"Размер\":\"3x3м\", \"Модульная система\"}"],
                [45000m, 68000m]),

            ["Тотемные Стойки"] = (2,
                ["TrussBase TBS-2M", "Proline T-1M"],
                ["{\"Высота\":\"2м\", \"Диаметр\":\"40см\"}",
             "{\"Высота\":\"1м\", \"Переносная\"}"],
                [12000m, 8500m]),

            ["Сценические комплексы"] = (2,
                ["StageCo SC-250", "Matrix STG-MID"],
                ["{\"Площадь\":\"25м²\", \"Высота\":\"6м\"}",
             "{\"Мобильная сцена\", \"Для помещений\"}"],
                [450000m, 280000m])
        };

        // Генерация всех товаров
        GenerateItemsForCategory(subcategories, soundSubs, items);
        GenerateItemsForCategory(subcategories, lightSubs, items);
        GenerateItemsForCategory(subcategories, videoSubs, items);
        GenerateItemsForCategory(subcategories, stageSubs, items);

        // Создание инвентаря
        foreach (var item in items)
        {
            var inventory = new ItemInventory
            {
                Id = Guid.NewGuid(),
                Quantity = new Random().Next(2, 15)
            };
            inventories.Add(inventory);
            item.InventoryId = inventory.Id;
        }

        await _context.Inventories.AddRangeAsync(inventories);
        await _context.Items.AddRangeAsync(items);
        await _context.SaveChangesAsync();
    }

    private void GenerateItemsForCategory(
        List<SubCategory> allSubcategories,
        Dictionary<string, (int count, string[] names, string[] descs, decimal[] prices)> config,
        List<Item> items)
    {
        foreach (var (subName, data) in config)
        {
            var subCategory = allSubcategories.First(s => s.Name == subName);
            items.AddRange(CreateItemsForSubcategory(
                subCategory,
                data.count,
                data.names,
                data.descs,
                data.prices));
        }
    }
    private IEnumerable<Item> CreateItemsForSubcategory(
        SubCategory subCategory,
        int count,
        string[] names,
        string[] descriptions,
        params decimal[] prices)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new Item
            {
                Id = Guid.NewGuid(),
                Name = names[i],
                Description = descriptions[i],
                Price = prices[i],
                Status = ProductState.Available,
                SubCategoryId = subCategory.Id,
                ImageName = $"{subCategory.Name.ToLower()}_{i + 1}.jpg"
            };
        }
    }

    private async Task SeedOrdersWithRelatedData()
    {
        var orderManager = await _context.Users.OfType<OrderManager>().FirstAsync();
        var techLeads = await _context.Users.OfType<TechOrderLead>().ToListAsync();
        var workers = await _context.Users.OfType<Worker>().ToListAsync();
        var customers = await _context.Users
            .Where(u => u.UserType == UserType.IndividualCustomer ||
                       u.UserType == UserType.LegalCustomer)
            .ToListAsync();
        var items = await _context.Items.ToListAsync();

        // Создаем 2 заказа
        var orders = new List<OrderBase>();
        foreach (var customer in customers.Take(2))
        {
            var order = new OrderBase
            {
                Id = Guid.NewGuid(),
                EventAddress = "Москва, Ленинградский пр-т 37",
                TotalPrice = 0, // Рассчитается после добавления items
                OrderType = OrderType.Rent,
                PaymentState = PaymentStatus.Paid,
                CustomerId = customer.Id,
                OrderManagerId = orderManager.Id,
                TechOrderLeadId = techLeads[0].Id,
                EventDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
                StartRentDate = DateOnly.FromDateTime(DateTime.Now.AddDays(28)),
                EndRentDate = DateOnly.FromDateTime(DateTime.Now.AddDays(31))
            };

            // Добавляем OrderItems
            var orderItems = items.Take(3).Select(item => new OrderItem
            {
                Id = Guid.NewGuid(),
                Quantity = new Random().Next(1, 5),
                ProductId = item.Id,
                OrderBaseId = order.Id
            }).ToList();

            order.TotalPrice = orderItems.Sum(oi => oi.Quantity * items.First(i => i.Id == oi.ProductId).Price);
            order.OrderItems = orderItems;

            // Создаем OrderCrew
            var orderCrew = new OrderCrew
            {
                Id = Guid.NewGuid(),
                WorkDate = DateOnly.FromDateTime(DateTime.Now.AddDays(28)),
                TechLeadId = techLeads[0].Id,
                OrderBaseId = order.Id,
                Workers = workers.Take(2).ToList()
            };

            // Создаем WorkTask и назначения
            var workTask = new WorkTask
            {
                Id = Guid.NewGuid(),
                Description = "Подготовка оборудования к мероприятию",
                WorkTaskState = WorkTaskState.Completed
            };

            var assignment = new WorkTaskAssignment
            {
                Id = Guid.NewGuid(),
                IsCompleted = true,
                WorkTaskId = workTask.Id,
                OrderCrewId = orderCrew.Id
            };

            order.OrderCrews = new List<OrderCrew> { orderCrew };
            await _context.WorkTasks.AddAsync(workTask);
            await _context.WorkTaskAssignments.AddAsync(assignment);

            orders.Add(order);
        }

        await _context.Orders.AddRangeAsync(orders);
        await _context.SaveChangesAsync();
    }

    private async Task SeedCarts()
    {
        var customers = await _context.Users
            .Where(u => u.UserType == UserType.IndividualCustomer ||
                       u.UserType == UserType.LegalCustomer)
            .Take(2)
            .ToListAsync();

        var items = await _context.Items.Take(3).ToListAsync();

        foreach (var customer in customers)
        {
            var cartId = Guid.NewGuid();
            var cart = new Cart
            {
                Id = cartId,
                CustomerId = customer.Id,
                TotalPrice = items.Sum(i => i.Price * 2),
                Items = items.Select(item => new CartItem
                {
                    Id = Guid.NewGuid(),
                    Quantity = 2,
                    ProductId = item.Id,
                    CartId = cartId
                }).ToList()
            };

            await _context.Carts.AddAsync(cart);
        }

        await _context.SaveChangesAsync();
    }
}