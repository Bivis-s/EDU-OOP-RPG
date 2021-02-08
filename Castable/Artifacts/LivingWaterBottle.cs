using EDU_OOP_RPG.Artifacts;
using EDU_OOP_RPG.Exceptions;

namespace EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces.Artifacts
{
    public class LivingWaterBottle : AbstractArtifact, ITargetSpell
    {
        public LivingWaterBottle(int capacity, bool reusable) : base(capacity, reusable)
        {
        }

        public void Cast(Character character)
        {
            if (Capacity > 0)
            {
                if (character.HealthDifference() < Capacity)
                    character.CurrentHealth = character.MaxHealth;
                else
                    character.CurrentHealth += Capacity;

                if (!Reusable) Capacity = 0;
            }
            else
            {
                throw new RpgException("Ресурс артефакта исчерпан");
            }
        }
    }
}