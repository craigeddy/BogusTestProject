using System;
using System.Linq;
using Bogus;

namespace BogusApp
{
    class Program
    {
        static void Main()
        {
            var random = new Randomizer();
            var lorem = new Bogus.DataSets.Lorem();
            var addresses = new Bogus.DataSets.Address();

            var order = new Order()
            {
                OrderId = random.Number(1, 100),
                Item = string.Join(" ", lorem.Letter(100)),
                Quantity = random.Number(1, 10),
                ZipCode = addresses.ZipCode()
            };

            order.Dump();

            Console.ReadLine();
            var fruit = new[] { "apple", "banana", "orange", "strawberry", "kiwi", "grapefruit"  };

            var testOrders = new Faker<Order>()
                //Ensure all properties have rules. By default, StrictMode is false
                //Set a global policy by using Faker.DefaultStrictMode
                .StrictMode(true)
                //OrderId is deterministic
                .RuleFor(o => o.OrderId, f => f.Random.Number(1,100))
                //Pick some fruit from a basket
                .RuleFor(o => o.Item, f => f.PickRandom(fruit))
                //A random quantity from 1 to 10
                .RuleFor(o => o.Quantity, f => f.Random.Number(1, 10))
                .RuleFor(o => o.ZipCode, f => f.Address.ZipCode());

            testOrders.Generate(3).ToList().ForEach(o => o.Dump());
            Console.ReadLine();
        }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }

        public string ZipCode { get; set; }
        public void Dump()
        {
            Console.Write($"{OrderId}: {Item} (Qty = {Quantity} shipping to {ZipCode}){Environment.NewLine}");
        }
    }
}
