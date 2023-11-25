using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Data.Entities.OrderManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();

            if (!context.Providers.Any())
            {
                context.Providers.AddRange(
                    new Provider
                    {
                        Name = "Главный поставщик",
                        Orders = new List<Order> {
                            new Order
                            {
                                Number = "1a",
                                Date = new DateTime(2023, 11, 15),
                                OrderItems = new List<OrderItem>
                                {
                                    new OrderItem
                                    {
                                        Name = "Предмет 7",
                                        Quantity = 10,
                                        Unit = "Штука"
                                    },
                                    new OrderItem
                                    {
                                        Name = "Предмет 8",
                                        Quantity = 20,
                                        Unit = "Штука"
                                    },
                                    new OrderItem
                                    {
                                        Name = "Предмет 9",
                                        Quantity = 25,
                                        Unit = "Штука"
                                    }
                                }
                            },
                            new Order
                            {
                                Number = "1b",
                                Date = new DateTime(2023, 11, 13),
                                OrderItems = new List<OrderItem>
                                {
                                    new OrderItem
                                    {
                                        Name = "Предмет 10",
                                        Quantity = 5,
                                        Unit = "Литр"
                                    },
                                    new OrderItem
                                    {
                                        Name = "Предмет 11",
                                        Quantity = 7.15m,
                                        Unit = "Литр"
                                    },
                                    new OrderItem
                                    {
                                        Name = "Предмет 12",
                                        Quantity = 18.4m,
                                        Unit = "Килограмм"
                                    }
                                }
                            }
                        }
                    },
                    new Provider
                    {
                        Name = "Поставщик 1",
                        Orders = new List<Order> {
                            new Order
                            {
                                Number = "2a",
                                Date = new DateTime(2023, 10, 10),
                                OrderItems = new List<OrderItem>
                                {
                                    new OrderItem
                                    {
                                        Name = "Предмет 1",
                                        Quantity = 150,
                                        Unit = "Метр"
                                    },
                                    new OrderItem
                                    {
                                        Name = "Предмет 2",
                                        Quantity = 175,
                                        Unit = "Метр"
                                    },
                                    new OrderItem
                                    {
                                        Name = "Предмет 3",
                                        Quantity = 1.152m,
                                        Unit = "Киломметр"
                                    }
                                }
                            },
                            new Order
                            {
                                Number = "2b",
                                Date = new DateTime(2023, 10, 15),
                                OrderItems = new List<OrderItem>
                                {
                                    new OrderItem
                                    {
                                        Name = "Предмет 4",
                                        Quantity = 4,
                                        Unit = "Упаковка"
                                    },
                                    new OrderItem
                                    {
                                        Name = "Предмет 5",
                                        Quantity = 11,
                                        Unit = "Флакон"
                                    },
                                    new OrderItem
                                    {
                                        Name = "Предмет 6",
                                        Quantity = 15,
                                        Unit = "Флакон"
                                    }
                                }
                            }
                        }
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
