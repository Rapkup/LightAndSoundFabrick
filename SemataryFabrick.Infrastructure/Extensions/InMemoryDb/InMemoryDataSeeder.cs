using System.Text.Json;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models;
using SemataryFabrick.Domain.Entities.Models.CartModels;
using SemataryFabrick.Domain.Entities.Models.Items;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using System.Text.Encodings.Web;
using SemataryFabrick.Domain.Entities.Models.ProduIctItemModels;

namespace SemataryFabrick.Infrastructure.Extensions.InMemoryDb;

public class InMemoryDataSeeder
{
    private readonly InMemoryDatabase _db;

    public InMemoryDataSeeder(InMemoryDatabase db) => _db = db;

    public void SeedAll()
    {
        ClearData();
        SeedUsers();
        SeedDiscounts();
        SeedProductCategories();
        SeedSubCategories();
        SeedItems();
        SeedOrdersWithRelatedData();
        SeedCartsWithItems();
    }

    private void ClearData()
    {
        _db.Users.Clear();
        _db.Discounts.Clear();
        _db.ProductCategories.Clear();
        _db.SubCategories.Clear();
        _db.Items.Clear();
        _db.Inventories.Clear();
        _db.Orders.Clear();
        _db.Carts.Clear();
        _db.WorkTaskAssignments.Clear();
        _db.WorkTasks.Clear();
        _db.WorkerCrewRelations.Clear();
        _db.OrderItems.Clear();
        _db.CartItems.Clear();
    }

    private void SeedUsers()
    {
        // Руководство
        var director = new Director
        {
            Id = Guid.NewGuid(),
            UserType = UserType.Director,
            UserName = "director",
            Password = "1234",
            Email = "director@company.com",
            PhoneNumber = "+1234567890",
            FirstName = "Иван",
            LastName = "Директоров"
        };

        // Менеджер
        var orderManager = new OrderManager
        {
            Id = Guid.NewGuid(),
            UserType = UserType.OrderManager,
            UserName = "manager",
            Password = "1234",
            Email = "manager@company.com",
            PhoneNumber = "+1234567891",
            FirstName = "Алексей",
            LastName = "Менеджеров"
        };

        // Технические руководители
        var techLead1 = new TechOrderLead
        {
            Id = Guid.NewGuid(),
            UserType = UserType.TechOrderLead,
            UserName = "techlead1",
            Password = "1234",
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
            Password = "1234",
            Email = "techlead2@company.com",
            PhoneNumber = "+1234567893",
            FirstName = "Дмитрий",
            LastName = "Руководитель"
        };

        // Работники
        var workers = new List<Worker>
        {
            new()
            {
                Id = Guid.NewGuid(),
                UserType = UserType.Worker,
                UserName = "worker1",
                Password = "1234",
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
                Password = "1234",
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
                Password = "1234",
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
                Password = "1234",
                Email = "worker4@company.com",
                PhoneNumber = "+1234567897",
                FirstName = "Артем",
                LastName = "Специалист"
            }
        };

        // Физические клиенты
        var individualCustomer1 = new IndividualCustomer
        {
            Id = Guid.NewGuid(),
            UserType = UserType.IndividualCustomer,
            UserName = "indcustomer1",
            Password = "1234",
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
            Password = "1234",
            Email = "indcustomer2@mail.com",
            PhoneNumber = "+7987654322",
            FirstName = "Елена",
            LastName = "Заказчикова",
            PassportIdNumber = "0987654321"
        };

        // Юридические клиенты
        var legalCustomer1 = new LegalCustomer
        {
            Id = Guid.NewGuid(),
            UserType = UserType.LegalCustomer,
            UserName = "legalcustomer1",
            Password = "1234",
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
            Password = "1234",
            Email = "legalcustomer2@company.com",
            PhoneNumber = "+7987654334",
            CompanyName = "ЗАО 'Крупные События'",
            TaxIdNumber = "098765432109",
            LegalAddress = "г. Москва, ул. Организационная, д. 5",
            ContactPersonFullName = "Александр Организаторов",
            isGovernment = true,
            GovernmentCode = "GOV123"
        };

        _db.Users.Add(director);
        _db.Users.Add(orderManager);
        _db.Users.Add(techLead1);
        _db.Users.Add(techLead2);
        _db.Users.AddRange(workers);
        _db.Users.Add(individualCustomer1);
        _db.Users.Add(individualCustomer2);
        _db.Users.Add(legalCustomer1);
        _db.Users.Add(legalCustomer2);
    }

