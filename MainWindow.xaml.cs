using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Risk
{
    public partial class MainWindow : Window
    {   private class Jucator
        {
            public string Nume { get; set; }
            public SolidColorBrush CuloareTara { get; set; }
            public SolidColorBrush CuloareCerc { get; set; }
        }

        private int numarJucatoriSelectati;
        private Random zar = new Random();
        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            this.ResizeMode = ResizeMode.NoResize;
            this.Loaded += MainWindow_Loaded;
        }

        private void Provincie_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AlegereJucatoriWindow ecranSelectie = new AlegereJucatoriWindow();

            if (ecranSelectie.ShowDialog() == true)
            {
                numarJucatoriSelectati = ecranSelectie.NumarJucatori;

                GenereazaHartaInitiala();
            }
            else
            {
                this.Close();
            }
        }

        private void GenereazaHartaInitiala()
        {
            List<string> tariLibere = new List<string>(ordineaTarilor);

            List<Jucator> jucatoriActivi = totiJucatorii.Take(numarJucatoriSelectati).ToList();

            int indexJucatorCurent = 0;
            int pozitieCurentaInLista = -1; 

            int totalAruncari = jucatoriActivi.Count * 5; 

            for (int aruncare = 0; aruncare < totalAruncari; aruncare++)
            {
                if (tariLibere.Count == 0) break;

                Jucator jucatorProprietar = jucatoriActivi[indexJucatorCurent];

                int pasiZar = zar.Next(1, 7);

                pozitieCurentaInLista = (pozitieCurentaInLista + pasiZar) % tariLibere.Count;

                string numeTaraAleasa = tariLibere[pozitieCurentaInLista];

                Path teritoriuXAML = HartaCanvas.Children.OfType<Path>().FirstOrDefault(p => p.Name == numeTaraAleasa);

                if (teritoriuXAML != null)
                {
                    teritoriuXAML.Fill = jucatorProprietar.CuloareTara;

                    if (!string.IsNullOrEmpty(teritoriuXAML.Uid))
                    {
                        DesenazaCercTancuri(teritoriuXAML.Uid, 1, jucatorProprietar.CuloareCerc);
                    }
                }

                tariLibere.RemoveAt(pozitieCurentaInLista);

                pozitieCurentaInLista--;

                indexJucatorCurent = (indexJucatorCurent + 1) % jucatoriActivi.Count;
            }
        }

        private void DesenazaCercTancuri(string uidCoordonate, int numarTancuri, SolidColorBrush culoareCerc)
        {
            string[] coordonate = uidCoordonate.Split(',');
            if (coordonate.Length == 2)
            {
                double x = double.Parse(coordonate[0]);
                double y = double.Parse(coordonate[1]);
                double diametru = 24;

                Grid containerArmata = new Grid { Width = diametru, Height = diametru, IsHitTestVisible = false };

                Ellipse cercPlat = new Ellipse
                {
                    Fill = culoareCerc, 
                    Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#95000000")),
                    StrokeThickness = 1.2
                };

                TextBlock textTancuri = new TextBlock
                {
                    Text = numarTancuri.ToString(),
                    Foreground = Brushes.White,
                    FontWeight = FontWeights.Bold,
                    FontSize = 13,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                containerArmata.Children.Add(cercPlat);
                containerArmata.Children.Add(textTancuri);

                Canvas.SetLeft(containerArmata, x - (diametru / 2));
                Canvas.SetTop(containerArmata, y - (diametru / 2));

                HartaCanvas.Children.Add(containerArmata);
            }
        }
        

        private List<Jucator> totiJucatorii = new List<Jucator>
        {
            new Jucator {
                Nume = "Player 1",
                CuloareTara = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAD12AE")),
                CuloareCerc = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF7B147D"))
            },
            new Jucator {
                Nume = "Player 2",
                CuloareTara = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF47C1DC")),
                CuloareCerc = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2293AC"))
            },
            new Jucator {
                Nume = "Player 3",
                CuloareTara = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF785C05")),
                CuloareCerc = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9D7902"))
            },
            new Jucator {
                Nume = "Player 4",
                CuloareTara = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF85DF8E")),
                CuloareCerc = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF48C74C"))
            },
            new Jucator {
                Nume = "Player 5",
                CuloareTara = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD9DD23")),
                CuloareCerc = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB5B81D"))
            },
            new Jucator {
                Nume = "Player 6",
                CuloareTara = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEA4224")),
                CuloareCerc = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9C240F"))
            }
        };

        private List<string> ordineaTarilor = new List<string>
        {
            "southern_europe", "siberia", "papua", "south_africa", "quebec", "brazil",
            "middle_east", "egypt", "ukraine", "columbia", "western_australia",
            "vancouver", "groenlanda", "china", "north_africa", "argentina", "western_europe",
            "eastern_australia", "ural", "eastern_united_states", "north_africa", "japan", "peru",
            "northwest_territory", "india", "east_africa", "kamchatka", "central_america",
            "indonezia", "winnipeg", "mongolia", "congo", "alaska", "indochina", "ukraine",
            "afghanistan", "western_united_states", "scandinavia", "irkutsk"
        };
    }
}