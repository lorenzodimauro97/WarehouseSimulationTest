using System.Windows;
using System.Windows.Controls;

namespace Warehouse_Simulation_Test.Warehouse
{
    public partial class UnitTestWindow
    {
        private readonly MainWindow _mainWindow;

        private bool _selectedWarehouse = true;

        public UnitTestWindow(MainWindow main)
        {
            InitializeComponent();
            _mainWindow = main;
        }

        private void SelectionButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            _selectedWarehouse = button?.Name == "LinearButton";
        }

        private void WarehouseReset(object sender, RoutedEventArgs e)
        {
            if (_selectedWarehouse)
            {
                _mainWindow.LinearWarehouse = Warehouse.RandomPopulateWarehouse(int.Parse(WarehouseSize.Text), false);
                _mainWindow.LinearDataGrid.ItemsSource = _mainWindow.LinearWarehouse.WarehouseSlots;
            }
            else
            {
                _mainWindow.RotatingWarehouse = Warehouse.RandomPopulateWarehouse(int.Parse(WarehouseSize.Text), true);
                _mainWindow.RotatingDataGrid.ItemsSource = _mainWindow.RotatingWarehouse.WarehouseSlots;
            }
        }

        private void TestSpaceButton_Click(object sender, RoutedEventArgs e)
        {
            var warehouse = _selectedWarehouse ? _mainWindow.LinearWarehouse : _mainWindow.RotatingWarehouse;

            var minimumSize = int.Parse(MinSize.Text);
            var maxSize = int.Parse(MaxSize.Text);

            ResultsBox.Text = string.Empty;

            for (var i = minimumSize; i <= maxSize; i++)
            {
                var sizeTestResult = warehouse.FindFreePlace(new Item("", i));

                if (sizeTestResult.Equals(-1))
                    ResultsBox.Text += $"Item of size {i} is too big for current warehouse!\n";
                else ResultsBox.Text += $"Item of size {i} can be placed at position {sizeTestResult}!\n";
            }
        }
    }
}