using System.Windows;
using MahApps.Metro.Controls;
using Warehouse_Simulation_Test.Warehouse;

namespace Warehouse_Simulation_Test
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public Warehouse.Warehouse LinearWarehouse;
        public Warehouse.Warehouse RotatingWarehouse;

        public MainWindow()
        {
            InitializeComponent();

            LinearWarehouse = Warehouse.Warehouse.RandomPopulateWarehouse(12, false);
            RotatingWarehouse = Warehouse.Warehouse.RandomPopulateWarehouse(12, true);

            //var places = new[] {false, false, false, true, true, false, false, false, false, true, false, false};

            /* MessageBox.Show(Warehouse.FindFreePlace(places, false, 6).ToString());
             MessageBox.Show(Warehouse.FindFreePlace(places, false, 4).ToString());
             MessageBox.Show(Warehouse.FindFreePlace(places, false, 5).ToString());
             MessageBox.Show(Warehouse.FindFreePlace(places, true, 5).ToString());*/
        }

        private void SelectWarehouseType(object sender, HamburgerMenuItemInvokedEventArgs args)
        {
            var selectedItem = args.InvokedItem as HamburgerMenuItem;

            switch (selectedItem?.Label)
            {
                case "Linear":
                    LinearGrid.Visibility = Visibility.Visible;
                    RotatingGrid.Visibility = Visibility.Hidden;
                    break;

                case "Rotating":
                    LinearGrid.Visibility = Visibility.Hidden;
                    RotatingGrid.Visibility = Visibility.Visible;
                    break;
            }

            HamburgerMenuControl.IsPaneOpen = false;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LinearDataGrid.ItemsSource = LinearWarehouse.WarehouseSlots;
            RotatingDataGrid.ItemsSource = RotatingWarehouse.WarehouseSlots;
        }

        private void OpenTestWindow(object sender, RoutedEventArgs e)
        {
            var window = new UnitTestWindow(this);

            window.Show();
        }
    }
}