    private void SeedDiscounts()
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

        _db.Discounts.AddRange(discounts);
    }

    private void SeedProductCategories()
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

        _db.ProductCategories.AddRange(categories);
    }

    private void SeedSubCategories()
    {
        var soundCategory = _db.ProductCategories.First(c => c.Name == "Звуковое оборудование");
        var lightCategory = _db.ProductCategories.First(c => c.Name == "Световое оборудование");
        var videoCategory = _db.ProductCategories.First(c => c.Name == "Видеоэкраны и панели");
        var stageCategory = _db.ProductCategories.First(c => c.Name == "Сцена и сценические конструкции");

        var subCategories = new List<SubCategory>
        {
            // Звуковое оборудование
            new() { Id = Guid.NewGuid(), Name = "Колонки", ParentCategoryId = soundCategory.Id },
            new() { Id = Guid.NewGuid(), Name = "Микрофоны", ParentCategoryId = soundCategory.Id },
            new() { Id = Guid.NewGuid(), Name = "Мониторы", ParentCategoryId = soundCategory.Id },
            new() { Id = Guid.NewGuid(), Name = "Стойки", ParentCategoryId = soundCategory.Id },
            
            // Световое оборудование
            new() { Id = Guid.NewGuid(), Name = "Стробоскоп", ParentCategoryId = lightCategory.Id },
            new() { Id = Guid.NewGuid(), Name = "Моторизированные головы", ParentCategoryId = lightCategory.Id },
            new() { Id = Guid.NewGuid(), Name = "Светодиодные панели", ParentCategoryId = lightCategory.Id },
            new() { Id = Guid.NewGuid(), Name = "Генератор эффектов", ParentCategoryId = lightCategory.Id },
            
            // Видеоэкраны и панели
            new() { Id = Guid.NewGuid(), Name = "Светодиодные экраны", ParentCategoryId = videoCategory.Id },
            
            // Сцена и сценические конструкции
            new() { Id = Guid.NewGuid(), Name = "Подиумы", ParentCategoryId = stageCategory.Id },
            new() { Id = Guid.NewGuid(), Name = "Тотемные Стойки", ParentCategoryId = stageCategory.Id },
            new() { Id = Guid.NewGuid(), Name = "Сценические комплексы", ParentCategoryId = stageCategory.Id }
        };

        _db.SubCategories.AddRange(subCategories);
    }

    private void SeedItems()
    {
        var subCategories = _db.SubCategories;
        var discounts = _db.Discounts;

        var items = new List<Item>();

        // Колонки
        var speakersSubCat = subCategories.First(sc => sc.Name == "Колонки");
        items.AddRange(new[]
        {
            CreateItem("JBL SRX835P", "Активная трехполосная полнодиапазонная система", 1000, speakersSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Активная колонка"}, {"Мощность", "1500 Вт"},
                    {"Частотный диапазон", "40 Гц - 20 кГц"}, {"Вес", "32 кг"},
                    {"Размеры", "533 x 432 x 432 мм"}
                }),
            CreateItem("Yamaha DXR12", "Активная двухполосная полнодиапазонная система", 700, speakersSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Активная колонка"}, {"Мощность", "1100 Вт"},
                    {"Частотный диапазон", "52 Гц - 20 кГц"}, {"Вес", "19.5 кг"},
                    {"Размеры", "418 x 380 x 362 мм"}
                })
        });

        // Микрофоны
        var micsSubCat = subCategories.First(sc => sc.Name == "Микрофоны");
        items.AddRange(new[]
        {
            CreateItem("Shure SM58", "Динамический вокальный микрофон", 70, micsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Динамический"}, {"Назначение", "Вокальный"},
                    {"Частотный диапазон", "50 Гц - 15 кГц"}, {"Чувствительность", "-54.5 dBV/Pa"},
                    {"Вес", "298 г"}
                }),
            CreateItem("Sennheiser e935", "Динамический вокальный микрофон", 80, micsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Динамический"}, {"Назначение", "Вокальный"},
                    {"Частотный диапазон", "40 Гц - 18 кГц"}, {"Чувствительность", "-52 dBV/Pa"},
                    {"Вес", "330 г"}
                })
        });

        // Мониторы
        var monitorsSubCat = subCategories.First(sc => sc.Name == "Мониторы");
        items.Add(CreateItem(
            "Behringer Eurolive B112D", "Активный сценический монитор", 400, monitorsSubCat.Id,
            new Dictionary<string, string>
            {
                {"Тип", "Активный монитор"}, {"Мощность", "1000 Вт"},
                {"Частотный диапазон", "55 Гц - 20 кГц"}, {"Вес", "14.5 кг"},
                {"Размеры", "362 x 350 x 530 мм"}
            }));

        // Стойки
        var standsSubCat = subCategories.First(sc => sc.Name == "Стойки");
        items.AddRange(new[]
        {
            CreateItem("K&M 210-9", "Стойка для микрофона", 50, standsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Стойка микрофонная"}, {"Максимальная высота", "1.5 м"},
                    {"Минимальная высота", "0.6 м"}, {"Вес", "1.2 кг"}, {"Грузоподъемность", "2 кг"}
                }),
            CreateItem("K&M 25950", "Стойка для колонок", 1200, standsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Стойка акустическая"}, {"Максимальная высота", "3 м"},
                    {"Минимальная высота", "1.2 м"}, {"Вес", "5.5 кг"}, {"Грузоподъемность", "50 кг"}
                })
        });

        // Стробоскопы
        var strobeSubCat = subCategories.First(sc => sc.Name == "Стробоскоп");
        items.AddRange(new[]
        {
            CreateItem("Eurolite LED ST-1000", "Светодиодный стробоскоп", 720, strobeSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "LED стробоскоп"}, {"Мощность", "100 Вт"},
                    {"Яркость", "1000 Вт аналогового эквивалента"}, {"Вес", "2.5 кг"},
                    {"Размеры", "200 x 200 x 200 мм"}
                }),
            CreateItem("Martin Atomic 3000", "Газоразрядный стробоскоп", 600, strobeSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Газоразрядный стробоскоп"}, {"Мощность", "3000 Вт"},
                    {"Яркость", "Очень высокая"}, {"Вес", "8 кг"}, {"Размеры", "300 x 300 x 300 мм"}
                })
        });

        // Моторизированные головы
        var movingHeadsSubCat = subCategories.First(sc => sc.Name == "Моторизированные головы");
        items.AddRange(new[]
        {
            CreateItem("Chauvet Intimidator Spot 355", "Моторизированный прожектор", 1000, movingHeadsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Моторизированная голова"}, {"Мощность", "150 Вт"},
                    {"Светодиоды", "RGBW"}, {"Вес", "12 кг"}, {"Размеры", "400 x 300 x 400 мм"}
                }),
            CreateItem("Martin MAC Aura", "Моторизированный прожектор", 1400, movingHeadsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Моторизированная голова"}, {"Мощность", "200 Вт"},
                    {"Светодиоды", "RGBW"}, {"Вес", "15 кг"}, {"Размеры", "450 x 350 x 450 мм"}
                })
        });

        // Светодиодные панели
        var ledPanelsSubCat = subCategories.First(sc => sc.Name == "Светодиодные панели");
        items.AddRange(new[]
        {
            CreateItem("Chauvet COLORband PiX", "Светодиодная панель малая", 440, ledPanelsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "LED панель"}, {"Размер", "0.5 x 0.5 м"},
                    {"Яркость", "800 нит"}, {"Пикселей", "256"}, {"Потребление", "200 Вт"}
                }),
            CreateItem("Eurolite LED BAR-12", "Светодиодная панель большая", 680, ledPanelsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "LED панель"}, {"Размер", "1 x 1 м"},
                    {"Яркость", "1200 нит"}, {"Пикселей", "512"}, {"Потребление", "400 Вт"}
                })
        });

        // Генератор эффектов
        var effectGenSubCat = subCategories.First(sc => sc.Name == "Генератор эффектов");
        items.AddRange(new[]
        {
            CreateItem("Look Solutions Unique Hazer", "Генератор дыма", 200, effectGenSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Дым машина"}, {"Производительность", "3000 куб.м/ч"},
                    {"Объем бака", "5 л"}, {"Вес", "10 кг"}
                }),
            CreateItem("Antari F-80", "Генератор тумана", 200, effectGenSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Туман машина"}, {"Производительность", "2000 куб.м/ч"},
                    {"Объем бака", "3 л"}, {"Вес", "8 кг"}
                }),
            CreateItem("MDG Atmosphere", "Вентилятор с эффектом ветра", 150, effectGenSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Тип", "Вентилятор"}, {"Мощность", "2000 Вт"},
                    {"Скорость", "3 режима"}, {"Вес", "15 кг"}
                })
        });

        // Светодиодные экраны
        var ledScreensSubCat = subCategories.First(sc => sc.Name == "Светодиодные экраны");
        items.Add(CreateItem("Absen PL2.5", "Светодиодный экран 2.5mm pitch", 100, ledScreensSubCat.Id,
            new Dictionary<string, string>
            {
                {"Тип", "LED экран"}, {"Шаг пикселя", "2.5 мм"},
                {"Яркость", "1500 нит"}, {"Размер панели", "500 x 500 мм"}, {"Потребление", "500 Вт/м²"}
            }));

        // Подиумы
        var podiumsSubCat = subCategories.First(sc => sc.Name == "Подиумы");
        items.AddRange(new[]
        {
            CreateItem("Global Truss ST-132", "Подиум 1x1 м", 36, podiumsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Размер", "1 x 1 м"}, {"Высота", "0.5 м"},
                    {"Грузоподъемность", "500 кг"}, {"Вес", "20 кг"}
                }),
            CreateItem("Prolyte S30", "Подиум 2x1 м", 48, podiumsSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Размер", "2 x 1 м"}, {"Высота", "0.5 м"},
                    {"Грузоподъемность", "1000 кг"}, {"Вес", "35 кг"}
                })
        });

        // Тотемные Стойки
        var totemSubCat = subCategories.First(sc => sc.Name == "Тотемные Стойки");
        items.AddRange(new[]
        {
            CreateItem("Truss Tower 2m", "Тотемная стойка 2 метра", 28, totemSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Высота", "2 м"}, {"Диаметр", "50 см"},
                    {"Грузоподъемность", "200 кг"}, {"Вес", "15 кг"}
                }),
            CreateItem("Truss Tower 1m", "Тотемная стойка 1 метр", 20, totemSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Высота", "1 м"}, {"Диаметр", "50 см"},
                    {"Грузоподъемность", "150 кг"}, {"Вес", "10 кг"}
                })
        });

        // Сценические комплексы
        var stageComplexSubCat = subCategories.First(sc => sc.Name == "Сценические комплексы");
        items.AddRange(new[]
        {
            CreateItem("Mountain 6x4", "Сценический комплекс средний", 4000, stageComplexSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Размер", "6 x 4 м"}, {"Высота", "1 м"},
                    {"Вместимость", "20 человек"}, {"Вес", "200 кг"}
                }),
            CreateItem("Mountain 4x3", "Сценический комплекс малый", 2000, stageComplexSubCat.Id,
                new Dictionary<string, string>
                {
                    {"Размер", "4 x 3 м"}, {"Высота", "0.8 м"},
                    {"Вместимость", "12 человек"}, {"Вес", "150 кг"}
                })
        });

        // Создание инвентаря
        var inventories = items.Select(item => new ItemInventory
        {
            Id = Guid.NewGuid(),
            Quantity = 30,
            Items = new List<Item> { item }
        }).ToList();

        foreach (var (item, inventory) in items.Zip(inventories))
        {
            item.InventoryId = inventory.Id;
        }

        _db.Inventories.AddRange(inventories);
        _db.Items.AddRange(items);
    }

    private void SeedOrdersWithRelatedData()
    {
        var users = _db.Users;
        var items = _db.Items;
        var discounts = _db.Discounts;
        var workers = users.OfType<Worker>().ToList();
        var manager = users.OfType<OrderManager>().First();
        var techLead1 = users.OfType<TechOrderLead>().First();
        var techLead2 = users.OfType<TechOrderLead>().Last();

        // Основные заказы
        var orders = new List<OrderBase>
    {
        CreateOrder(
            customer: users.OfType<IndividualCustomer>().First(),
            manager: manager,
            techLead: techLead1,
            items: items.Take(3).ToList(),
            workers: workers,
            discounts: discounts,
            state: OrderState.Done,
            orderType: OrderType.Individual,
            paymentStatus: PaymentStatus.Paid,
            eventDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-7))),

        CreateOrder(
            customer: users.OfType<LegalCustomer>().First(),
            manager: manager,
            techLead: techLead2,
            items: items.Skip(3).Take(2).ToList(),
            workers: workers,
            discounts: discounts,
            state: OrderState.ProccessedByTechLead,
            orderType: OrderType.Rent,
            paymentStatus: PaymentStatus.Paid,
            startRentDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-5)),
            endRentDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-1)))
    };

        // Добавляем заказы для страницы TechLead
        var techLeadOrders = CreateOrdersForTechLeadPage(
            techLead: techLead1,
            customer: users.OfType<IndividualCustomer>().First(),
            manager: manager,
            items: items,
            workers: workers,
            discounts: discounts
        );
        orders.AddRange(techLeadOrders);

        // Добавляем заказы для страницы Manager
        var managerOrders = CreateOrdersForManagerPage(
            manager: manager,
            techLead1: techLead1,
            techLead2: techLead2,
            items: items,
            workers: workers,
            discounts: discounts
        );
        orders.AddRange(managerOrders);

        // Дополнительные тестовые заказы
        orders.AddRange(new[]
        {
        CreateOrder(
            users.OfType<IndividualCustomer>().Last(),
            manager,
            techLead1,
            items.Skip(5).Take(2).ToList(),
            workers,
            discounts,
            OrderState.ApprovedByManager,
            OrderType.Individual,
            PaymentStatus.PaymentConfirmation,
            eventDate: DateOnly.FromDateTime(DateTime.Now.AddDays(3)))
    });

        // Сохранение всех заказов
        foreach (var order in orders)
        {
            _db.Orders.Add(order);
            _db.OrderItems.AddRange(order.OrderItems);

            foreach (var crew in order.OrderCrews)
            {
                _db.OrderCrews.Add(crew);
                var relatedWorkers = _db.WorkerCrewRelations
                    .Where(r => r.CrewId == crew.Id)
                    .ToList();

                _db.WorkerCrewRelations.AddRange(relatedWorkers);

                var assignments = _db.WorkTaskAssignments
                    .Where(a => a.OrderCrewId == crew.Id)
                    .ToList();

                _db.WorkTasks.AddRange(assignments.Select(a => a.WorkTask));
                _db.WorkTaskAssignments.AddRange(assignments);
            }
        }
    }


    private void SeedCartsWithItems()
    {
        var customers = _db.Users.OfType<IndividualCustomer>().Take(2).ToList();
        var items = _db.Items.ToList();
        var discounts = _db.Discounts.ToList();

        // Корзина для аренды (Rent)
        var rentCart = new Cart
        {
            Id = Guid.NewGuid(),
            CustomerId = customers[0].Id,
            EventDate = null,
            Items = items.Take(3).Select((item, index) => new CartItem
            {
                Id = Guid.NewGuid(),
                ProductId = item.Id,
                Quantity = 1,
                StartRentDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                EndRentDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
                DiscountId = index == 0 ? discounts[0].Id : null
            }).ToList()
        };
        rentCart.TotalPrice = CalculateCartTotal(rentCart.Items.ToList(), items, discounts);

        // Корзина для мероприятия (Individual)
        var eventCart = new Cart
        {
            Id = Guid.NewGuid(),
            CustomerId = customers[1].Id,
            EventDate = DateOnly.FromDateTime(DateTime.Now.AddDays(10)),
            Items = items.Skip(3).Take(2).Select((item, index) => new CartItem
            {
                Id = Guid.NewGuid(),
                ProductId = item.Id,
                Quantity = 2,
                DiscountId = index == 1 ? discounts[1].Id : null
            }).ToList()
        };
        eventCart.TotalPrice = CalculateCartTotal(eventCart.Items.ToList(), items, discounts);

        // Сохраняем корзины и их элементы
        void AddCartWithItems(Cart cart)
        {
            _db.Carts.Add(cart);
            foreach (var item in cart.Items)
            {
                item.CartId = cart.Id;
                _db.CartItems.Add(item);
            }
        }

        AddCartWithItems(rentCart);
        AddCartWithItems(eventCart);
    }

    private Item CreateItem(string name, string description, decimal price, Guid subCategoryId,
        Dictionary<string, string> parameters)
    {
        return new Item
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = JsonSerializer.Serialize(parameters, new JsonSerializerOptions
            {
                WriteIndented = false,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }),
            Price = price,
            Status = ProductState.Available,
            SubCategoryId = subCategoryId,
            ImageName = $"{name.ToLower().Replace(" ", "_")}.jpg"
        };
    }

    private List<OrderBase> CreateOrdersForTechLeadPage(
    TechOrderLead techLead,
    ApplicationUser customer,
    OrderManager manager,
    List<Item> items,
    List<Worker> workers,
    List<Discount> discounts)
    {
        var orders = new List<OrderBase>();

        // Активный заказ (мероприятие)
        var activeOrder = CreateOrder(
            customer: customer,
            manager: manager,
            techLead: techLead,
            items: items.Take(2).ToList(),
            workers: workers,
            discounts: discounts,
            state: OrderState.ApprovedByManager,
            orderType: OrderType.Individual,
            paymentStatus: PaymentStatus.Unpaid,
            eventDate: DateOnly.FromDateTime(DateTime.Now.AddDays(5)));
        orders.Add(activeOrder);

        // Заказ в обработке (аренда)
        var processedOrder = CreateOrder(
            customer: customer,
            manager: manager,
            techLead: techLead,
            items: items.Skip(2).Take(3).ToList(),
            workers: workers,
            discounts: discounts,
            state: OrderState.ProccessedByTechLead,
            orderType: OrderType.Rent,
            paymentStatus: PaymentStatus.Unpaid,
            startRentDate: DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
            endRentDate: DateOnly.FromDateTime(DateTime.Now.AddDays(6)));
        orders.Add(processedOrder);

        // Завершенный заказ (мероприятие)
        var historyOrder = CreateOrder(
            customer: customer,
            manager: manager,
            techLead: techLead,
            items: items.Skip(5).Take(4).ToList(),
            workers: workers,
            discounts: discounts,
            state: OrderState.Done,
            orderType: OrderType.Individual,
            paymentStatus: PaymentStatus.Paid,
            eventDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-7)));
        orders.Add(historyOrder);

        return orders;
    }

    private List<OrderBase> CreateOrdersForManagerPage(
        OrderManager manager,
        TechOrderLead techLead1,
        TechOrderLead techLead2,
        List<Item> items,
        List<Worker> workers,
        List<Discount> discounts)
    {
        var orders = new List<OrderBase>();
        var individualCustomers = _db.Users.OfType<IndividualCustomer>().ToList();
        var legalCustomers = _db.Users.OfType<LegalCustomer>().ToList();

        // NewOrders (Stock)
        orders.Add(CreateOrder(
            customer: individualCustomers.First(),
            manager: manager,
            techLead: techLead1,
            items: items.Take(2).ToList(),
            workers: workers,
            discounts: discounts,
            state: OrderState.Stock,
            orderType: OrderType.Individual,
            paymentStatus: PaymentStatus.Unpaid,
            eventDate: DateOnly.FromDateTime(DateTime.Now.AddDays(3))));

        orders.Add(CreateOrder(
            customer: legalCustomers.First(),
            manager: manager,
            techLead: techLead2,
            items: items.Skip(3).Take(1).ToList(),
            workers: workers,
            discounts: discounts,
            state: OrderState.Stock,
            orderType: OrderType.Rent,
            paymentStatus: PaymentStatus.Unpaid,
            startRentDate: DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            endRentDate: DateOnly.FromDateTime(DateTime.Now.AddDays(5))));

        // InProgressOrders
        orders.Add(CreateOrder(
            customer: individualCustomers.Last(),
            manager: manager,
            techLead: techLead1,
            items: items.Skip(5).Take(3).ToList(),
            workers: workers,
            discounts: discounts,
            state: OrderState.ApprovedByManager,
            orderType: OrderType.Individual,
            paymentStatus: PaymentStatus.Unpaid,
            eventDate: DateOnly.FromDateTime(DateTime.Now.AddDays(7))));

        orders.Add(CreateOrder(
            customer: legalCustomers.Last(),
            manager: manager,
            techLead: techLead2,
            items: items.Skip(2).Take(4).ToList(),
            workers: workers,
            discounts: discounts,
            state: OrderState.ProccessedByTechLead,
            orderType: OrderType.Rent,
            paymentStatus: PaymentStatus.Paid,
            startRentDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-2)),
            endRentDate: DateOnly.FromDateTime(DateTime.Now.AddDays(2))));

        // PaymentConfirmationOrders
        var paymentOrder1 = CreateOrder(
            customer: individualCustomers.First(),
            manager: manager,
            techLead: techLead1,
            items: items.Take(3).ToList(),
            workers: workers,
            discounts: discounts,
            state: OrderState.Done,
            orderType: OrderType.Individual,
            paymentStatus: PaymentStatus.PaymentConfirmation,
            eventDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-3)));
        orders.Add(paymentOrder1);

        var paymentOrder2 = CreateOrder(
            customer: legalCustomers.First(),
            manager: manager,
            techLead: techLead2,
            items: items.Skip(4).Take(2).ToList(),
            workers: workers,
            discounts: discounts,
            state: OrderState.ApprovedByManager,
            orderType: OrderType.Rent,
            paymentStatus: PaymentStatus.PaymentConfirmation,
            startRentDate: DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            endRentDate: DateOnly.FromDateTime(DateTime.Now.AddDays(4)));
        orders.Add(paymentOrder2);

        return orders;
    }

    private OrderBase CreateOrder(
    ApplicationUser customer,
    OrderManager manager,
    TechOrderLead techLead,
    List<Item> items,
    List<Worker> workers,
    List<Discount> discounts,
    OrderState state,
    OrderType orderType,
    PaymentStatus paymentStatus,
    DateOnly? eventDate = null,
    DateOnly? startRentDate = null,
    DateOnly? endRentDate = null)
    {
        var order = new OrderBase
        {
            Id = Guid.NewGuid(),
            EventAddress = "ул. Центральная, 1",
            TotalPrice = 0,
            OrderType = orderType,
            OrderState = state,
            PaymentState = paymentStatus,
            CustomerId = customer.Id,
            OrderManagerId = manager.Id,
            TechOrderLeadId = techLead.Id,
            EventDate = orderType == OrderType.Individual ? eventDate : null,
            StartRentDate = orderType == OrderType.Rent ? startRentDate : null,
            EndRentDate = orderType == OrderType.Rent ? endRentDate : null,
            OrderItems = new List<OrderItem>(),
            OrderCrews = new List<OrderCrew>()
        };

        // Создание элементов заказа
        order.OrderItems = items.Select((item, index) => new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderBaseId = order.Id,
            ProductId = item.Id,
            Quantity = index + 1,
            DiscountId = index % 2 == 0 ? discounts.FirstOrDefault()?.Id : null
        }).ToList();

        // Расчет общей стоимости
        order.TotalPrice = CalculateOrderTotal(
            order.OrderItems.ToList(),
            items,
            discounts,
            orderType,
            order.StartRentDate,
            order.EndRentDate
        );

        // Используем CreateOrderCrew для создания команды
        var crew = CreateOrderCrew(
            orderId: order.Id,
            techLead: techLead,
            workers: workers,
            workDate: orderType == OrderType.Individual
                ? order.EventDate
                : order.StartRentDate
        );

        order.OrderCrews.Add(crew);

        return order;
    }


    private OrderCrew CreateOrderCrew(
    Guid orderId,
    TechOrderLead techLead,
    List<Worker> workers,
    DateOnly? workDate = null)
    {
        var crew = new OrderCrew
        {
            Id = Guid.NewGuid(),
            OrderBaseId = orderId,
            TechLeadId = techLead.Id,
            WorkDate = workDate ?? DateOnly.FromDateTime(DateTime.Now)
        };

        // Создаем связанные задачи и назначения
        var (tasks, assignments) = CreateWorkTasks(crew.Id);

        // Сохраняем все связанные сущности
        _db.WorkTasks.AddRange(tasks);
        _db.WorkTaskAssignments.AddRange(assignments);

        // Привязываем работников к команде
        workers.Take(2).ToList().ForEach(w =>
            _db.AddWorkerToCrew(w.Id, crew.Id));

        return crew;
    }

    private (List<WorkTask> Tasks, List<WorkTaskAssignment> Assignments) CreateWorkTasks(Guid crewId)
    {
        var tasks = new List<WorkTask>
    {
        new WorkTask
        {
            Id = Guid.NewGuid(),
            Description = "Подготовка оборудования",
            WorkTaskState = WorkTaskState.Completed
        },
        new WorkTask
        {
            Id = Guid.NewGuid(),
            Description = "Доставка на площадку",
            WorkTaskState = WorkTaskState.Completed
        }
    };

        var assignments = tasks.Select(task => new WorkTaskAssignment
        {
            Id = Guid.NewGuid(),
            IsCompleted = true,
            WorkTaskId = task.Id,
            OrderCrewId = crewId,
            WorkTask = task
        }).ToList();

        return (tasks, assignments);
    }

    private decimal CalculateOrderTotal(
    List<OrderItem> orderItems,
    List<Item> items,
    List<Discount> discounts,
    OrderType orderType, // Добавляем параметр с типом заказа
    DateOnly? startRentDate = null,
    DateOnly? endRentDate = null)
    {
        return orderItems.Sum(oi =>
        {
            var item = items.First(i => i.Id == oi.ProductId);
            var discount = discounts.FirstOrDefault(d => d.Id == oi.DiscountId);
            var price = item.Price * oi.Quantity;

            // Используем переданные параметры вместо обращения через OrderBase
            if (orderType == OrderType.Rent &&
                startRentDate.HasValue &&
                endRentDate.HasValue)
            {
                var days = endRentDate.Value.DayNumber - startRentDate.Value.DayNumber;
                price *= days > 0 ? days : 1;
            }

            return price * (1 - (discount?.DiscountPercent ?? 0) / 100m);
        });
    }

    private decimal CalculateCartTotal(List<CartItem> cartItems, List<Item> items, List<Discount> discounts)
    {
        return cartItems.Sum(ci =>
        {
            var item = items.First(i => i.Id == ci.ProductId);
            var discount = discounts.FirstOrDefault(d => d.Id == ci.DiscountId);
            var price = item.Price * ci.Quantity;

            // Для аренды учитываем количество дней
            if (ci.StartRentDate.HasValue && ci.EndRentDate.HasValue)
            {
                var days = ci.EndRentDate.Value.DayNumber - ci.StartRentDate.Value.DayNumber;
                price *= days > 0 ? days : 1;
            }

            return price * (1 - (discount?.DiscountPercent ?? 0) / 100m);
        });
    }

}