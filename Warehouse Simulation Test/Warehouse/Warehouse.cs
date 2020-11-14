using System;
using System.Text;
using System.Windows;

namespace Warehouse_Simulation_Test.Warehouse
{
    public class Warehouse
    {
        public Item[] WarehouseSlots;
        public bool IsRotary;

        public Warehouse(int size, bool isRotary)
        {
            if (size.Equals(0))
            {
                MessageBox.Show("Warehouse size is 0! This is not allowed! Please assign the correct warehouse size!");
                Application.Current.Shutdown();
            }
            WarehouseSlots = new Item[size];

            this.IsRotary = isRotary;
        }

        public void AddItemToList(int position, Item item)
        {
            if (WarehouseSlots == null)
            {
                MessageBox.Show("Warehouse is not initialized!");
                Application.Current.Shutdown();
            }

            if (item.Size.Equals(0)) WarehouseSlots[position] = item;

            else for (var i = 0; i < item.Size; i++, position++)
                {
                    WarehouseSlots[position] = item;
                }
        }

        public int FindFreePlace(Item item)
        {
            var count = 0;

            var iterationsCount = !IsRotary ? 0 : 1;
            var index = 0;

            for (var i = 0; i <= iterationsCount; i++)
            {
                foreach (var items in WarehouseSlots)
                {
                    if (count == item.Size) return (index - item.Size) + 1;
                    if (items.Size.Equals(0) || items.Name == "Empty") count++;
                    else count = 0;
                    index++;
                }
            }
            return -1;
        }

        public static int FindFreePlace(bool[] places, bool isRotary, int neededPlaces)
        {
            var count = 0;

            var iterationsCount = !isRotary ? 0 : 1;

            var index = 0;

            for (var i = 0; i <= iterationsCount; i++)
            {
                foreach (var item in places)
                {
                    if (!item) count++;
                    else count = 0;
                    if (count == neededPlaces) return (index - neededPlaces + 1);
                    index++;
                }
            }
            return -1;
        }

        public static Warehouse RandomPopulateWarehouse(int numberOfItems, bool isRotary)
        {
            var random = new Random();

            var warehouse = new Warehouse(numberOfItems, isRotary);

            for (var i = 0; i < numberOfItems; i++)
            {
                if (random.Next(0, 10) > 1)
                {
                    warehouse.AddItemToList(i, new Item("Empty", 0));
                    continue;
                }

                warehouse.AddItemToList(i, new Item(RandomString(10), 1));
            }

            return warehouse;
        }

        public static string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            var random = new Random();

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            var offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
