﻿using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace IERG3080_part2
{
    /// <summary>
    /// Interaction logic for MainMapPage.xaml
    /// </summary>
    public partial class MainMapPage : Page
    {
        Player player = Player.Instance;
        private ImageBrush UserImageUp, UserImageDown, UserImageLeft, UserImageRight;
        Rect userHitBox, GymBattleHitBox;
        DispatcherTimer gameTimer = new DispatcherTimer(); 
        public MainMapPage()
        {
            InitializeComponent();
            GameSetUp();
        }
        private void User_KeyDown(object sender, KeyEventArgs e)
        {
            Point MyMapCoordinate = Map.TranslatePoint(new Point(0, 0), MyCanvas);
            Point UserCoordinate = user.TranslatePoint(new Point(0, 0), Map);
            Point MenuCoordinate = Menu.TranslatePoint(new Point(0, 0), Map);
            if (e.Key == Key.Left || e.Key == Key.Down || e.Key == Key.Up || e.Key == Key.Right)
            {
                MessageBox.Show(" W (Up) S (Down) A (Left) D (Right) ");
                MainMapPage again = new MainMapPage();
                NavigationService.Navigate(again);
            }
            if (UserCoordinate.Y<205 || UserCoordinate.Y > 790 || UserCoordinate.X < 420 || UserCoordinate.X > 1150)
                {
                    MessageBox.Show("Palkia controls space.");
                    MessageBox.Show("YOU CAN'T LEAVE :) ");
                    MainMapPage again = new MainMapPage();
                    NavigationService.Navigate(again);
                } 
                if (e.Key == Key.A)
            {
                Canvas.SetLeft(user, UserCoordinate.X - 4);
                Canvas.SetLeft(Menu, MenuCoordinate.X - 4);
                Canvas.SetLeft(Map, MyMapCoordinate.X + 4);
                user.Fill = UserImageLeft;
            }
            if (e.Key == Key.D)
            {
                Canvas.SetLeft(user, UserCoordinate.X + 4);
                Canvas.SetLeft(Menu, MenuCoordinate.X + 4);
                Canvas.SetLeft(Map, MyMapCoordinate.X - 4);
                user.Fill = UserImageRight;
            }
            if (e.Key == Key.W )
            {
                Canvas.SetTop(user, UserCoordinate.Y - 4);
                Canvas.SetTop(Menu, MenuCoordinate.Y - 4);
                Canvas.SetTop(Map, MyMapCoordinate.Y + 4);
                user.Fill = UserImageUp;
            }
            if (e.Key == Key.S)
            {
                Canvas.SetTop(user, UserCoordinate.Y + 4);
                Canvas.SetTop(Menu, MenuCoordinate.Y + 4);
                Canvas.SetTop(Map, MyMapCoordinate.Y - 4);
                user.Fill = UserImageDown;
            }
            player.Mapcoordinate = MyMapCoordinate;
            player.Menucoordinate = MenuCoordinate;
            player.Playercoordinate = UserCoordinate;
            userHitBox = new Rect(Canvas.GetLeft(user), Canvas.GetTop(user), user.Width, user.Height);
            GymBattleHitBox = new Rect(Canvas.GetLeft(GymBattle), Canvas.GetTop(GymBattle), GymBattle.Width, GymBattle.Height);
            if (userHitBox.IntersectsWith(GymBattleHitBox))
            {
                MessageBox.Show("GymBattle!");
                NavigationService.Navigate(new Uri("GymBattle.xaml", UriKind.Relative));
            }
            foreach (var x in Map.Children.OfType<Rectangle>())
            {               
                Rect PokemonHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                if ((string)x.Tag == "RandomPokemon")
                {
                    if (userHitBox.IntersectsWith(PokemonHitBox) && x.Visibility == Visibility.Visible)
                    {
                        MessageBox.Show("Encounter Pokemon!");
                        Capture test=new Capture();
                        test.ShowDialog();
                    }
                }
            }
        }
        private void GameSetUp()
        {
            MyCanvas.Focus();          
            UserImageUp = new ImageBrush();
            UserImageUp.ImageSource = new BitmapImage(new Uri("../../../images/zup.png",UriKind.RelativeOrAbsolute));
            UserImageDown = new ImageBrush();
            UserImageDown.ImageSource = new BitmapImage(new Uri("../../../images/zdown.png", UriKind.RelativeOrAbsolute));
            UserImageLeft = new ImageBrush();
            UserImageLeft.ImageSource = new BitmapImage(new Uri("../../../images/zleft.png", UriKind.RelativeOrAbsolute));
            UserImageRight = new ImageBrush();
            UserImageRight.ImageSource = new BitmapImage(new Uri("../../../images/zright.png",UriKind.RelativeOrAbsolute));
            user.Fill = UserImageDown;
            foreach (var x in Map.Children.OfType<Rectangle>())
            {
                Random rnd = new Random();
                if ((string)x.Tag == "RandomPokemon")
                {
                    ImageBrush PokemonImage = new ImageBrush();
                    int PokeId = rnd.Next(1, 6);
                    PokemonImage.ImageSource = new BitmapImage(new Uri("../../../images/"+ PokeId + ".png", UriKind.RelativeOrAbsolute));
                    x.Fill=PokemonImage;
                    x.Name ="pokemon_"+PokeId.ToString();
                }
            }
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(1);
            gameTimer.Start();
            Point CheckPoint = new Point(0, 0);
               if (player.Mapcoordinate != CheckPoint)
               {
                   Canvas.SetLeft(user, player.Playercoordinate.X);
                   Canvas.SetLeft(Menu, player.Menucoordinate.X);
                   Canvas.SetLeft(Map, player.Mapcoordinate.X);
                   Canvas.SetTop(user, player.Playercoordinate.Y);
                   Canvas.SetTop(Menu, player.Menucoordinate.Y);
                   Canvas.SetTop(Map, player.Mapcoordinate.Y);
               } 
        }
        private void GameLoop(object sender, EventArgs e)
        {
            foreach (var x in Map.Children.OfType<Rectangle>())
            {
                Random rnd = new Random();
                Rect PokemonHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                if ((string)x.Tag == "RandomPokemon")
                {
                    if (rnd.Next() % 2 == 0) Canvas.SetLeft(x, Canvas.GetLeft(x) - rnd.Next(5, 10));
                    else Canvas.SetLeft(x, Canvas.GetLeft(x) + rnd.Next(5, 10));
                    if (rnd.Next() % 2 == 0) Canvas.SetTop(x, Canvas.GetTop(x) - rnd.Next(5, 10));
                    else Canvas.SetTop(x, Canvas.GetTop(x) + rnd.Next(5, 10));                  
                }
            }
        }
        private void PokemonBagButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("PokemonBag.xaml", UriKind.Relative));
        }
    }
}
