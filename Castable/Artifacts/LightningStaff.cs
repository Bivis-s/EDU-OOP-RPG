using EDU_OOP_RPG.Artifacts;
using EDU_OOP_RPG.Exceptions;

namespace EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces.Artifacts
{
    public class LightningStaff : AbstractArtifact, IGradeTargetSpell
    {
        public LightningStaff(int capacity, bool reusable) : base(capacity, reusable)
        {
        }

        public void Cast(Character character, int grade)
        {
            if (Capacity > 0)
            {
                if (character.HealthDifference() > grade)
                {
                    character.CurrentHealth = 0;
                }
                else
                {
                    character.CurrentHealth -= grade;
                }

                Capacity -= grade;
            }
            else
            {
                throw new RpgException("Ресур посоха исчерпан");
            }
        }
    }
}