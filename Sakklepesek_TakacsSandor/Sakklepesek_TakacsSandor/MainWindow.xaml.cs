﻿using System;
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
        string[] oszlopJelek = { "A", "B", "C", "D", "E", "F", "G", "H" };
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
                    mezok[i, j].Click += KoordinataValasztas;
                    Grid.SetRow(mezok[i, j], i);
                    Grid.SetColumn(mezok[i, j], j);
                }
            }

            for (int i = 0; i < tablaMeret; i++)
            {
                for (int j = 0; j < tablaMeret; j++)
                {
                    if (j % 2 == 1 && i % 2 == 1)
                    {
                        mezok[i, j].Background = Brushes.White;
                    }
                    else if (j % 2 == 0 && i % 2 == 0)
                    {
                        mezok[i, j].Background = Brushes.White;
                    }
                    else
                    {
                        mezok[i, j].Background = Brushes.Black;
                    }
                }
            }
        }

        private int[] Holvan(Button gomb)
        {
            int[] koordinatak = { -1, -1 };
            for (int i = tablaMeret - 1; i >= 0; i--)
            {
                for (int j = tablaMeret - 1; j >= 0; j--)
                {
                    if (mezok[i,j].Equals(gomb))
                    {
                        i = 7 - i;
                        koordinatak[0] = i;
                        koordinatak[1] = j;
                        return koordinatak;
                    }
                }
            }
            return koordinatak;
        }

        private void KoordinataValasztas(object sender, RoutedEventArgs e)
        {
            Button aktualis = sender as Button;
            int x = Holvan(aktualis)[0];
            int y = Holvan(aktualis)[1];
            x = 8 - x;
            katintott_pozicio.Text = oszlopJelek[y] + x.ToString();
            if (kijelolt_figura.Text.Length == 0)
            {
                MessageBox.Show("Válassz figurát");
            }
            else
            {

            }
        }

        private void Forgatas(int x, int y)
        {
            if (mezok[x, y].Background != Brushes.Black)
                mezok[x, y].Background = Brushes.LightBlue;
            else
                mezok[x, y].Background = Brushes.LightBlue;
        }
                
        private void figura_kivalasztas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (figura_kivalasztas.SelectedItem == vilagos_vezer)
            {
                kijelolt_figura.Text = "Világos vezér";
            }
            else if (figura_kivalasztas.SelectedItem == vilagos_kiraly)
            {
                kijelolt_figura.Text = "Világos király";
            }
            else if (figura_kivalasztas.SelectedItem == vilagos_futo)
            {
                kijelolt_figura.Text = "Világos futó";
            }
            else if (figura_kivalasztas.SelectedItem == vilagos_huszar)
            {
                kijelolt_figura.Text = "Világos huszár";
            }
            else if (figura_kivalasztas.SelectedItem == vilagos_bastya)
            {
                kijelolt_figura.Text = "Világos bástya";
            }
            else if (figura_kivalasztas.SelectedItem == vilagos_gyalog)
            {
                kijelolt_figura.Text = "Világos gyalog";
            }
            else if (figura_kivalasztas.SelectedItem == sotet_gyalog)
            {
                kijelolt_figura.Text = "Sötét gyalog";
            }
        }
    }
}
