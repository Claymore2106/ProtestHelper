using ProtestHelperBackend.Networking;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProtestHelperBackend
{
    class Program
    {
        static void Main(string[] args)
        {
            //var item1 = AddItemRequest(new Item { ItemName = "Cone", Count = 2 });
            //var item2 = AddItemRequest(new Item { ItemName = "Water", Count = 10 });

            //RemoveItemRequest(item1);

            //var item3 = AddItemRequest(new Item { ItemName = "Mask", Count = 5 });

            Thread t = new Thread(delegate ()
            {
                // replace the IP with your system IP Address...
                Server myserver = new Server("10.11.5.101", 11235);
            });
            t.Start();

            Console.WriteLine("Server Started...!");
        }

        public static Item AddItemRequest(Item item)
        {
            var db = new RequestsDb();

            db.Database.Log = Console.WriteLine;

            db.Items.Add(item);
            db.SaveChanges();
            return item;
        }

        public static void RemoveItemRequest(Item item)
        {
            var db = new RequestsDb();

            db.Items.Attach(item);
            db.Items.Remove(item);
            db.SaveChanges();
        }
    }

    public class Item
    {
        public override string ToString()
        {
            return $"ItemId : {ItemId}, ItemName : {ItemName}, Count : {Count}";
        }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Count { get; set; }

    }

    public class Medical
    {
        public int MedicalId { get; set; }
    }
}
