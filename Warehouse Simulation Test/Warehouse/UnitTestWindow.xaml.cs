using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace Warehouse_Simulation_Test.Warehouse
{
    /// <summary>
    /// Logica di interazione per UnitTestWindow.xaml
    /// </summary>
    public partial class UnitTestWindow : MetroWindow
    {
        private readonly MainWindow mainWindow;

        private bool selectedWarehouse = true;

        public UnitTestWindow(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }

        private void SelectionButton_OnClick(object sender, RoutedEventArgs e)
        {
            var Button = sender as Button;

            selectedWarehouse = Button.Name == "LinearButton";
        }

        private void WarehouseReset(object sender, RoutedEventArgs e)
        {
            if (selectedWarehouse)
            {
                mainWindow.linearWarehouse = Warehouse.RandomPopulateWarehouse(int.Parse(WarehouseSize.Text), false);
                mainWindow.LinearDataGrid.ItemsSource = mainWindow.linearWarehouse.WarehouseSlots;
            }
            else
            {
                mainWindow.rotatingWarehouse = Warehouse.RandomPopulateWarehouse(int.Parse(WarehouseSize.Text), true);
                mainWindow.RotatingDataGrid.ItemsSource = mainWindow.rotatingWarehouse.WarehouseSlots;
            }
        }

        private void TestSpaceButton_Click(object sender, RoutedEventArgs e)
        {
            var warehouse = selectedWarehouse ? mainWindow.linearWarehouse : mainWindow.rotatingWarehouse;

            var minimumSize = int.Parse(MinSize.Text);
            var maxSize = int.Parse(MaxSize.Text);

            ResultsBox.Text = string.Empty;

            for (var i = minimumSize; i <= maxSize; i++)
            {
                var sizeTestResult = warehouse.FindFreePlace(new Item("", i));

                if (sizeTestResult.Equals(-1)) ResultsBox.Text += $"Item of size {i} is too big for current warehouse!\n";
                else ResultsBox.Text += $"Item of size {i} can be placed at position {sizeTestResult}!\n";
            }
        }
    }
}
