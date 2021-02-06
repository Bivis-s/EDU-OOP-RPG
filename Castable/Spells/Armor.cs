using System.Threading;
using EDU_OOP_RPG.Exceptions;
using EDU_OOP_RPG.Spells.BaseSpells;
using EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces;

namespace EDU_OOP_RPG.Spells
{
    public class Armor : AbstractSpell, IGradeTargetSpell
    {
        public Armor(int manaCost, bool isVerbal, bool isMotor) : base(manaCost, isVerbal, isMotor)
        {
        }

        public void Cast(Character character, int grade)
        {
            if (character.State == states.Dead)
            {
                throw new RpgException("Цель заклинания мертва");
            }

            Thread invulnerableTimer = new Thread(() =>
            {
                states oldState = character.State;
                character.State = states.Invulnerable;
                Thread.Sleep(1000 * grade);
                character.State = oldState;
            });
            invulnerableTimer.Start();
        }
    }
}