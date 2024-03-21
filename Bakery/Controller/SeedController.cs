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
             var order = new Order[]
            {
                new Order
                {
                    DeliveryPlace = "Finlandsgade 17", 
                    DeliveryDate = "12082024 0800",
                    GPSCoordinates = "56.17202054, 10.19114428"
                },
                new Order { DeliveryPlace = "Katrinebjergvej 2", DeliveryDate = "11102024 1000", GPSCoordinates = "35.464646, 10.432566"}
            };

            var package = new Package[]
            {
                new Package { Order = order[0]},
                new Package { Order = order[0] }
            };
            

            var ingredients = new Ingredient[]
            {
                new Ingredient { IngredientName = "Leftover cake", StockQuantity = 5000, Allergens = "Gluten"},
                new Ingredient { IngredientName = "Raspberry jam" , StockQuantity = 10, Allergens = "Berries"},
                new Ingredient { IngredientName = "Cocoa", StockQuantity = 200, Allergens = "Koffein"},
                new Ingredient { IngredientName = "Rum", StockQuantity = 123, Allergens = "Alkohol"},
                new Ingredient { IngredientName = "coconut flakes", StockQuantity = 400, Allergens = "Nuts"},
                new Ingredient { IngredientName = "Sugar" , StockQuantity = 100000, Allergens = "Nuts"},
                new Ingredient { IngredientName = "Flour" , StockQuantity = 242000, Allergens = "Flour"},
                new Ingredient { IngredientName = "Salt", StockQuantity =10000, Allergens = "Lactose"}

            };

            var bakingGood = new BakingGood[]
            {
                new BakingGood
                    { Quantity = 300, BakingGoodName = "Alexandertorte" },
                new BakingGood
                    { Quantity = 100, BakingGoodName = "Buttercookies" },
                new BakingGood
                    { Quantity = 100, BakingGoodName = "Studenterbrød" },
                new BakingGood
                    { Quantity = 200, BakingGoodName = "Romkugler" },
                new BakingGood
                    { Quantity = 2900, BakingGoodName = "Alexandertorte" },
                new BakingGood
                    { Quantity = 1499, BakingGoodName = "Buttercookies" },
                new BakingGood
                    { Quantity = 800, BakingGoodName = "Studenterbrød" },
                new BakingGood
                    { Quantity = 1800, BakingGoodName = "Romkugler" }
            };
            
            var bakingGoodOrder = new BakingGoodOrder[]
            {
                new BakingGoodOrder { Order = order[0], BakingGoods = bakingGood[0] },
                new BakingGoodOrder { Order = order[0], BakingGoods = bakingGood[1] },
                new BakingGoodOrder { Order = order[0], BakingGoods = bakingGood[2] },
                new BakingGoodOrder { Order = order[0], BakingGoods = bakingGood[3] },
                new BakingGoodOrder { Order = order[1], BakingGoods = bakingGood[4] },
                new BakingGoodOrder { Order = order[1], BakingGoods = bakingGood[5] },
                new BakingGoodOrder { Order = order[1], BakingGoods = bakingGood[6] },
                new BakingGoodOrder { Order = order[1], BakingGoods = bakingGood[7] },
            };

            var batch = new Batch[]
            {
                new Batch
                {
                    StartTime = new DateTime(2024, 06, 03, 10, 30, 00),
                    FinishTime = new DateTime(2024, 06, 03, 14, 30, 00),
                    TargetFinishTime = new DateTime(2024, 06, 03, 14, 20, 00),
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


            var batchIngredient = new BatchIngredient[]
            {
                new BatchIngredient
                {
                     Batch = batch[0],
                     Ingredients = ingredients[0],
                     Quantity = 50000
                },
               
                new BatchIngredient
                {
                    Batch = batch[0],
                    Ingredients = ingredients[1],
                    Quantity = 30
                },
                new BatchIngredient
                {
                    Batch = batch[0],
                    Ingredients = ingredients[2],
                    Quantity = 20
                },
                new BatchIngredient
                {
                    Batch = batch[0],
                    Ingredients = ingredients[3],
                    Quantity = 30
                },
                new BatchIngredient
                {
                    Batch = batch[0],
                    Ingredients = ingredients[4],
                    Quantity = 0
                }
            };
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                { 
                    _context.Ingredients.AddRange(ingredients);
                    _context.Batch.AddRange(batch);
                    _context.Orders.AddRange(order);
                    _context.Packages.AddRange(package);
                    _context.BakingGoods.AddRange(bakingGood); 
                    _context.BatchIngredient.AddRange(batchIngredient);
                    _context.BakingGoodOrders.AddRange(bakingGoodOrder);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            
            
            return new JsonResult(new
            {
                Package = _context.Packages.Count(),
                Order = _context.Orders.Count(),
                batch = _context.Batch.Count(),
                ingredients = _context.Ingredients.Count(),
                batchIngredient = _context.BatchIngredient.Count(),
                BakingGoods = _context.BakingGoods.Count()
                
                
            });


        }

         
    }
}
