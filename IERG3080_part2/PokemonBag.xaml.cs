using System;
using System.Collections.Generic;
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

namespace IERG3080_part2
{
    /// <summary>
    /// Interaction logic for PokemonBag.xaml
    /// </summary>
    public partial class PokemonBag : Page
    {
        Player player = Player.Instance;
        public PokemonBag()
        {
            InitializeComponent();
            UpperMenu.Content = "Candy: " + player.Candy + "       stardust: " + player.startdust + "       Money: " + player.Money;
        }
        private void PokemonBagButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Navigation.xaml", UriKind.Relative));
        }
    }
}
