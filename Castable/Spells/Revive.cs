using EDU_OOP_RPG.Exceptions;
using EDU_OOP_RPG.Spells.BaseSpells;
using EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces;

namespace EDU_OOP_RPG.Spells
{
    public class Revive : AbstractSpell, ITargetSpell
    {
        public Revive(int manaCost, bool isVerbal, bool isMotor) : base(manaCost, isVerbal, isMotor)
        {
        }

        public void Cast(Character character)
        {
            if (character.State != states.Dead)
            {
                throw new RpgException("Цель заклинания не мертва");
            }

            character.CurrentHealth = 1;
            character.State = states.Weakened;
        }
    }
}