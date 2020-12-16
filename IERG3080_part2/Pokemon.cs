using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Xml;

namespace IERG3080_part2
{
    class Pokemon
    {
        private int cp;
        private int id;
        private double hp;
        private int lv;
        private int price;
        private double attack;
        private int evolveLv;
        private string evolve;
        private string name;
        private string skill1;
        private string skill2;
        private string skill3;
        private string skill4;
        public int Cp
        {
            get
            {
                Random rnd = new Random();
                cp = cp + rnd.Next(50, 200);
                return cp;
            }
        }
        public int Id { get { return id; } set { id = value; } }
        public double Hp { get { return hp + Cp * 0.05; } set { hp = value; } }
        public int Lv { get { return lv; } set { lv = value; } }
        public int Price { get { return price; } set { price = value; } }
        public double Attack { get { return attack + Cp * 0.07;  } set { attack = value; } }
        public int EvolveLv { get { return evolveLv; } set { evolveLv = value; } }
        public string Evolve { get { return evolve; } set { evolve = value; } }
        public string Name { get { return name; } set {name = value; } }
        public string Skill1 { get { return skill1; } set { skill1 = value; } }
        public string Skill2 { get { return skill2; } set { skill2 = value; } }
        public string Skill3 { get { return skill3; } set { skill3 = value; } }
        public string Skill4 { get { return skill4; } set { skill4 = value; } }
       public void LevelUp()
        {
            Random rnd = new Random();
            Hp = Hp + rnd.Next(1, 8);
            Attack = Attack + rnd.Next(2, 6);
            if (Lv == EvolveLv)
            {
                id++;
                Attack= Attack +rnd.Next(4,10);
                Hp = Hp + rnd.Next(7, 13);
            }
        }
        public Pokemon(int ID)
        {           
            XmlReader reader = XmlReader.Create("C:\\Users\\ths019\\Desktop\\IERG3080_part2\\IERG3080_part2\\pokemon.xml");
            while (reader.Read())
            {
                if(reader.Name=="Pokemon"&&reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.HasAttributes && int.Parse(reader.GetAttribute("Id")) == ID)
                    { //maybe can do it one line?                  
                        this.id = int.Parse(reader.GetAttribute("Id"));
                        this.lv = int.Parse(reader.GetAttribute("Lv"));
                        this.price = int.Parse(reader.GetAttribute("Price"));
                        this.attack = Convert.ToDouble(reader.GetAttribute("Attack"));
                        this.hp = Convert.ToDouble(reader.GetAttribute("Hp"));
                        this.evolveLv = int.Parse(reader.GetAttribute("EvolveLv"));
                        this.name = reader.GetAttribute("Name");
                        this.evolve = reader.GetAttribute("Evolve");
                        this.cp = int.Parse(reader.GetAttribute("Cp"));
                        this.skill1 = reader.GetAttribute("Skill1");
                        this.skill2 = reader.GetAttribute("Skill2");
                        this.skill3 = reader.GetAttribute("Skill3");
                        this.skill4 = reader.GetAttribute("Skill4");
                    }
                }
            }
        }
    }
}
