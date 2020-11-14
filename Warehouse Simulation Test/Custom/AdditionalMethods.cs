using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Warehouse_Simulation_Test.Warehouse;

namespace Warehouse_Simulation_Test.Custom
{
    public class AdditionalMethods
    {
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
                var @char = (char) random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public static Warehouse.Warehouse ParseCustomWarehouse(string list, string separator, bool isRotary)
        {
            var array = Regex.Split(list, separator);
            var warehouse = new Warehouse.Warehouse(array.Length, isRotary);

            foreach (var item in array.Select((value, index) => new {index, value}))
                warehouse.AddItemToList(item.index,
                    item.value == "T" ? new Item(RandomString(10), 1) : new Item(string.Empty, 0));

            return warehouse;
        }
    }
}