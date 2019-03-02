using Store.Services;
using Store.Services.Interfaces;
using Store.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alt_Source_Test
{
    class Program
    {
        static IStoreService storeService = null;
        static void Main(string[] args)
        {
            storeService = new StoreService();
            Console.WriteLine("Buying: press 1 ");
            Console.WriteLine("Selling: press 2 ");
            Console.WriteLine("Exit: press 0 ");
            Console.Write("Please choose an action: ");
            var action = Console.ReadLine();
            while (action != "0")
            {
                if (action == "1")
                {
                    Buying();
                }
                else if (action == "2")
                {
                    Selling();
                }

                Console.Write("Please choose an action: ");
                action = Console.ReadLine();
            }



            Console.Read();
        }

        static void Buying()
        {
            Console.WriteLine("Please choose the clothes type : [T-Shirst: press 1, DressShirt press 2. To make Order: press 9 , Press 0 to exit] :");
            var type = Console.ReadLine();
            List<OrderItemData> items = new List<OrderItemData>();

            while (type != "0")
            {
                if (type == "9")
                {
                    storeService.MakeOrder(items);
                    Console.WriteLine("Order successfull");
                    break;
                }

                Console.Write("Please choose a price to buy:");
                var price = Console.ReadLine();

                Console.Write("Please choose amount:");
                var amount = Console.ReadLine();

                Console.Write("Please choose a Supplier: [Input supplier Id in [1-9]]");
                var supplier = Console.ReadLine();

                var item = new OrderItemData
                {
                    Amount = Convert.ToInt32(amount),
                    Price = Convert.ToDecimal(price),
                };

                if (int.TryParse(supplier, out int result))
                {
                    item.SupplierId = result;
                }

                if (type == "1")
                {
                    item.Type = Store.Common.Enums.ClothesType.TShirt;
                }
                else if (type == "2")
                {
                    item.Type = Store.Common.Enums.ClothesType.DressShirt;
                }

                items.Add(item);

                Console.WriteLine("================================");
                type = Console.ReadLine();
            }
        }

        static void DisplayClothesDetail()
        {
            var clothesDetails = storeService.GetClothesSellingPrice();
            Console.WriteLine("Selling price: ");

            Console.WriteLine("=====Type=====price: ");
            foreach (var clothesDetail in clothesDetails)
            {
                Console.WriteLine($"    {clothesDetail.Type.ToString()}    {clothesDetail.SellPrice}$");
            }

        }


        static void Selling()
        {
            DisplayClothesDetail();

            Console.WriteLine();
            Console.WriteLine("Do you want to update selling price: [Yes: press 1, No: press 0]");
            var yesNo = Console.ReadLine();
            if (yesNo == "0") return;

            Console.Write("Please choose the type to update price: [T-Shirst: press 1, DressShirt: press 2: To exit Press 0]: ");
            var type = Console.ReadLine();
            if (type != "0")
            {
                Console.Write("Please input new selling price:");
                var newPrice = Console.ReadLine();
                if (type == "1")
                {
                    storeService.UpdateSellingPrice(Store.Common.Enums.ClothesType.TShirt, Convert.ToDecimal(newPrice));
                }
                else if (type == "2")
                {
                    storeService.UpdateSellingPrice(Store.Common.Enums.ClothesType.DressShirt, Convert.ToDecimal(newPrice));
                }
                
            }

        }
    }
}
