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
                if (grade <= Capacity)
                {
                    AddHealthSpell addHealthSpell = new AddHealthSpell(Capacity);
                    int spentCapacity = grade - character.HealthDifference();
                    addHealthSpell.Cast(character, grade);
                    Capacity -= spentCapacity;
                }
            }
            else
            {
                throw new RpgException("Не хватает ресурса артефакта");
            }
        }
    }
}