using System;
using System.Windows;
using Warehouse_Simulation_Test.Custom;

namespace Warehouse_Simulation_Test.Warehouse
{
    public class Warehouse
    {
        public bool IsRotary;
        public Item[] WarehouseSlots;

        public Warehouse(int size, bool isRotary)
        {
            if (size.Equals(0))
            {
                MessageBox.Show("Warehouse size is 0! This is not allowed! Please assign the correct warehouse size!");
                Application.Current.Shutdown();
            }

            WarehouseSlots = new Item[size];

            IsRotary = isRotary;
        }

        public void AddItemToList(int position, Item item)
        {
            if (WarehouseSlots == null)
            {
                MessageBox.Show("Warehouse is not initialized!");
                Application.Current.Shutdown();
            }

            if (item.Size.Equals(0)) WarehouseSlots[position] = item;

            else
                for (var i = 0; i < item.Size; i++, position++)
                    WarehouseSlots[position] = item;
        }

        public int FindFreePlace(Item item)
        {
            var count = 0;

            var iterationsCount = !IsRotary ? 0 : 1;
            var index = 0;

            for (var i = 0; i <= iterationsCount; i++)
                foreach (var items in WarehouseSlots)
                {
                    if (count == item.Size) return index - item.Size + 1;
                    if (items.Size.Equals(0) || items.Name == string.Empty) count++;
                    else count = 0;
                    index++;
                }

            return -1;
        }

        public static int FindFreePlace(bool[] places, bool isRotary, int neededPlaces)
        {
            var count = 0;

            var iterationsCount = !isRotary ? 0 : 1;

            var index = 0;

            for (var i = 0; i <= iterationsCount; i++)
                foreach (var item in places)
                {
                    if (!item) count++;
                    else count = 0;
                    if (count == neededPlaces) return index - neededPlaces + 1;
                    index++;
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
                    warehouse.AddItemToList(i, new Item(string.Empty, 0));
                    continue;
                }

                warehouse.AddItemToList(i, new Item(AdditionalMethods.RandomString(10), 1));
            }

            return warehouse;
        }
    }
}