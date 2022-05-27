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
                    mezok[i, j].Click += GombokraKivalasztottFigura;
                    mezok[i, j].Click += Lepes_Lehetosegek_Beszinezese;
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
                    if (mezok[i, j].Equals(gomb))
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
        }

        private void GombokraKivalasztottFigura(object sender, RoutedEventArgs e)
        {
            Button aktualis = sender as Button;
            int x = Holvan(aktualis)[0];
            int y = Holvan(aktualis)[1];

            if (kijelolt_figura.Text == "Világos vezér")
            {
                aktualis.Content = new Image
                {
                    Source = new BitmapImage(new Uri("Kepek/kiralyno_feher.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center
                };
            }
            else if (kijelolt_figura.Text == "Világos király")
            {
                aktualis.Content = new Image
                {
                    Source = new BitmapImage(new Uri("Kepek/kiraly_feher.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center
                };
            }
            else if (kijelolt_figura.Text == "Világos futó")
            {
                aktualis.Content = new Image
                {
                    Source = new BitmapImage(new Uri("Kepek/futo_feher.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center
                };
            }
            else if (kijelolt_figura.Text == "Világos huszár")
            {
                aktualis.Content = new Image
                {
                    Source = new BitmapImage(new Uri("Kepek/lovag_feher.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center
                };
            }
            else if (kijelolt_figura.Text == "Világos bástya")
            {
                aktualis.Content = new Image
                {
                    Source = new BitmapImage(new Uri("Kepek/bastya_feher.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center
                };
            }
            else if (kijelolt_figura.Text == "Világos gyalog")
            {
                aktualis.Content = new Image
                {
                    Source = new BitmapImage(new Uri("Kepek/paraszt_feher.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center
                };
            }
            else if (kijelolt_figura.Text == "Sötét gyalog")
            {
                aktualis.Content = new Image
                {
                    Source = new BitmapImage(new Uri("Kepek/paraszt_fekete.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center
                };
            }

        }

        private void Hely_Megjeloles(int x, int y)
        {

            if (x >= 0 && x <= 7 && y >= 0 && y <= 7)
            {
                lepesek_helyei.Items.Add(oszlopJelek[7 - x] + "-" + (8 - y).ToString());
                Forgatas(7 - x, y);
            }
        }

        private void Lepes_Lehetosegek_Beszinezese(object sender,RoutedEventArgs e)
        {
            Button aktualis = sender as Button;
            int x = Holvan(aktualis)[0];
            int y = Holvan(aktualis)[1];
            x = 8 - x;

            if (kijelolt_figura.Text == "Világos gyalog")
            {
                Vilagos_Gyalog(x, y);
            }
            else if (kijelolt_figura.Text == "Sötét gyalog")
            {
                Sotet_Gyalog(x, y);
            }
            else if (kijelolt_figura.Text == "Világos király")
            {
                Kiraly(x, y);
            }
            else if (kijelolt_figura.Text == "Világos bástya")
            {
                Bastya(x, y);
            }
            else if (kijelolt_figura.Text == "Világos huszár")
            {
                Huszar(x, y);
            }
            else if (kijelolt_figura.Text == "Világos futó")
            {
                Futo(x, y);
            }
            else if (kijelolt_figura.Text == "Világos vezér")
            {
                Vezer(x, y);
            }
        }

        private void Vilagos_Gyalog(int x, int y)
        {
            List<int[]> Vilagos_Gyalog_lephet = new List<int[]>();
            x = 8 - x;
            lepesek_helyei.Items.Clear();
            int[] poz = new int[2];

            if (x >= 1)
            {
                if (x == 1)
                {
                    poz[0] = x + 2;
                    poz[1] = y;
                    Vilagos_Gyalog_lephet.Add(poz);
                    Hely_Megjeloles(poz[0], poz[1]);
                }

                poz[0] = x + 1;
                poz[1] = y - 1;
                Vilagos_Gyalog_lephet.Add(poz);
                Hely_Megjeloles(poz[0], poz[1]);

                poz[0] = x + 1;
                poz[1] = y;
                Vilagos_Gyalog_lephet.Add(poz);
                Hely_Megjeloles(poz[0], poz[1]);

                poz[0] = x + 1;
                poz[1] = y + 1;
                Vilagos_Gyalog_lephet.Add(poz);
                Hely_Megjeloles(poz[0], poz[1]);
            }
        }

        private void Sotet_Gyalog(int x, int y)
        {
            List<int[]> Sotet_Gyalog_lephet = new List<int[]>();
            x = 8 - x;
            lepesek_helyei.Items.Clear();

            int[] poz = new int[2];

            if (x <= 6)
            {
                poz[0] = x - 1;
                poz[1] = y;
                Sotet_Gyalog_lephet.Add(poz);
                Hely_Megjeloles(poz[0], poz[1]);

                poz[0] = x - 1;
                poz[1] = y - 1;
                Sotet_Gyalog_lephet.Add(poz);
                Hely_Megjeloles(poz[0], poz[1]);

                poz[0] = x - 1;
                poz[1] = y + 1;
                Sotet_Gyalog_lephet.Add(poz);
                Hely_Megjeloles(poz[0], poz[1]);

            }
        }

        private void Kiraly(int x, int y)
        {
            List<int[]> Kiraly_Lephet = new List<int[]>();
            x = 8 - x;
            lepesek_helyei.Items.Clear();
            int[] poz = new int[2];

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    poz[0] = x + i;
                    poz[1] = y + j;
                    Kiraly_Lephet.Add(poz);
                    if (!(poz[0] == x && poz[1] == y))
                    {
                        Hely_Megjeloles(poz[0], poz[1]);
                    }
                }
            }
        }

        private void Bastya(int x, int y)
        {
            List<int[]> Bastya_lephet = new List<int[]>();
            x = 8 - x;
            lepesek_helyei.Items.Clear();

            int[] poz = new int[2];
            for (int i = 0; i < 8; i++)
            {
                poz[0] = i;
                poz[1] = y;
                Bastya_lephet.Add(poz);
                Hely_Megjeloles(poz[0], poz[1]);
            }
            for (int i = 0; i < 8; i++)
            {
                poz[0] = x;
                poz[1] = i;
                Bastya_lephet.Add(poz);
                Hely_Megjeloles(poz[0], poz[1]);
            }

        }

        private void Huszar(int x, int y)
        {
            List<int[]> Huszar_lephet = new List<int[]>();
            x = 8 - x;
            lepesek_helyei.Items.Clear();

            int[] poz = new int[2];

            poz[0] = x + 2;
            poz[1] = y + 1;
            Huszar_lephet.Add(poz);
            Hely_Megjeloles(poz[0], poz[1]);

            poz[0] = x + 2;
            poz[1] = y - 1;
            Huszar_lephet.Add(poz);
            Hely_Megjeloles(poz[0], poz[1]);

            poz[0] = x - 2;
            poz[1] = y - 1;
            Huszar_lephet.Add(poz);
            Hely_Megjeloles(poz[0], poz[1]);

            poz[0] = x - 2;
            poz[1] = y + 1;
            Huszar_lephet.Add(poz);
            Hely_Megjeloles(poz[0], poz[1]);

            poz[0] = x + 1;
            poz[1] = y - 2;
            Huszar_lephet.Add(poz);
            Hely_Megjeloles(poz[0], poz[1]);

            poz[0] = x - 1;
            poz[1] = y - 2;
            Huszar_lephet.Add(poz);
            Hely_Megjeloles(poz[0], poz[1]);

            poz[0] = x - 1;
            poz[1] = y + 2;
            Huszar_lephet.Add(poz);
            Hely_Megjeloles(poz[0], poz[1]);

            poz[0] = x + 1;
            poz[1] = y + 2;
            Huszar_lephet.Add(poz);
            Hely_Megjeloles(poz[0], poz[1]);
        }

        private void Futo(int x, int y)
        {
            List<int[]> Futo_lephet = new List<int[]>();
            x = 8 - x;
            lepesek_helyei.Items.Clear();
            int[] poz = new int[2];

            //jobbra nagy átló
            if (x == 0 && y == 0 || x == 7 && y == 7)
            {
                for (int i = 0; i < 8; i++)
                {
                    poz[0] = i;
                    poz[1] = i;
                    Futo_lephet.Add(poz);
                    Hely_Megjeloles(poz[0], poz[1]);
                }
            }

            //jobbra átló
            else
            {
                if (x != 0 && y != 0)
                {
                    for (int i = 0; i < x; i++)
                    {
                        poz[0] = x - i;
                        if ((y - i) >= 0)
                        {
                            poz[1] = y - i;
                            Futo_lephet.Add(poz);
                            Hely_Megjeloles(poz[0], poz[1]);
                        }
                    }
                    int yy = y + 1;
                    for (int i = x + 1; i < 8; i++)
                    {
                        poz[0] = i;
                        poz[1] = yy++;

                        if (poz[0] < 8 && poz[1] < 8)
                        {
                            Futo_lephet.Add(poz);
                            Hely_Megjeloles(poz[0], poz[1]);
                        }
                    }
                }
            }

            //Balra nagy átló
            if (x == 7 && y == 0 || x == 0 && y == 7)
            {
                for (int i = 0; i < 8; i++)
                {
                    poz[0] = 7 - i;
                    poz[1] = 7 - i;
                    Futo_lephet.Add(poz);
                    Hely_Megjeloles(poz[0], poz[1]);
                }
            }

            //Balra átló
            int y3 = y - 1;
            for (int i = x + 1; i < 8; i++)
            {
                poz[0] = i;
                poz[1] = y3--;
                Futo_lephet.Add(poz);
                Hely_Megjeloles(poz[0], poz[1]);
            }

            int y4 = y + 1;
            for (int i = x - 1; i >= 0; i--)
            {
                poz[0] = i;
                poz[1] = y4++;
                if (poz[1] < 8)
                {
                    Futo_lephet.Add(poz);
                    Hely_Megjeloles(poz[0], poz[1]);
                }
            }
        }

        private void Vezer(int x, int y)
        {
            List<int[]> Vezer_lephet = new List<int[]>();
            x = 8 - x;
            lepesek_helyei.Items.Clear();

            int[] poz = new int[2];

            //jobra nagyátló
            if (x == 0 && y == 0 || x == 7 && y == 7)
            {
                for (int i = 0; i < 8; i++)
                {
                    poz[0] = i;
                    poz[1] = i;
                    Vezer_lephet.Add(poz);
                    Hely_Megjeloles(poz[0], poz[1]);
                }
            }

            //jobbra átló
            if (x != 0 && y != 0)
            {
                for (int i = 0; i < x; i++)
                {
                    poz[0] = x - i;
                    if ((y - i) >= 0)
                    {
                        poz[1] = y - i;
                        Hely_Megjeloles(poz[0], poz[1]);
                    }
                }
                int yy = y + 1;
                for (int i = x + 1; i < 8; i++)
                {
                    poz[0] = i;
                    poz[1] = yy++;

                    if (poz[0] < 8 && poz[1] < 8)
                    {
                        Hely_Megjeloles(poz[0], poz[1]);
                    }
                }
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

        private void ujraSzinezes_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
