using EDU_OOP_RPG.Artifacts;
using EDU_OOP_RPG.Exceptions;

namespace EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces.Artifacts
{
    public class PoisonousSaliva : AbstractArtifact, IGradeTargetSpell
    {
        public PoisonousSaliva(int capacity, bool reusable) : base(capacity, reusable)
        {
        }

        public void Cast(Character character, int grade)
        {
            if (Capacity > 0)
            {
                if (character.State == states.Dead)
                {
                    throw new RpgException("Цель мертва");
                }
                if (grade <= Capacity)
                {
                    if (character.HealthDifference() < grade)
                    {
                        character.CurrentHealth = 0;
                    }
                    else
                    {
                        character.CurrentHealth -= grade;
                        character.State = states.Poisoned;
                    }
                    Capacity -= grade;
                }
                else
                {
                    throw new RpgException($"Указана мощность {grade}, когда доступно {Capacity}");
                }
            }
            else
            {
                throw new RpgException("Ресур посоха исчерпан");
            }
        }
    }
}