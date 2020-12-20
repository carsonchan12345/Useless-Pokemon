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
using System.Windows.Shapes;
using System.Xml;

namespace IERG3080_part2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Gymbattle : Window
    {
        Button[] Skill = new Button[4];
        private Uri PokemonImage;
        public bool Playerturns { get; private set; }
        public int HP { get; private set; }
        public int GymHP { get; private set; }
        public int playercurrentpokeindex { get; private set; }
        public int gymcurrentpokeindex { get; private set; }
        private Gympokemon gym = new Gympokemon();

        public Gymbattle()
        {
            InitializeComponent();
            playercurrentpokeindex = -1;
            gymcurrentpokeindex = -1;
            Battle();
            Initiateskillsbutton();
            Loadskillsbutton();
        }

        private void Initiateskillsbutton()
        {
            for (int i = 0; i < 4; i++)
            {
                Skill[i] = skills.Children[i] as Button;
            }
        }
        private void Loadskillsbutton()
        {

            Skill[0].Content = gym.MyPokemon[0].Skill1;
            Skill[1].Content = gym.MyPokemon[0].Skill2;
            Skill[2].Content = gym.MyPokemon[0].Skill3;
            Skill[3].Content = gym.MyPokemon[0].Skill4;

        }

        private void LoadNext(int id, Image pokeimg)//wrapper
        {
            Loadimage(id);
            Displayimage(gymcurrentimg);
        }

        private void Loadimage(int id)
        {
            PokemonImage = new Uri("images/" + id + ".png", UriKind.Relative);
        }
        private void Displayimage(Image pokeimg)
        {
            pokeimg.Source = new BitmapImage(PokemonImage);
        }

        private void Battle()
        {

        }

    }

     class Gympokemon
    {
        public List<Pokemon> MyPokemon = new List<Pokemon>();
        public Gympokemon()
        {
            MyPokemon.Add(new Pokemon(2));
            MyPokemon.Add(new Pokemon(4));
            MyPokemon.Add(new Pokemon(5));
        }
            
    }

}

