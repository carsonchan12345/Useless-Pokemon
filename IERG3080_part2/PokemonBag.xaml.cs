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
            for (int i = 0; i < player.MyPokemon.Count ; i++)
            {
                Rectangle rec = new Rectangle
                {
                    Width = 100,
                    Height = 100,
                    StrokeThickness = 1,
                    Stroke = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(5),
                };
                rec.Fill = player.MyPokemon[i].PokemonImage;
                rec.Name = "MyPokemon_"+ i.ToString();
                rec.MouseLeftButtonDown += rec_MouseLeftButtonDown;
                PokemonList.Children.Add(rec);
            }
        }

        private void rec_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Rectangle x = (Rectangle)sender;
            string[] pokeId = x.Name.Split("MyPokemon_");
            PokemonBag_Pokemon ToPage= new PokemonBag_Pokemon(int.Parse(pokeId[1]));
            NavigationService.Navigate(ToPage);
        }
        private void PokemonBagButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Navigation.xaml", UriKind.Relative));
        }
    }
}
