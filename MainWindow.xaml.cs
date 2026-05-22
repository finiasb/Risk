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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;

            // Acum aplicăm modul full-screen în ordinea corectă
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            this.ResizeMode = ResizeMode.NoResize;
        }
        private void Provincie_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Path hartiutaLovit)
            {
                string numeTara = hartiutaLovit.Tag?.ToString() ?? hartiutaLovit.Name;

                MessageBox.Show("Ai apasat pe: " + numeTara);
            }
        }
    }
}