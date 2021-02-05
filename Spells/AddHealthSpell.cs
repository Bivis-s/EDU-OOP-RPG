using System;
using EDU_OOP_RPG.Spells.BaseSpells;
using EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces;

namespace EDU_OOP_RPG.Spells
{
    public class AddHealthSpell : AbstractSpell, IGradeTargetSpell
    {
        public AddHealthSpell(int manaCost, bool isVerbal, bool isMotor) : base(manaCost, isVerbal, isMotor)
        {
        }

        public void Cast(Character character, int grade)
        {
            int healthDifference = character.MaxHealth - character.CurrentHealth;
            int healthToHeal = Convert.ToInt32(Math.Floor((double) grade / 2));
            if (healthDifference != 0 && healthDifference >= healthToHeal)
            {
                character.CurrentHealth += healthToHeal;
            }
            else
            {
                character.CurrentHealth = character.MaxHealth;
            }
        }
    }
}