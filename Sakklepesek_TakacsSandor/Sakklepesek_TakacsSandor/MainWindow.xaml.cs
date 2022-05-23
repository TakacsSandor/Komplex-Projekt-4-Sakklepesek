using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sakklepesek_TakacsSandor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int tablaMeret = 8;
        Button[,] mezok;
        public MainWindow()
        {
            InitializeComponent();
            FeluletKialakitasa();
        }

        private void FeluletKialakitasa()
        {
            tabla.RowDefinitions.Clear();
            tabla.ColumnDefinitions.Clear();
            for (int i = 0; i < tablaMeret; i++)
            {
                tabla.RowDefinitions.Add(new RowDefinition());
                tabla.ColumnDefinitions.Add(new ColumnDefinition());
            }
            tabla.Children.Clear();
            mezok = new Button[tablaMeret, tablaMeret];
            for (int i = 0; i < tablaMeret; i++)
            {
                for (int j = 0; j < tablaMeret; j++)
                {
                    mezok[i, j] = new Button();
                    tabla.Children.Add(mezok[i, j]);
                    Grid.SetRow(mezok[i, j], i);
                    Grid.SetColumn(mezok[i, j], j);
                }
            }
        }
    }
}
