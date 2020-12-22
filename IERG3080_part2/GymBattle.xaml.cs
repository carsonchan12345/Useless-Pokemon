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
using System.Threading;


namespace IERG3080_part2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Gymbattle : Window
    {

        private bool End;
        Button[] Skill = new Button[4];
        private Uri PokemonImage;
        public bool Playerturns { get; private set; }
        public double HP { get; private set; }
        public double GymHP { get; private set; }
        public int playercurrentpokeindex { get; private set; }
        public int gymcurrentpokeindex { get; private set; }
        private Gympokemon gym = new Gympokemon();

        public Gymbattle()
        {   
            InitializeComponent();
            End = false;
            playercurrentpokeindex = -1;
            gymcurrentpokeindex = -1;
            Playerturns = true;
            Initiateskillsbutton();
            Battle();
            
        }

        private void Initiateskillsbutton()
        {
            for (int i = 0; i < 4; i++)
            {
                Skill[i] = skills.Children[i] as Button;
                Skill[i].Click += new RoutedEventHandler(Playerattack);
            }
        }

        private void Lockbutton()
        {
            foreach (Button x in Skill)
                x.IsEnabled = false;

        }

        private void Enablebutton()
        {
            foreach (Button x in Skill)
            {
                if (x.Content.ToString() != "null")
                    x.IsEnabled = true;
            }
        }

        private void Loadskillsbutton()
        {

            Skill[0].Content = Player.Instance.MyPokemon[playercurrentpokeindex].Skill1;
            Skill[1].Content = Player.Instance.MyPokemon[playercurrentpokeindex].Skill2;
            Skill[2].Content = Player.Instance.MyPokemon[playercurrentpokeindex].Skill3;
            Skill[3].Content = Player.Instance.MyPokemon[playercurrentpokeindex].Skill4;
            foreach (Button x in Skill)
            {
                if (x.Content.ToString() == "null")
                    x.IsEnabled = false;
            }
        }

        public double Loadskills(string Skillname)
        {
            double attack=0;
            XmlReader reader = XmlReader.Create("../../../data/SkillSet.xml");
            while (reader.Read())
            {
                if (reader.Name == "SkillSet" && reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.HasAttributes && reader.GetAttribute("Name") == Skillname)
                    { //maybe can do it one line?                   
                         attack = Convert.ToDouble(reader.GetAttribute("Attack"));

                    }
                }
            }

            return attack;
        }

        private void Gymattack()
        {   
            Random rnd = new Random();
            List<string> avaskillset = new List<string>();
            string choosenskill;
            double attack;
            if (gym.MyPokemon[gymcurrentpokeindex].Skill1 != "null")
                    avaskillset.Add(gym.MyPokemon[gymcurrentpokeindex].Skill1);
            if (gym.MyPokemon[gymcurrentpokeindex].Skill2 != "null")
                avaskillset.Add(gym.MyPokemon[gymcurrentpokeindex].Skill2);
            if (gym.MyPokemon[gymcurrentpokeindex].Skill3 != "null")
                avaskillset.Add(gym.MyPokemon[gymcurrentpokeindex].Skill3);
            if (gym.MyPokemon[gymcurrentpokeindex].Skill4 != "null")
                avaskillset.Add(gym.MyPokemon[gymcurrentpokeindex].Skill4);

            choosenskill = avaskillset[ rnd.Next(0, avaskillset.Count())];
            attack = Loadskills(choosenskill)* gym.MyPokemon[gymcurrentpokeindex].Attack;
            this.HP -= attack;
            HPdisplay();
            Checkhp();
            Battleinfodisplay(choosenskill, attack);
            HPdisplay();
            this.Playerturns = true;
            Enablebutton();
        }

        public void Playerattack(object sender, RoutedEventArgs e)
        {
            Lockbutton();
            string choosenskill;
            double attack;
            Button button = sender as Button;
            choosenskill = button.Content.ToString();
            attack = Loadskills(choosenskill) * Player.Instance.MyPokemon[playercurrentpokeindex].Attack;
            this.GymHP -= attack;
            Battleinfodisplay(choosenskill, attack);
            HPdisplay();
            Checkhp();
            
            Playerturns = false;
            
            Thread.Sleep(1000);
            Gymattack();

        }


        public bool Isend()
        {
            return End;
        }

        private void PlayerLoadNext()//wrapper
        {
            playercurrentpokeindex += 1;
            if (playercurrentpokeindex >= Player.Instance.MyPokemon.Count())
            {
                MessageBox.Show("You have no pokemon left!");
                End = true;
                this.Close();


            }
            else
            {

                Loadimage(Player.Instance.MyPokemon[playercurrentpokeindex].Id);
                Displayimage(playercurrentimg);
                Loadskillsbutton();
                HP = Player.Instance.MyPokemon[playercurrentpokeindex].Hp;
                HPdisplay();
            }
        }

        private void GymLoadNext()
        {   gymcurrentpokeindex += 1;
            if (gymcurrentpokeindex >= gym.MyPokemon.Count())
            {
                MessageBox.Show("Gym no pokemon left! You win!");
                this.Close();
            }
            else
            {
            Loadimage(gym.MyPokemon[gymcurrentpokeindex].Id);
            Displayimage(gymcurrentimg);
            GymHP = gym.MyPokemon[gymcurrentpokeindex].Hp;
            HPdisplay();
            }
           
        }

        private void Loadimage(int id)
        {
            PokemonImage = new Uri("images/" + id + ".png", UriKind.Relative);
        }
        private void Displayimage(Image pokeimg)
        {
            pokeimg.Source = new BitmapImage(PokemonImage);
        }

        private void HPdisplay()
        {
            playerhp.Text = HP.ToString();
            gymhp.Text = GymHP.ToString();

        }

        private void Battleinfodisplay(string skill, double attack)
        {
            string tmp;
            if (Playerturns)
                tmp = "Player use " + skill +"deal -"+ attack + " to gym";
            else
                tmp = info.Text + "\nGym use " + skill + "deal -" + attack + " to player";
            info.Text = tmp;
        }
        private void Checkhp()
        {
            if (HP <= 0)
            {
                Thread.Sleep(1000); 
                MessageBox.Show("Your pokemon is fainted, next!");
                PlayerLoadNext();
               
            }
            if (GymHP <= 0)
            {
                Thread.Sleep(1000);
                MessageBox.Show("Gym pokemon is fainted, next!");
                GymLoadNext();
            
            }

        }
        private void Battle()//wrapper
        {
            PlayerLoadNext();
            GymLoadNext();
 

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
            MyPokemon.Add(new Pokemon(5));
            MyPokemon.Add(new Pokemon(5));
            MyPokemon.Add(new Pokemon(5));
        }
            
    }

}

