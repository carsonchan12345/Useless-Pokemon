using System;
using System.Collections.Generic;
using System.Text;

namespace IERG3080_part2
{
    class Player
    {
        private string name;
        private int money;
        private int candy;
        private int stardust;
        public List<Pokemon> MyPokemon = new List<Pokemon>();
        private int evolveStone;
        private double xcoordinate = 0;
        private double ycoordinate = 0;
        public double Xcoordinate { get { return xcoordinate; } set { xcoordinate = value; } }
        public double Ycoordinate { get { return xcoordinate; } set { xcoordinate = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int EvolveStone { get { return evolveStone; } set { evolveStone = value; } }
        public int Money { get { return money; } set { money = value; } }
        public int Candy { get { return candy; } set { candy = value; } }
        public int startdust { get { return stardust; } set { stardust = value; } }
        public Player()
        {
            money = 500;
            candy = 10;
            stardust = 1000;
            EvolveStone = 2;

        }
        public void AddPokemon(Pokemon poke)
        {
            if(MyPokemon.Count<6)
                  MyPokemon.Add(poke);
         //   else ???
        }
        public void DelPokemon(int elementAt ) //by object or by num?
        {
            if (MyPokemon.Count >= elementAt)
                 MyPokemon.RemoveAt(elementAt);
        }
        private static Player instance;
        public static Player Instance
        {
            get
            {
                if (instance == null)
                    instance = new Player();
                return instance;
            }
        }
    }
}
