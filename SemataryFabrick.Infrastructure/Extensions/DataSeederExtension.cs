using System.Text.Json;
using SemataryFabrick.Domain.Entities;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.Items;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using SemataryFabrick.Domain.Entities.Models;

using SemataryFabrick.Infrastructure.Implementations.Contexts;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using Microsoft.EntityFrameworkCore;

namespace SemataryFabrick.Infrastructure.Extensions;

public class DataSeederExtension(ApplicationContext context)
{
    public async Task SeedDataAsync()
    {
        await SeedUsers();
        await SeedDiscounts();
        await SeedProductCategories();
        await SeedSubCategories();
        await SeedItems();
        await SeedOrdersWithRelatedData();
        // await SeedCartsWithItems(); // Закомментировано по требованию
    }

    private async Task SeedUsers()
    {

        // Director
        var director = new Director
        {
            Id = Guid.NewGuid(),
            UserType = UserType.Director,
            UserName = "director",
            Password = "director123",
            Email = "director@company.com",
            PhoneNumber = "+1234567890",
            FirstName = "Иван",
            LastName = "Директоров"
        };

        // Order Manager
        var orderManager = new OrderManager
        {
            Id = Guid.NewGuid(),
            UserType = UserType.OrderManager,
            UserName = "manager",
            Password = "manager123",
            Email = "manager@company.com",
            PhoneNumber = "+1234567891",
            FirstName = "Алексей",
            LastName = "Менеджеров"
        };

        // Tech Order Leads
        var techLead1 = new TechOrderLead
        {
            Id = Guid.NewGuid(),
            UserType = UserType.TechOrderLead,
            UserName = "techlead1",
            Password = "techlead123",
            Email = "techlead1@company.com",
            PhoneNumber = "+1234567892",
            FirstName = "Сергей",
            LastName = "Технический"
        };

        var techLead2 = new TechOrderLead
        {
            Id = Guid.NewGuid(),
            UserType = UserType.TechOrderLead,
            UserName = "techlead2",
            Password = "techlead123",
            Email = "techlead2@company.com",
            PhoneNumber = "+1234567893",
            FirstName = "Дмитрий",
            LastName = "Руководитель"
        };

        // Workers
        var workers = new List<Worker>
        {
            new()
            {
                Id = Guid.NewGuid(),
                UserType = UserType.Worker,
                UserName = "worker1",
                Password = "worker123",
                Email = "worker1@company.com",
                PhoneNumber = "+1234567894",
                FirstName = "Петр",
                LastName = "Рабочий"
            },
            new()
            {
                Id = Guid.NewGuid(),
                UserType = UserType.Worker,
                UserName = "worker2",
                Password = "worker123",
                Email = "worker2@company.com",
                PhoneNumber = "+1234567895",
                FirstName = "Андрей",
                LastName = "Монтажник"
            },
            new()
            {
                Id = Guid.NewGuid(),
                UserType = UserType.Worker,
                UserName = "worker3",
                Password = "worker123",
                Email = "worker3@company.com",
                PhoneNumber = "+1234567896",
                FirstName = "Николай",
                LastName = "Техник"
            },
            new()
            {
                Id = Guid.NewGuid(),
                UserType = UserType.Worker,
                UserName = "worker4",
                Password = "worker123",
                Email = "worker4@company.com",
                PhoneNumber = "+1234567897",
                FirstName = "Артем",
                LastName = "Специалист"
            }
        };

        // Individual Customers
        var individualCustomer1 = new IndividualCustomer
        {
            Id = Guid.NewGuid(),
            UserType = UserType.IndividualCustomer,
            UserName = "indcustomer1",
            Password = "customer123",
            Email = "indcustomer1@mail.com",
            PhoneNumber = "+7987654321",
            FirstName = "Олег",
            LastName = "Клиентов",
            PassportIdNumber = "1234567890"
        };

        var individualCustomer2 = new IndividualCustomer
        {
            Id = Guid.NewGuid(),
            UserType = UserType.IndividualCustomer,
            UserName = "indcustomer2",
            Password = "customer123",
            Email = "indcustomer2@mail.com",
            PhoneNumber = "+7987654322",
            FirstName = "Елена",
            LastName = "Заказчикова",
            PassportIdNumber = "0987654321"
        };

        // Legal Customers
        var legalCustomer1 = new LegalCustomer
        {
            Id = Guid.NewGuid(),
            UserType = UserType.LegalCustomer,
            UserName = "legalcustomer1",
            Password = "customer123",
            Email = "legalcustomer1@company.com",
            PhoneNumber = "+7987654333",
            CompanyName = "ООО 'Мероприятия и Ко'",
            TaxIdNumber = "123456789012",
            LegalAddress = "г. Москва, ул. Бизнесовая, д. 1",
            ContactPersonFullName = "Ирина Контактова"
        };

        var legalCustomer2 = new LegalCustomer
        {
            Id = Guid.NewGuid(),
            UserType = UserType.LegalCustomer,
            UserName = "legalcustomer2",
            Password = "customer123",
            Email = "legalcustomer2@company.com",
            PhoneNumber = "+7987654334",
            CompanyName = "ЗАО 'Крупные События'",
            TaxIdNumber = "098765432109",
            LegalAddress = "г. Москва, ул. Организационная, д. 5",
            ContactPersonFullName = "Александр Организаторов",
            isGovernment = true,
            GovernmentCode = "GOV123"
        };

        await context.Users.AddRangeAsync(director, orderManager, techLead1, techLead2);
        await context.Users.AddRangeAsync(workers);
        await context.Users.AddRangeAsync(individualCustomer1, individualCustomer2);
        await context.Users.AddRangeAsync(legalCustomer1, legalCustomer2);
        await context.SaveChangesAsync();
    }

