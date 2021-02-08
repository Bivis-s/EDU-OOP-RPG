using EDU_OOP_RPG.Artifacts;
using EDU_OOP_RPG.Exceptions;

namespace EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces.Artifacts
{
    public class HealingArtifact : AbstractArtifact, IGradeTargetSpell
    {
        public HealingArtifact(int capacity, bool reusable) : base(capacity, reusable)
        {
        }

        public void Cast(Character character, int grade)
        {
            if (Capacity > 0)
            {
                if (character.State == States.Dead) throw new RpgException("Цель мертва");
                if (grade <= Capacity)
                {
                    var addHealthSpell = new AddHealthSpell(Capacity);
                    addHealthSpell.Cast(character, grade);
                    Capacity -= grade;
                }
                else
                {
                    throw new RpgException($"Указана мощность {grade}, когда доступно {Capacity}");
                }
            }
            else
            {
                throw new RpgException("Ресурс артефакта лечения исчерпан");
            }
        }
    }
}