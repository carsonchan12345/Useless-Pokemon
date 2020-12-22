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
    /// Interaction logic for PokemonBag_Pokemon.xaml
    /// </summary>
    public partial class PokemonBag_Pokemon : Page
    {
        Player player = Player.Instance;
        private int index;
        public PokemonBag_Pokemon(int index)  
        {
            InitializeComponent();
            this.index = index;   //change UI base on player class
            pokeImage.Fill = player.MyPokemon[index].PokemonImage;
            pokeAttack.Content = "Attack: " + player.MyPokemon[index].Attack;
            pokeCp.Content = "CP: " + player.MyPokemon[index].Cp;
            pokeEvolution.Content = "Required:  1 evoluteStone" ;
            pokeHeight.Content ="Height: " + player.MyPokemon[index].Height;
            pokeHp.Content = "HP: " + player.MyPokemon[index].Hp;
            pokeLv.Content ="Level: " + player.MyPokemon[index].Lv;
            pokeName.Content = "Name: "+ player.MyPokemon[index].Name;
            pokeSell.Content ="Price: " + player.MyPokemon[index].Price;
            pokeType.Content ="Type: "+ player.MyPokemon[index].Type;
            pokeWeight.Content ="Weight: " + player.MyPokemon[index].Weight;
            MyCandy.Content = "MyCandy: " + player.Candy;
            MyStardust.Content = "MyStardust: " + player.startdust;
            MyEvolveStone.Content = "MyEvolveStone: " + player.EvolveStone;
            requiredPowerUp.Content = "Required: Stardust: " + player.MyPokemon[index].Lv*10+ "   Candy: " + player.MyPokemon[index].Lv / 10;
            if (player.MyPokemon[index].Evolve == "null")
            {
                pokeEvolution.Content = "";
                pokeEvolutionButton.Visibility = Visibility.Hidden;
            }

            RenameTextBox.Visibility = Visibility.Hidden;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("PokemonBag.xaml", UriKind.Relative));
        }

        private void Evolution_Click(object sender, RoutedEventArgs e)
        {
            EvPokemon();
            Refresh();
        }

        private void Sell_Click(object sender, RoutedEventArgs e)
        {
            SellPokeMon();
            NavigationService.Navigate(new Uri("PokemonBag.xaml", UriKind.Relative));
        }

        private void PowerUp_Click(object sender, RoutedEventArgs e)
        {
            PowerUpPokemon();
            Refresh();
        }
        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            if (RenameTextBox.Visibility == Visibility.Hidden)
                RenameTextBox.Visibility = Visibility.Visible;
            else
            {
                player.MyPokemon[index].Name = RenameTextBox.Text;
                Refresh();
            }
        }

        private void Refresh()
        {
            PokemonBag_Pokemon again = new PokemonBag_Pokemon(index);
            NavigationService.Navigate(again);

        }
        private void PowerUpPokemon()
        {
            int reqStar = player.MyPokemon[index].Lv * 10;
            int reqCandy = player.MyPokemon[index].Lv / 10;
            if (player.startdust >= reqStar && player.Candy >= reqCandy)
            {
                player.MyPokemon[index].LevelUp();
                player.startdust -= reqStar;
                player.Candy -= reqCandy;
            }
            else
                MessageBox.Show("No enough Material!!!");
           
        }
        private void SellPokeMon()
        {
            player.Candy += player.MyPokemon[index].Lv / 10;
            player.startdust += player.MyPokemon[index].Lv * 10;
            player.Money += player.MyPokemon[index].Price;
            MessageBox.Show("Congrulation!! You get Price:" + player.MyPokemon[index].Price + " Candy: " + player.MyPokemon[index].Lv / 10 + " Stardust: " + player.MyPokemon[index].Lv * 10);
            player.DelPokemon(index);
        }
        private void EvPokemon()
        {
            if (player.MyPokemon[index].Evolve == "null")
                MessageBox.Show("This pokemon can't evolve!");
            else if (player.EvolveStone < 1)
                MessageBox.Show("You need one EvolveStone!");
            else if (player.MyPokemon[index].EvolveLv > player.MyPokemon[index].Lv)
                MessageBox.Show("Pokemon EvolveLv is " + player.MyPokemon[index].EvolveLv);
            else
            {
                player.EvolveStone--;
                player.MyPokemon[index] = player.MyPokemon[index].PokemonEvolve();
                MessageBox.Show("Congratulation!!!!");
            }              
        }

    }
}
