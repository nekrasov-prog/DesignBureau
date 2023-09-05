using Microsoft.AspNetCore.Identity;
using DesignBureauWebApplication.Data.Enum;
using DesignBureauWebApplication.Models;
using System.Diagnostics;
using System.Net;
using DesignBureauWebApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DesignBureauWebApplication.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Project.Any())
                {
                    context.Project.AddRange(new List<Project>()
                   {
                       new Project()
                       {
                           ProjectTitle = "Робот для земляных работ",
                           PlanStartDate = DateTime.Now.AddDays(5),
                           PlanOverDate = DateTime.Now.AddDays(65)
                        },
                       new Project()
                       {
                           ProjectTitle = "Робот-паук для исследования труднодоступных мест",
                           PlanStartDate = DateTime.Now.AddDays(30),
                           PlanOverDate = DateTime.Now.AddDays(70)
                       },
                       new Project()
                       {
                           ProjectTitle = "Робот-садовник для ухода за растениями",
                           PlanStartDate = DateTime.Now.AddDays(44),
                           PlanOverDate = DateTime.Now.AddDays(99)
                       },
                       new Project()
                       {
                           ProjectTitle = "Робот-няня для присмотра за маленькими детьми",
                           PlanStartDate = DateTime.Now.AddDays(-54),
                           PlanOverDate = DateTime.Now.AddDays(5)
                       }
                   });
                    context.SaveChanges();
                }

                if (!context.WorkDictionary.Any())
                {
                    context.WorkDictionary.Add(new WorkDictionary() { WorkTitle = "Модернизация" });
                    context.WorkDictionary.Add(new WorkDictionary() { WorkTitle = "Тестирование" });
                    context.WorkDictionary.Add(new WorkDictionary() { WorkTitle = "Разработка" });
                    context.SaveChanges();
                }

                if (!context.MaterialDictionary.Any())
                {
                    context.MaterialDictionary.AddRange(new List<MaterialDictionary>()
                    {
                        new MaterialDictionary()
                        {
                            MaterialName = "Конденсатор",
                            MaterialType = MaterialType.Electrical
                        },
                        new MaterialDictionary()
                        {
                            MaterialName = "Магнит",
                            MaterialType = MaterialType.Magnetic
                        },
                        new MaterialDictionary()
                        {
                            MaterialName = "Шестеренка",
                            MaterialType = MaterialType.Mechanical
                        },
                        new MaterialDictionary()
                        {
                            MaterialName = "Раствор аммиака",
                            MaterialType = MaterialType.Chemical
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.EquipmentDictionary.Any())
                {
                    context.EquipmentDictionary.AddRange(new List<EquipmentDictionary>()
                    {
                        new EquipmentDictionary()
                        {
                            EquipmentName = "Сверлильный станок",
                            EquipmentType = EquipmentType.Instrument
                        },
                        new EquipmentDictionary()
                        {
                            EquipmentName = "Лазерный резак",
                            EquipmentType = EquipmentType.Machine
                        },
                        new EquipmentDictionary()
                        {
                            EquipmentName = "3D-принтер",
                            EquipmentType = EquipmentType.Computer
                        },
                        new EquipmentDictionary()
                        {
                            EquipmentName = "Робот-манипулятор",
                            EquipmentType = EquipmentType.Robot
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Supplier.Any())
                {
                    context.Supplier.Add(new Supplier { SName = "Программистика", PhoneNumber = "123-456-7890", Email = "supplier1@example.com" });
                    context.Supplier.Add(new Supplier { SName = "Роботех", PhoneNumber = "234-567-8901", Email = "supplier2@example.com" });
                    context.Supplier.Add(new Supplier { SName = "МехЛаб", PhoneNumber = "345-678-9012", Email = "supplier3@example.com" });
                    context.SaveChanges();
                }

                if (!context.Position.Any())
                {
                    context.Position.Add(new Position() { JobTitle = "Разработчик" });
                    context.Position.Add(new Position() { JobTitle = "Инженер" });
                    context.Position.Add(new Position() { JobTitle = "Дизайнер" });
                    context.SaveChanges();
                }

                if (!context.AssignmentDictionary.Any())
                {
                    context.AssignmentDictionary.AddRange(new List<AssignmentDictionary>()
                    {
                        new AssignmentDictionary()
                        {
                            AssignmentTitle = "Спроектировать корпус робота"
                        },
                        new AssignmentDictionary()
                        {
                            AssignmentTitle = "Собрать и отладить электронику робота"
                        },
                        new AssignmentDictionary()
                        {
                            AssignmentTitle = "Написать и протестировать программное обеспечение робота"
                        },
                        new AssignmentDictionary()
                        {
                            AssignmentTitle = "Провести испытания и доработки робота"
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Location.Any())
                {
                    context.Location.AddRange(new List<Location>()
                    {
                        new Location()
                        {
                            City = "Москва",
                            Street = "Ленинский проспект",
                            HouseNumber = "12",
                            LocationType = LocationType.Workshop
                        },
                        new Location()
                        {
                            City = "Санкт-Петербург",
                            Street = "Невский проспект",
                            HouseNumber = "34",
                            LocationType = LocationType.Stock
                        },
                        new Location()
                        {
                            City = "Новосибирск",
                            Street = "Красный проспект",
                            HouseNumber = "56",
                            LocationType = LocationType.Laboratory
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Employee.Any())
                {
                    context.Employee.Add(new Employee()
                    {
                        ELastName = "Петров",
                        EFirstName = "Василий",
                        EPatronymic = "Иванович",
                        BirthDate = new DateTime(1985, 6, 15),
                        Email = "vasyl.petrov@example.com",
                        PhoneNumber = "+1-555-555-5555",
                        PositionId = context.Position.Select(p => p.PositionId).FirstOrDefault()
                    });
                    context.Employee.Add(new Employee()
                    {
                        ELastName = "Смирнова",
                        EFirstName = "Евгения",
                        EPatronymic = "Викторовна",
                        BirthDate = new DateTime(1990, 12, 25),
                        Email = "smirnova.evg@example.com",
                        PhoneNumber = "+1-555-555-5555",
                        PositionId = context.Position.Select(p => p.PositionId).FirstOrDefault()
                    });

                    context.SaveChanges();
                }

                if (!context.ProjectHistory.Any())
                {
                    context.ProjectHistory.AddRange(new List<ProjectHistory>()
                    {
                        new ProjectHistory()
                        {
                            ProjectId = 1,
                            ProjectStatus = ProjectStatus.Launch,
                            CreatedAt = DateTime.Now
                        },
                        new ProjectHistory()
                        {
                            ProjectId = 1,
                            ProjectStatus = ProjectStatus.Planning,
                            CreatedAt = DateTime.Now.AddDays(1)
                        },
                        new ProjectHistory()
                        {
                            ProjectId = 1,
                            ProjectStatus = ProjectStatus.Completed,
                            CreatedAt = DateTime.Now.AddDays(2)
                        },
                        new ProjectHistory()
                        {
                            ProjectId = 2,
                            ProjectStatus = ProjectStatus.Frozen,
                            CreatedAt = DateTime.Now
                        },
                        new ProjectHistory()
                        {
                            ProjectId = 2,
                            ProjectStatus = ProjectStatus.Cancelled,
                            CreatedAt = DateTime.Now.AddDays(3)
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Work.Any())
                {
                    context.Work.AddRange(new List<Work>()
                   {
                       new Work()
                       {
                           PlanStartDate = DateTime.Now.AddDays(6),
                           PlanOverDate = DateTime.Now.AddDays(8),
                           ProjectId = context.Project.Select(p => p.ProjectId).FirstOrDefault(),
                           WorkDictionaryId = context.WorkDictionary.Select(wd => wd.WorkDictionaryId).FirstOrDefault()
                       },
                       new Work()
                       {
                           PlanStartDate = DateTime.Now.AddDays(12),
                           PlanOverDate = DateTime.Now.AddDays(4),
                           ProjectId = context.Project.Select(p => p.ProjectId).FirstOrDefault(),
                           WorkDictionaryId = context.WorkDictionary.Select(wd => wd.WorkDictionaryId).FirstOrDefault() + 1
                       }
                   });
                    context.SaveChanges();
                }

                if (!context.WorkHistory.Any())
                {
                    context.WorkHistory.AddRange(new List<WorkHistory>()
                    {
                        new WorkHistory()
                        {
                            WorkId = 1,
                            WorkStatus = WorkStatus.Preparation,
                            CreatedAt = DateTime.Now
                        },
                        new WorkHistory()
                        {
                            WorkId = 1,
                            WorkStatus = WorkStatus.InProgress,
                            CreatedAt = DateTime.Now.AddHours(1)
                        },
                        new WorkHistory()
                        {
                            WorkId = 1,
                            WorkStatus = WorkStatus.Completed,
                            CreatedAt = DateTime.Now.AddHours(2)
                        },
                        new WorkHistory()
                        {
                            WorkId = 2,
                            WorkStatus = WorkStatus.Paused,
                            CreatedAt = DateTime.Now
                        },
                        new WorkHistory()
                        {
                            WorkId = 2,
                            WorkStatus = WorkStatus.InProgress,
                            CreatedAt = DateTime.Now.AddHours(3)
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Assignment.Any())
                {
                    context.Assignment.AddRange(new List<Assignment>()
                    {
                        new Assignment()
                        {
                            AssignmentDictionaryId = 1,
                            WorkId = 1,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(1)
                        },
                        new Assignment()
                        {
                            AssignmentDictionaryId = 2,
                            WorkId = 2,
                            StartDate = DateTime.Now.AddDays(1),
                            EndDate = DateTime.Now.AddDays(2)
                        },
                        new Assignment()
                        {
                            AssignmentDictionaryId = 3,
                            WorkId = 2,
                            StartDate = DateTime.Now.AddDays(2),
                            EndDate = DateTime.Now.AddDays(3)
                        },
                        new Assignment()
                        {
                            AssignmentDictionaryId = 4,
                            WorkId = 1,
                            StartDate = DateTime.Now.AddDays(3),
                            EndDate = DateTime.Now.AddDays(4)
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.AssignmentHistory.Any())
                {
                    context.AssignmentHistory.AddRange(new List<AssignmentHistory>()
                    {
                        new AssignmentHistory()
                        {
                            AssignmentId = 1,
                            AssignmentStatus = AssignmentStatus.Completed,
                            CreatedAt = DateTime.Now
                        },
                        new AssignmentHistory()
                        {
                            AssignmentId = 2,
                            AssignmentStatus = AssignmentStatus.InProgress,
                            CreatedAt = DateTime.Now.AddHours(1)
                        },
                        new AssignmentHistory()
                        {
                            AssignmentId = 3,
                            AssignmentStatus = AssignmentStatus.Completed,
                            CreatedAt = DateTime.Now.AddHours(2)
                        },
                        new AssignmentHistory()
                        {
                            AssignmentId = 2,
                            AssignmentStatus = AssignmentStatus.Rebooted,
                            CreatedAt = DateTime.Now
                        },
                        new AssignmentHistory()
                        {
                            AssignmentId = 2,
                            AssignmentStatus = AssignmentStatus.Preparation,
                            CreatedAt = DateTime.Now.AddHours(3)
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Execution.Any())
                {
                    context.Execution.AddRange(new List<Execution>()
                    {
                        new Execution()
                        {
                            AssignmentId = 1,
                            EmployeeId = 1
                        },
                        new Execution()
                        {
                            AssignmentId = 2,
                            EmployeeId = 2
                        },
                        new Execution()
                        {
                            AssignmentId = 3,
                            EmployeeId = 1
                        },
                        new Execution()
                        {
                            AssignmentId = 4,
                            EmployeeId = 2
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Material.Any())
                {
                    context.Material.AddRange(new List<Material>()
                    {
                        new Material()
                        {
                            MaterialDictionaryId = 1,
                            Quantity = 10
                        },
                        new Material()
                        {
                            MaterialDictionaryId = 2,
                            Quantity = 20
                        },
                        new Material()
                        {
                            MaterialDictionaryId = 3,
                            Quantity = 30
                        },
                        new Material()
                        {
                            MaterialDictionaryId = 4,
                            Quantity = 40
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Equipment.Any())
                {
                    context.Equipment.AddRange(new List<Equipment>()
                    {
                        new Equipment()
                        {
                            EquipmentDictionaryId = 1
                        },
                        new Equipment()
                        {
                            EquipmentDictionaryId = 2
                        },
                        new Equipment()
                        {
                            EquipmentDictionaryId = 3
                        },
                        new Equipment()
                        {
                            EquipmentDictionaryId = 4
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.EquipmentHistory.Any())
                {
                    context.EquipmentHistory.AddRange(new List<EquipmentHistory>
                    {
                        new EquipmentHistory()
                        {
                            EquipmentId = 1,
                            EquipmentStatus = EquipmentStatus.New,
                            StatusStart = DateTime.Now.AddDays(-7),
                            StatusEnd = DateTime.Now.AddDays(10)
                        },
                        new EquipmentHistory()
                        {
                            EquipmentId = 1,
                            EquipmentStatus = EquipmentStatus.Reserved,
                            StatusStart = DateTime.Now.AddDays(10),
                            StatusEnd = null
                        },
                        new EquipmentHistory()
                        {
                            EquipmentId = 2,
                            EquipmentStatus = EquipmentStatus.OutOfService,
                            StatusStart = DateTime.Now.AddDays(8),
                            StatusEnd = DateTime.Now.AddDays(9)
                        },
                        new EquipmentHistory()
                        {
                            EquipmentId = 3,
                            EquipmentStatus = EquipmentStatus.Working,
                            StatusStart = DateTime.Now.AddDays(1),
                            StatusEnd = DateTime.Now.AddDays(2)
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Consumption.Any())
                {
                    context.Consumption.AddRange(new List<Consumption>()
                    {
                        new Consumption()
                        {
                            AssignmentId = 1,
                            MaterialId = 1,
                            Quantity = 5,
                            CreatedAt = DateTime.Now
                        },
                        new Consumption()
                        {
                            AssignmentId = 2,
                            MaterialId = 2,
                            Quantity = 10,
                            CreatedAt = DateTime.Now.AddDays(5)
                        },
                        new Consumption()
                        {
                            AssignmentId = 3,
                            MaterialId = 3,
                            Quantity = 15,
                            CreatedAt = DateTime.Now.AddDays(-5)
                        },
                        new Consumption()
                        {
                            AssignmentId = 4,
                            MaterialId = 4,
                            Quantity = 20,
                            CreatedAt = DateTime.Now.AddDays(2)
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Order.Any())
                {
                    var supplierId = context.Supplier.First().SupplierId;

                    context.Order.Add(new Order() { ContractNumber = "ABC123", SupplierId = supplierId });
                    context.Order.Add(new Order() { ContractNumber = "DEF456", SupplierId = supplierId + 1 });
                    context.Order.Add(new Order() { ContractNumber = "GHI789", SupplierId = supplierId + 2 });
                    context.SaveChanges();
                }

                if (!context.OrderHistory.Any())
                {
                    context.OrderHistory.AddRange(new List<OrderHistory>()
                    {
                        new OrderHistory()
                        {
                            OrderId = 1,
                            OrderStatus = OrderStatus.Created,
                            CreatedAt = DateTime.Now
                        },
                        new OrderHistory()
                        {
                            OrderId = 2,
                            OrderStatus = OrderStatus.Shipped,
                            CreatedAt = DateTime.Now.AddDays(-1)
                        },
                        new OrderHistory()
                        {
                            OrderId = 3,
                            OrderStatus = OrderStatus.Delivered,
                            CreatedAt = DateTime.Now.AddDays(-2)
                        },
                        new OrderHistory()
                        {
                            OrderId = 2,
                            OrderStatus = OrderStatus.Cancelled,
                            CreatedAt = DateTime.Now.AddDays(-3)
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Inventory.Any())
                {
                    context.Inventory.AddRange(new List<Inventory>()
                    {
                        new Inventory()
                        {
                            MaterialId = 1,
                            EquipmentId = null,
                            LocationId = 1
                        },
                        new Inventory()
                        {
                            MaterialId = 2,
                            EquipmentId = null,
                            LocationId = 2
                        },
                        new Inventory()
                        {
                            MaterialId = null,
                            EquipmentId = 1,
                            LocationId = 2
                        },
                        new Inventory()
                        {
                            MaterialId = null,
                            EquipmentId = 2,
                            LocationId = 1
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.OrderItem.Any())
                {
                    context.OrderItem.AddRange(new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            OrderId = 1,
                            InventoryId = 1,
                            Cost = 100,
                            QualityCheckingGrade = QualityCheckingGrade.Approved
                        },
                        new OrderItem()
                        {
                            OrderId = 2,
                            InventoryId = 2,
                            Cost = 200,
                            QualityCheckingGrade = QualityCheckingGrade.Approved
                        },
                        new OrderItem()
                        {
                            OrderId = 3,
                            InventoryId = 3,
                            Cost = 300,
                            QualityCheckingGrade = QualityCheckingGrade.Declined
                        },
                        new OrderItem()
                        {
                            OrderId = 2,
                            InventoryId = 4,
                            Cost = 400,
                            QualityCheckingGrade = null
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Transportation.Any())
                {
                    var locationAId = context.Location.First().LocationId;
                    var locationBId = context.Location.First().LocationId;

                    context.Transportation.Add(new Transportation() { PlanDepartureDateTime = DateTime.Now, PlanArrivalDateTime = DateTime.Now.AddDays(1), OriginId = locationAId, DestinationId = locationBId });
                    context.Transportation.Add(new Transportation() { PlanDepartureDateTime = DateTime.Now, PlanArrivalDateTime = DateTime.Now.AddDays(2), OriginId = locationBId, DestinationId = locationAId });
                    context.SaveChanges();
                }

                if (!context.InventoryTransportation.Any())
                {
                    var inventoryId = context.Inventory.First().InventoryId;
                    var transportationId = context.Transportation.First().TransportationId;

                    context.InventoryTransportation.Add(new InventoryTransportation() { InventoryId = inventoryId, TransportationId = transportationId });
                    context.InventoryTransportation.Add(new InventoryTransportation() { InventoryId = inventoryId + 2, TransportationId = transportationId + 1 });
                    context.InventoryTransportation.Add(new InventoryTransportation() { InventoryId = inventoryId + 1, TransportationId = transportationId });
                    context.SaveChanges();
                }

                if (!context.TransportationHistory.Any())
                {
                    context.TransportationHistory.AddRange(new List<TransportationHistory>()
                    {
                        new TransportationHistory()
                        {
                            TransportationId = 1,
                            TransportationStatus = TransportationStatus.InTransit,
                            CreatedAt = DateTime.Now
                        },
                        new TransportationHistory()
                        {
                            TransportationId = 2,
                            TransportationStatus = TransportationStatus.Planned,
                            CreatedAt = DateTime.Now.AddDays(-1)
                        },
                        new TransportationHistory()
                        {
                            TransportationId = 2,
                            TransportationStatus = TransportationStatus.Cancelled,
                            CreatedAt = DateTime.Now.AddDays(-2)
                        },
                        new TransportationHistory()
                        {
                            TransportationId = 1,
                            TransportationStatus = TransportationStatus.Delivered,
                            CreatedAt = DateTime.Now.AddDays(-3)
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.InventoryTransportationHistory.Any())
                {
                    context.InventoryTransportationHistory.AddRange(new List<InventoryTransportationHistory>()
                    {
                        new InventoryTransportationHistory()
                        {
                            InventoryTransportationId = 1,
                            InventoryTransportationStatus = InventoryTransportationStatus.Received,
                            CreatedAt = DateTime.Now
                        },
                        new InventoryTransportationHistory()
                        {
                            InventoryTransportationId = 2,
                            InventoryTransportationStatus = InventoryTransportationStatus.Received,
                            CreatedAt = DateTime.Now.AddDays(-1)
                        },
                        new InventoryTransportationHistory()
                        {
                            InventoryTransportationId = 3,
                            InventoryTransportationStatus = InventoryTransportationStatus.Lost,
                            CreatedAt = DateTime.Now.AddDays(-2)
                        },
                        new InventoryTransportationHistory()
                        {
                            InventoryTransportationId = 3,
                            InventoryTransportationStatus = InventoryTransportationStatus.Lost,
                            CreatedAt = DateTime.Now.AddDays(-3)
                        }
                    });
                    context.SaveChanges();
                }

            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                if (!await roleManager.RoleExistsAsync(UserRoles.ProjectManager))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.ProjectManager));
                if (!await roleManager.RoleExistsAsync(UserRoles.Engineer))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Engineer));
                if (!await roleManager.RoleExistsAsync(UserRoles.PurchasingManager))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.PurchasingManager));
                if (!await roleManager.RoleExistsAsync(UserRoles.QualityDepartmentSpecialist))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.QualityDepartmentSpecialist));
                if (!await roleManager.RoleExistsAsync(UserRoles.ProductionWorker))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.ProductionWorker));
                if (!await roleManager.RoleExistsAsync(UserRoles.SupplyDepartmentWorker))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.SupplyDepartmentWorker));
                if (!await roleManager.RoleExistsAsync(UserRoles.LogisticsManager))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.LogisticsManager));
                if (!await roleManager.RoleExistsAsync(UserRoles.Driver))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Driver));
                if (!await roleManager.RoleExistsAsync(UserRoles.Repair))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Repair));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "developer@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "developer",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Employee = new Employee()
                        {
                            ELastName = "Тестов",
                            EFirstName = "Мотест",
                            EPatronymic = "Модестович",
                            BirthDate = DateTime.Now.AddYears(-30),
                            PositionId = 2,
                            Email = "hahahehe@example.com",
                            PhoneNumber = "+7516151651"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Employee = new Employee()
                        {
                            ELastName = "Тестов",
                            EFirstName = "Мотест",
                            EPatronymic = "Модестович",
                            BirthDate = DateTime.Now.AddYears(-30),
                            PositionId = 2,
                            Email = "hahahehe@example.com",
                            PhoneNumber = "+7516151651"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}