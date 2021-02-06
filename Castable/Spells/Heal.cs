using EDU_OOP_RPG.Exceptions;
using EDU_OOP_RPG.Spells.BaseSpells;
using EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces;

namespace EDU_OOP_RPG.Spells
{
    public class Heal : AbstractSpell, ITargetSpell
    {
        public Heal(int manaCost) : base(manaCost)
        {
        }

        public Heal(int manaCost, bool isVerbal, bool isMotor) : base(manaCost, isVerbal, isMotor)
        {
        }

        public void Cast(Character character)
        {
            if (character.State != states.Ill)
            {
                throw new RpgException("Невозможно исцелить цель заклинания");
            }

            character.State = states.Normal;
        }
    }
}