    private async Task SeedDiscounts()
    {
        var discounts = new List<Discount>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Скидка новым клиентам",
                Description = "Скидка 10% для новых клиентов при первом заказе",
                DiscountPercent = 10
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Сезонная скидка",
                Description = "Сезонная скидка 15% на аренду оборудования",
                DiscountPercent = 15
            }
        };

        await context.Discounts.AddRangeAsync(discounts);
        await context.SaveChangesAsync();
    }

    private async Task SeedProductCategories()
    {
        var categories = new List<ProductCategory>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Звуковое оборудование",
                Description = "Оборудование для звукового сопровождения мероприятий"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Световое оборудование",
                Description = "Оборудование для светового оформления мероприятий"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Видеоэкраны и панели",
                Description = "Экраны и панели для видео трансляций"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Сцена и сценические конструкции",
                Description = "Конструкции для сцены и подиумы"
            }
        };

        await context.ProductCategories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }

    private async Task SeedSubCategories()
    {
        var categories = await context.ProductCategories.ToListAsync();
        var soundCategory = categories.First(c => c.Name == "Звуковое оборудование");
        var lightCategory = categories.First(c => c.Name == "Световое оборудование");
        var videoCategory = categories.First(c => c.Name == "Видеоэкраны и панели");
        var stageCategory = categories.First(c => c.Name == "Сцена и сценические конструкции");

        var subCategories = new List<SubCategory>
        {
            // Звуковое оборудование
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Колонки",
                ParentCategoryId = soundCategory.Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Микрофоны",
                ParentCategoryId = soundCategory.Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Мониторы",
                ParentCategoryId = soundCategory.Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Стойки",
                ParentCategoryId = soundCategory.Id
            },
            
            // Световое оборудование
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Стробоскоп",
                ParentCategoryId = lightCategory.Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Моторизированные головы",
                ParentCategoryId = lightCategory.Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Светодиодные панели",
                ParentCategoryId = lightCategory.Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Генератор эффектов",
                ParentCategoryId = lightCategory.Id
            },
            
            // Видеоэкраны и панели
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Светодиодные экраны",
                ParentCategoryId = videoCategory.Id
            },
            
            // Сцена и сценические конструкции
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Подиумы",
                ParentCategoryId = stageCategory.Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Тотемные Стойки",
                ParentCategoryId = stageCategory.Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Сценические комплексы",
                ParentCategoryId = stageCategory.Id
            }
        };

        await context.SubCategories.AddRangeAsync(subCategories);
        await context.SaveChangesAsync();
    }

    private async Task SeedItems()
    {
        var subCategories = await context.SubCategories.ToListAsync();
        var discounts = await context.Discounts.ToListAsync();

        var items = new List<Item>();

        // Колонки
        var speakersSubCat = subCategories.First(sc => sc.Name == "Колонки");
        items.AddRange(new[]
        {
            CreateItem(
                "JBL SRX835P",
                "Активная трехполосная полнодиапазонная система",
                25000,
                speakersSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Активная колонка"},
                    {"Мощность", "1500 Вт"},
                    {"Частотный диапазон", "40 Гц - 20 кГц"},
                    {"Вес", "32 кг"},
                    {"Размеры", "533 x 432 x 432 мм"}
                }),
            CreateItem(
                "Yamaha DXR12",
                "Активная двухполосная полнодиапазонная система",
                18000,
                speakersSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Активная колонка"},
                    {"Мощность", "1100 Вт"},
                    {"Частотный диапазон", "52 Гц - 20 кГц"},
                    {"Вес", "19.5 кг"},
                    {"Размеры", "418 x 380 x 362 мм"}
                })
        });

        // Микрофоны
        var micsSubCat = subCategories.First(sc => sc.Name == "Микрофоны");
        items.AddRange(new[]
        {
            CreateItem(
                "Shure SM58",
                "Динамический вокальный микрофон",
                5000,
                micsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Динамический"},
                    {"Назначение", "Вокальный"},
                    {"Частотный диапазон", "50 Гц - 15 кГц"},
                    {"Чувствительность", "-54.5 dBV/Pa"},
                    {"Вес", "298 г"}
                }),
            CreateItem(
                "Sennheiser e935",
                "Динамический вокальный микрофон",
                6500,
                micsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Динамический"},
                    {"Назначение", "Вокальный"},
                    {"Частотный диапазон", "40 Гц - 18 кГц"},
                    {"Чувствительность", "-52 dBV/Pa"},
                    {"Вес", "330 г"}
                })
        });

        // Мониторы
        var monitorsSubCat = subCategories.First(sc => sc.Name == "Мониторы");
        items.Add(CreateItem(
            "Behringer Eurolive B112D",
            "Активный сценический монитор",
            12000,
            monitorsSubCat.Id,
            new Dictionary<string, string>
            {
                {"Тип", "Активный монитор"},
                {"Мощность", "1000 Вт"},
                {"Частотный диапазон", "55 Гц - 20 кГц"},
                {"Вес", "14.5 кг"},
                {"Размеры", "362 x 350 x 530 мм"}
            }));

        // Стойки
        var standsSubCat = subCategories.First(sc => sc.Name == "Стойки");
        items.AddRange(new[]
        {
            CreateItem(
                "K&M 210/9",
                "Стойка для микрофона",
                1500,
                standsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Стойка микрофонная"},
                    {"Максимальная высота", "1.5 м"},
                    {"Минимальная высота", "0.6 м"},
                    {"Вес", "1.2 кг"},
                    {"Грузоподъемность", "2 кг"}
                }),
            CreateItem(
                "K&M 25950",
                "Стойка для колонок",
                3000,
                standsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Стойка акустическая"},
                    {"Максимальная высота", "3 м"},
                    {"Минимальная высота", "1.2 м"},
                    {"Вес", "5.5 кг"},
                    {"Грузоподъемность", "50 кг"}
                })
        });

        // Стробоскопы
        var strobeSubCat = subCategories.First(sc => sc.Name == "Стробоскоп");
        items.AddRange(new[]
        {
            CreateItem(
                "Eurolite LED ST-1000",
                "Светодиодный стробоскоп",
                8000,
                strobeSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "LED стробоскоп"},
                    {"Мощность", "100 Вт"},
                    {"Яркость", "1000 Вт аналогового эквивалента"},
                    {"Вес", "2.5 кг"},
                    {"Размеры", "200 x 200 x 200 мм"}
                }),
            CreateItem(
                "Martin Atomic 3000",
                "Газоразрядный стробоскоп",
                15000,
                strobeSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Газоразрядный стробоскоп"},
                    {"Мощность", "3000 Вт"},
                    {"Яркость", "Очень высокая"},
                    {"Вес", "8 кг"},
                    {"Размеры", "300 x 300 x 300 мм"}
                })
        });

        // Моторизированные головы
        var movingHeadsSubCat = subCategories.First(sc => sc.Name == "Моторизированные головы");
        items.AddRange(new[]
        {
            CreateItem(
                "Chauvet Intimidator Spot 355",
                "Моторизированный прожектор",
                25000,
                movingHeadsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Моторизированная голова"},
                    {"Мощность", "150 Вт"},
                    {"Светодиоды", "RGBW"},
                    {"Вес", "12 кг"},
                    {"Размеры", "400 x 300 x 400 мм"}
                }),
            CreateItem(
                "Martin MAC Aura",
                "Моторизированный прожектор",
                35000,
                movingHeadsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Моторизированная голова"},
                    {"Мощность", "200 Вт"},
                    {"Светодиоды", "RGBW"},
                    {"Вес", "15 кг"},
                    {"Размеры", "450 x 350 x 450 мм"}
                })
        });

        // Светодиодные панели
        var ledPanelsSubCat = subCategories.First(sc => sc.Name == "Светодиодные панели");
        items.AddRange(new[]
        {
    CreateItem(
        "Chauvet COLORband PiX",
        "Светодиодная панель малая",
        12000,
        ledPanelsSubCat.Id,
        new Dictionary<string, string>
        {
            {"Тип", "LED панель"},
            {"Размер", "0.5 x 0.5 м"},
            {"Яркость", "800 нит"},
            {"Пикселей", "256"},
            {"Потребление", "200 Вт"}
        }),
    CreateItem(
        "Eurolite LED BAR-12",
        "Светодиодная панель большая",
        18000,
        ledPanelsSubCat.Id,
        new Dictionary<string, string>
        {
            {"Тип", "LED панель"},
            {"Размер", "1 x 1 м"},
            {"Яркость", "1200 нит"},
            {"Пикселей", "512"},
            {"Потребление", "400 Вт"}
        })
});

        // Генератор эффектов
        var effectGenSubCat = subCategories.First(sc => sc.Name == "Генератор эффектов");
        items.AddRange(new[]
        {
    CreateItem(
        "Look Solutions Unique Hazer",
        "Генератор дыма",
        25000,
        effectGenSubCat.Id,
        new Dictionary<string, string>
        {
            {"Тип", "Дым машина"},
            {"Производительность", "3000 куб.м/ч"},
            {"Объем бака", "5 л"},
            {"Вес", "10 кг"}
        }),
    CreateItem(
        "Antari F-80",
        "Генератор тумана",
        18000,
        effectGenSubCat.Id,
        new Dictionary<string, string>
        {
            {"Тип", "Туман машина"},
            {"Производительность", "2000 куб.м/ч"},
            {"Объем бака", "3 л"},
            {"Вес", "8 кг"}
        }),
    CreateItem(
        "MDG Atmosphere",
        "Вентилятор с эффектом ветра",
        32000,
        effectGenSubCat.Id,
        new Dictionary<string, string>
        {
            {"Тип", "Вентилятор"},
            {"Мощность", "2000 Вт"},
            {"Скорость", "3 режима"},
            {"Вес", "15 кг"}
        })
});

        // Светодиодные экраны
        var ledScreensSubCat = subCategories.First(sc => sc.Name == "Светодиодные экраны");
        items.Add(CreateItem(
            "Absen PL2.5",
            "Светодиодный экран 2.5mm pitch",
            150000,
            ledScreensSubCat.Id,
            new Dictionary<string, string>
            {
        {"Тип", "LED экран"},
        {"Шаг пикселя", "2.5 мм"},
        {"Яркость", "1500 нит"},
        {"Размер панели", "500 x 500 мм"},
        {"Потребление", "500 Вт/м²"}
            }));

        // Подиумы
        var podiumsSubCat = subCategories.First(sc => sc.Name == "Подиумы");
        items.AddRange(new[]
        {
    CreateItem(
        "Global Truss ST-132",
        "Подиум 1x1 м",
        8000,
        podiumsSubCat.Id,
        new Dictionary<string, string>
        {
            {"Размер", "1 x 1 м"},
            {"Высота", "0.5 м"},
            {"Грузоподъемность", "500 кг"},
            {"Вес", "20 кг"}
        }),
    CreateItem(
        "Prolyte S30",
        "Подиум 2x1 м",
        12000,
        podiumsSubCat.Id,
        new Dictionary<string, string>
        {
            {"Размер", "2 x 1 м"},
            {"Высота", "0.5 м"},
            {"Грузоподъемность", "1000 кг"},
            {"Вес", "35 кг"}
        })
});

        // Тотемные Стойки
        var totemSubCat = subCategories.First(sc => sc.Name == "Тотемные Стойки");
        items.AddRange(new[]
        {
    CreateItem(
        "Truss Tower 2m",
        "Тотемная стойка 2 метра",
        7000,
        totemSubCat.Id,
        new Dictionary<string, string>
        {
            {"Высота", "2 м"},
            {"Диаметр", "50 см"},
            {"Грузоподъемность", "200 кг"},
            {"Вес", "15 кг"}
        }),
    CreateItem(
        "Truss Tower 1m",
        "Тотемная стойка 1 метр",
        5000,
        totemSubCat.Id,
        new Dictionary<string, string>
        {
            {"Высота", "1 м"},
            {"Диаметр", "50 см"},
            {"Грузоподъемность", "150 кг"},
            {"Вес", "10 кг"}
        })
});

        // Сценические комплексы
        var stageComplexSubCat = subCategories.First(sc => sc.Name == "Сценические комплексы");
        items.AddRange(new[]
        {
    CreateItem(
        "Mountain 6x4",
        "Сценический комплекс средний",
        50000,
        stageComplexSubCat.Id,
        new Dictionary<string, string>
        {
            {"Размер", "6 x 4 м"},
            {"Высота", "1 м"},
            {"Вместимость", "20 человек"},
            {"Вес", "200 кг"}
        }),
    CreateItem(
        "Mountain 4x3",
        "Сценический комплекс малый",
        35000,
        stageComplexSubCat.Id,
        new Dictionary<string, string>
        {
            {"Размер", "4 x 3 м"},
            {"Высота", "0.8 м"},
            {"Вместимость", "12 человек"},
            {"Вес", "150 кг"}
        })
});

        // Создаем инвентарь с фиксированным количеством 30 для каждого товара
        var inventories = items.Select(item => new ItemInventory
        {
            Id = Guid.NewGuid(),
            Quantity = 30, // Фиксированное количество
            Items = new List<Item> { item }
        }).ToList();

        // Связываем товары с инвентарем
        foreach (var (item, inventory) in items.Zip(inventories))
        {
            item.InventoryId = inventory.Id;
        }

        await context.Inventories.AddRangeAsync(inventories);
        await context.Items.AddRangeAsync(items);
        await context.SaveChangesAsync();
    }
    private async Task SeedOrdersWithRelatedData()
    {
        var users = await context.Users.ToListAsync();
        var items = await context.Items.ToListAsync();
        var workers = users.OfType<Worker>().ToList();

        // Создаем заказы для Individual и Legal клиентов
        var individualOrder = await CreateOrder(
            users.OfType<IndividualCustomer>().First(),
            users.OfType<OrderManager>().First(),
            users.OfType<TechOrderLead>().First(),
            items.Take(3).ToList(),
            workers);

        var legalOrder = await CreateOrder(
            users.OfType<LegalCustomer>().First(),
            users.OfType<OrderManager>().First(),
            users.OfType<TechOrderLead>().Last(),
            items.Skip(3).Take(4).ToList(),
            workers);

        await context.SaveChangesAsync();
    }

    private Item CreateItem(string name, string description, decimal price, Guid subCategoryId, Dictionary<string, string> parameters)
    {
        return new Item
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = JsonSerializer.Serialize(parameters),
            Price = price,
            Status = ProductState.Available,
            SubCategoryId = subCategoryId,
            ImageName = $"{name.ToLower().Replace(" ", "_")}.jpg"
        };
    }

    private async Task<OrderBase> CreateOrder(ApplicationUser customer, OrderManager manager, TechOrderLead techLead, List<Item> items, List<Worker> workers)
    {
        var order = new OrderBase
        {
            Id = Guid.NewGuid(),
            EventAddress = "ул. Центральная, 1",
            TotalPrice = items.Sum(i => i.Price),
            OrderType = OrderType.Rent,
            PaymentState = PaymentStatus.Paid,
            CustomerId = customer.Id,
            OrderManagerId = manager.Id,
            TechOrderLeadId = techLead.Id,
            OrderItems = items.Select((item, index) => new OrderItem
            {
                Id = Guid.NewGuid(),
                Quantity = index + 1,
                ProductId = item.Id
            }).ToList(),
            OrderCrews = new List<OrderCrew>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    WorkDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                    TechLeadId = techLead.Id,
                    Workers = workers.Take(2).ToList(),
                    WorkTaskAssignments = CreateWorkTasks()
                }
            }
        };

        await context.Orders.AddAsync(order);
        return order;
    }

    private List<WorkTaskAssignment> CreateWorkTasks()
    {
        return new List<WorkTaskAssignment>
        {
            new()
            {
                Id = Guid.NewGuid(),
                IsCompleted = true,
                WorkTask = new WorkTask
                {
                    Id = Guid.NewGuid(),
                    Description = "Подготовка оборудования",
                    WorkTaskState = WorkTaskState.Completed
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                IsCompleted = true,
                WorkTask = new WorkTask
                {
                    Id = Guid.NewGuid(),
                    Description = "Доставка на площадку",
                    WorkTaskState = WorkTaskState.Completed
                }
            }
        };
    }
}