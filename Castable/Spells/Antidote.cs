using EDU_OOP_RPG.Exceptions;
using EDU_OOP_RPG.Spells.BaseSpells;
using EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces;

namespace EDU_OOP_RPG.Spells
{
    public class Antidote : AbstractSpell, ITargetSpell
    {
        public Antidote(int manaCost) : base(manaCost)
        {
        }
        
        public Antidote(int manaCost, bool isVerbal, bool isMotor) : base(manaCost, isVerbal, isMotor)
        {
        }

        public void Cast(Character character)
        {
            if (character.State != states.Poisoned)
            {
                throw new RpgException("Цель заклинания не отравлена");
            }

            character.State = states.Weakened;
        }
    }
}