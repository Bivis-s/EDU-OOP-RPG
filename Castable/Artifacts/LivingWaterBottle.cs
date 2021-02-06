using EDU_OOP_RPG.Artifacts;

namespace EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces.Artifacts
{
    public class LivingWaterBottle : AbstractArtifact, ITargetSpell
    {
        public LivingWaterBottle(int capacity, bool reusable) : base(capacity, reusable)
        {
        }

        public void Cast(Character character)
        {
            if (character.HealthDifference() > Capacity)
            {
                character.CurrentHealth = character.MaxHealth;
            }
            else
            {
                character.CurrentHealth += Capacity;
            }

            if (!Reusable)
            {
                Capacity = 0;
            }
        }
    }
}