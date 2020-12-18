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
using System.Windows.Threading;


namespace IERG3080_part2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainMapPage Map=new MainMapPage();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.NavigationService.Navigate(new Uri("Navigation.xaml", UriKind.Relative));
            start.Visibility = Visibility.Hidden;
            end.Visibility = Visibility.Hidden;
            Pokemon p1 =new Pokemon(5);
            Pokemon p2 = new Pokemon(5);
            Pokemon p3 = new Pokemon(2);
            Pokemon p4 = new Pokemon(2);
            Player Me = new Player();
            Me.AddPokemon(p1);
            Me.AddPokemon(p2);
            Me.AddPokemon(p3);
            Me.AddPokemon(p4);
            Me.DelPokemon(2);
            Me.Name = "Peter";
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void dock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Down || e.Key == Key.Up || e.Key == Key.Right)
                e.Handled = false;
        }

    }
}
