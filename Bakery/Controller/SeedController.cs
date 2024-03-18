using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using Bakery.Data;
using Microsoft.EntityFrameworkCore;
using Bakery.Models;


namespace Bakery.Controller
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [ApiController]
    public class SeedController:ControllerBase
    {
         private readonly MyDbContext _context;
         private IWebHostEnvironment _environment;
        
        public SeedController(
            MyDbContext context,
            IWebHostEnvironment environment )
        {
            _context = context;
            _environment = environment;
        }

        [HttpPut(Name = "Seed")]
        [ResponseCache(NoStore = true)]

        public async Task<IActionResult> Put()
        {
             var order = new List<Order>
            {
                new Order { DeliveryPlace = "Finlandsgade 17", DeliveryDate = new DateTime(2024, 08, 12, 08, 00, 00) },
                new Order { DeliveryPlace = "Katrinebjergvej 2", DeliveryDate = new DateTime(2024, 10, 11, 10, 00, 00) }
            };

            var package = new List<Package>
            {
                new Package { Order = order.FirstOrDefault(p => p.OrderId == 1) },
                new Package { Order = order.FirstOrDefault(p => p.OrderId == 1) }
            };

            var ingredients = new List<Ingredient>
            {
                new Ingredient { IngredientName = "Leftover cake" },
                new Ingredient { IngredientName = "Raspberry jam" },
                new Ingredient { IngredientName = "Cocoa" },
                new Ingredient { IngredientName = "Rum" },
                new Ingredient { IngredientName = "coconut flakes" },
                new Ingredient { IngredientName = "Sugar" },
                new Ingredient { IngredientName = "Flour" },
                new Ingredient { IngredientName = "Salt" }

            };

            var stock = new List<Stock>
            {
                new Stock { Quantity = 100000, Ingredient = ingredients.FirstOrDefault(i => i.IngredientId == 6) },
                new Stock { Quantity = 242000, Ingredient = ingredients.FirstOrDefault(i => i.IngredientId == 7) },
                new Stock { Quantity = 10000, Ingredient = ingredients.FirstOrDefault(i => i.IngredientId == 8) }
            };

            var listOfBakingGoods = new List<ListOfBakingGoods>
            {
                new ListOfBakingGoods
                    { Order = order.FirstOrDefault(l => l.OrderId == 1), Quantity = 300, Type = "Alexandertorte" },
                new ListOfBakingGoods
                    { Order = order.FirstOrDefault(l => l.OrderId == 1), Quantity = 100, Type = "Buttercookies" },
                new ListOfBakingGoods
                    { Order = order.FirstOrDefault(l => l.OrderId == 1), Quantity = 100, Type = "Studenterbrød" },
                new ListOfBakingGoods
                    { Order = order.FirstOrDefault(l => l.OrderId == 1), Quantity = 200, Type = "Romkugler" },
                new ListOfBakingGoods
                    { Order = order.FirstOrDefault(l => l.OrderId == 2), Quantity = 2900, Type = "Alexandertorte" },
                new ListOfBakingGoods
                    { Order = order.FirstOrDefault(l => l.OrderId == 2), Quantity = 1499, Type = "Buttercookies" },
                new ListOfBakingGoods
                    { Order = order.FirstOrDefault(l => l.OrderId == 2), Quantity = 800, Type = "Studenterbrød" },
                new ListOfBakingGoods
                    { Order = order.FirstOrDefault(l => l.OrderId == 2), Quantity = 1800, Type = "Romkugler" }
            };

            var batch = new List<Batch>
            {
                new Batch
                {
                    StartTime = new DateTime(2024, 06, 03, 10, 30, 00),
                    FinishTime = new DateTime(2024, 06, 03, 14, 30, 00),
                    TargetFinishTime = new DateTime(2024, 06, 03, 14, 20, 00)
                },

                new Batch
                {
                    StartTime = new DateTime(2024, 07, 10, 12, 10, 00),
                    FinishTime = new DateTime(2024, 07, 10, 15, 30, 00),
                    TargetFinishTime = new DateTime(2024, 07, 10, 15, 00, 00)
                },

                new Batch
                {
                    StartTime = new DateTime(2024, 03, 01, 08, 00, 00),
                    FinishTime = new DateTime(2024, 03, 01, 09, 30, 00),
                    TargetFinishTime = new DateTime(2024, 03, 01, 09, 10, 00),
                }

            };


            var batchIngredient = new List<BatchIngredient>
            {
                new BatchIngredient
                {
                    BatchId = batch.FirstOrDefault(b => b.BatchId == 1).BatchId,
                    IngredientsId = ingredients.FirstOrDefault(i => i.IngredientId == 1).IngredientId,
                    Batch = batch.FirstOrDefault(b => b.BatchId == 1),
                    Ingredients = ingredients.FirstOrDefault(i => i.IngredientId == 1),
                    Quantity = 50000
                },
               
                new BatchIngredient
                {
                    BatchId = batch.FirstOrDefault(b => b.BatchId == 1).BatchId,
                    IngredientsId = ingredients.FirstOrDefault(i => i.IngredientId == 1).IngredientId,
                    Batch = batch.FirstOrDefault(b => b.BatchId == 2), Quantity = 30,
                    Ingredients = ingredients.FirstOrDefault(i => i.IngredientId == 2)
                },
                new BatchIngredient
                {
                    BatchId = batch.FirstOrDefault(b => b.BatchId == 1).BatchId,
                    IngredientsId = ingredients.FirstOrDefault(i => i.IngredientId == 1).IngredientId,
                    Batch = batch.FirstOrDefault(b => b.BatchId == 2), Quantity = 20,
                    Ingredients = ingredients.FirstOrDefault(i => i.IngredientId == 3)
                },
                new BatchIngredient
                {
                    BatchId = batch.FirstOrDefault(b => b.BatchId == 1).BatchId,
                    IngredientsId = ingredients.FirstOrDefault(i => i.IngredientId == 1).IngredientId,
                    Batch = batch.FirstOrDefault(b => b.BatchId == 2), Quantity = 30,
                    Ingredients = ingredients.FirstOrDefault(i => i.IngredientId == 4)
                },
                new BatchIngredient
                {
                    BatchId = batch.FirstOrDefault(b => b.BatchId == 1).BatchId,
                    IngredientsId = ingredients.FirstOrDefault(i => i.IngredientId == 1).IngredientId,
                    Batch = batch.FirstOrDefault(b => b.BatchId == 2), Quantity = 0,
                    Ingredients = ingredients.FirstOrDefault(i => i.IngredientId == 5)
                }
            };

            foreach (var o in order)
            {
                _context.Orders.Add(o);
            }

            foreach (var p in package)
            {
                _context.Packages.Add(p);
            }
            
            foreach (var l in listOfBakingGoods)
            {
                _context.ListOfBakingGoods.Add(l);
            }
            
            foreach (var b in batch)
            {
                _context.Batch.Add(b);
            }

            foreach (var s in stock)
            {
                _context.Stocks.Add(s);
            }

            foreach (var i in ingredients)
            {
                _context.Ingredients.Add(i);
            }
            
            foreach (var bi in batchIngredient)
            {
                _context.BatchIngredient.Add(bi);
            }

            _context.SaveChanges();
            
            return new JsonResult(new
            {
                Package = _context.Packages.Count(),
                Order = _context.Orders.Count(),
                batchIngredient = _context.BatchIngredient.Count(),
                batch = _context.Batch.Count(),
                ingredients = _context.Ingredients.Count(),
                stock = _context.Stocks.Count(),
                listOfBakingGoods = _context.ListOfBakingGoods.Count()
                
                
            });


        }

         
    }
}
