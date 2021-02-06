using System;

namespace EDU_OOP_RPG.Characters
{
    public class Wizard : Character
    {
        private int currentMana;
        private int maxMana;

        public Wizard(int id, string name, races race, genders gender, int currentMana, int maxMana) : base(id, name,
            race, gender)
        {
            this.currentMana = currentMana;
            this.maxMana = maxMana;
        }

        public int CurrentMana
        {
            get => currentMana;
            set
            {
                if (value > MaxMana)
                {
                    throw new ArgumentException("Текущая мана не может стать больше максимальной");
                }
                else if (value >= 0)
                {
                    currentMana = value;
                }
                else
                {
                    throw new ArgumentException("Мана не может стать отрицательной");
                }
            }
        }

        public int MaxMana
        {
            get => maxMana;
            set
            {
                if (value >= 0)
                {
                    maxMana = value;
                }
                else
                {
                    throw new ArgumentException("Максимальная мана не может стать отрицательной");
                }
            }
        }
    }
}