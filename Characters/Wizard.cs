using System;
using System.Collections.Generic;
using System.Text;

namespace EDU_OOP_RPG.Characters
{
    public class Wizard : Character
    {
        private int currentMana;
        private int maxMana;

        public Wizard(int id, string name, races race, genders gender,  int currentMana, int maxMana) : base(id, name, race, gender)
        {
            this.currentMana = currentMana;
            this.maxMana = maxMana;
        }
    }
}
