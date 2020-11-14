using System.Windows;
using Warehouse_Simulation_Test.Custom;

namespace Warehouse_Simulation_Test
{
    public partial class CustomWarehouse
    {
        private readonly MainWindow _mainWindow;

        public CustomWarehouse(MainWindow window)
        {
            InitializeComponent();
            _mainWindow = window;
        }

        private void WarehouseResetButton_Click(object sender, RoutedEventArgs e)
        {
            var grid = _mainWindow.LinearGrid.IsVisible;
            var warehouse =
                AdditionalMethods.ParseCustomWarehouse(WarehouseTextBox.Text, ";", !grid);

            if (grid)
            {
                _mainWindow.LinearWarehouse = warehouse;
                _mainWindow.LinearDataGrid.ItemsSource = _mainWindow.LinearWarehouse.WarehouseSlots;
            }

            else
            {
                _mainWindow.RotatingWarehouse = warehouse;
                _mainWindow.RotatingDataGrid.ItemsSource = _mainWindow.RotatingWarehouse.WarehouseSlots;
            }
        }
    }
}