namespace Warehouse_Simulation_Test.Warehouse
{
    public class Item
    {
        public string Name { get; }
        public int Size { get; }   //Size equals to the number of occupied slots in magazine

        public Item(string name, int size)
        {
            Name = name;
            Size = size;
        }
    }
}
