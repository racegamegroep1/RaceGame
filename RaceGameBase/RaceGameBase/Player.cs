using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceGameBase
{
    public class Player
    {
        public static List<Player> Players { get; set; }
        public string Name { get; set; }
        public Car CarObject { get; set; }
        public bool UseKeyboard { get; set; }
        public bool IsAI { get; set; }
        public int InputID { get; set; }

        public Player(string name, Car car, int type, int inputid)
        {
            Name = name;
            CarObject = car;

            if (type == 1)
                UseKeyboard = true;
            else if (type == 0)
                UseKeyboard = false;
            else
            {
                IsAI = true;
            }

            InputID = inputid;
        }

        //type: 1 = keyboard, 2 = xbox, 3 = ai
        public static int GetInputId(List<Player> players, int type)
        {
            int keyboard = 1;
            int xbox = 1;
            int ai = 1;

            foreach (Player p in players)
            {
                if (p.UseKeyboard)
                    keyboard++;
                else
                {
                    if (p.IsAI)
                        ai++;
                    else
                        xbox++;
                }
            }

            switch (type)
            {
                case 1: return keyboard; 
                case 2: return xbox;
                case 3: return ai;

                default: return 0;
            }
        }
    }
}