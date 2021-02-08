using EDU_OOP_RPG.Exceptions;
using EDU_OOP_RPG.Spells.BaseSpells;
using EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces;

namespace EDU_OOP_RPG.Spells
{
    public class Unparalyze : AbstractSpell, ITargetSpell
    {
        public Unparalyze(int manaCost) : base(manaCost)
        {
        }

        public Unparalyze(int manaCost, bool isVerbal, bool isMotor) : base(manaCost, isVerbal, isMotor)
        {
        }

        public void Cast(Character character)
        {
            if (character.State != States.Paralyzed) throw new RpgException("Цель заклинания не парализована");

            character.CurrentHealth = 1;
            character.State = States.Weakened;
        }
    }
}