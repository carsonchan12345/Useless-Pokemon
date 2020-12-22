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
using System.IO;
using System.Xml;


namespace IERG3080_part2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Capture : Window
    {
        static int Chances = 0;
        private Uri PokemonImage;
        private Random rnd;
        private int rndint;
        private int id;
        private Pokemon Encounterpoke;
        public int Result { get; private set; }
        private int Answer;

        public Capture(int id)
        {
            InitializeComponent();
           
            rnd = new Random();
            this.id = id;
            Loadpokemon();
            rndint= rnd.Next(1, 6);
            Gamestart();
        }

        private void Loadpokemon()
        {
            Encounterpoke = new Pokemon(id);

        }

        private void Addpokemon()
        {
            Player.Instance.AddPokemon(Encounterpoke);
        }

        private void Loadimage(int id)
        {
             PokemonImage =new Uri("images/" + id+ ".png", UriKind.Relative); 
        }

        private void Displayimage()
        {
            pokeimg.Source = new BitmapImage(PokemonImage);
        }

        private void Displaytitle()
        { var tmp = Encounterpoke.Name + " shows up!\n" + "Guess a number between 1-5. Close the window if you don't want this.";
            title.Text = tmp;
        }

        private void Compare()
        {
            if (rndint == Answer)
            {
                MessageBox.Show("Your guess is correct! Captured!");
                Addpokemon();
                Addmiscell(1, 1, 1);
            }
            else
            {
                MessageBox.Show("Your guess is incorrect! Can't be Captured!");
                if (Chancescheck())
                {
                    MessageBox.Show("But you have 100 chances now, you still get it!");
                    Addpokemon();
                    Addmiscell(1, 1, 1);
                }
            }
            this.Close();
        }

        private void Addmiscell(int startdust, int Money, int Candy)
        {

            Player.Instance.startdust += startdust;
            Player.Instance.Money += Money;
            Player.Instance.Candy += Candy;

        }

        private void Displaychances()
        { string tmp = "100% catch it if you accumlated over 100 chances. Your chances now: " + Chances.ToString();
            chances.Text = tmp;
        }

        private bool Chancescheck()
        {
            Chances += 50;
            bool result;
            if (Chances>=100)
            {
                result = true;
                Chances = 0;
            }
            else
            {
                result = false;
                
            }
            return result;
        }
        private void Gamestart()
        {   
            Loadimage(id);
            Displayimage();
            Displaytitle();
            Displaychances();

        }

        private void Submit_click(object sender, RoutedEventArgs e)
        {
            try
            {
                Answer = int.Parse(answer.Text);
                Compare();
            }
            catch (Exception)
            {
                MessageBox.Show("Only 1-5 input is allowed!");
                answer.Text = "";
            }
            
        }
    }


}


