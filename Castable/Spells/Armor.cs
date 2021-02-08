using System.Threading;
using EDU_OOP_RPG.Exceptions;
using EDU_OOP_RPG.Spells.BaseSpells;
using EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces;

namespace EDU_OOP_RPG.Spells
{
    public class Armor : AbstractSpell, IGradeTargetSpell
    {
        public Armor()
        {
        }

        public Armor(int manaCost) : base(manaCost)
        {
        }

        public Armor(int manaCost, bool isVerbal, bool isMotor) : base(manaCost, isVerbal, isMotor)
        {
        }

        public void Cast(Character character, int grade)
        {
            if (character.State != States.Invulnerable)
            {
                if (character.State == States.Dead) throw new RpgException("Цель заклинания мертва");

                var oldState = character.State;
                character.State = States.Invulnerable;
                var invulnerableTimer = new Thread(() =>
                {
                    Thread.Sleep(1000 * grade);
                    character.State = oldState;
                });
                invulnerableTimer.Start();
            }
            else
            {
                throw new RpgException("Персонаж уже под эффектом брони!");
            }
        }
    }
}