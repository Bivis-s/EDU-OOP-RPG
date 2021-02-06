using EDU_OOP_RPG.Artifacts;
using EDU_OOP_RPG.Exceptions;

namespace EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces.Artifacts
{
    public class PoisonousSaliva : AbstractArtifact, ITargetSpell
    {
        public PoisonousSaliva(int capacity, bool reusable) : base(capacity, reusable)
        {
        }
        
        public void Cast(Character character)
        {
            if (Capacity > 0)
            {
                if (character.State == states.Dead)
                {
                    throw new RpgException("Цель мертва");
                }

                int spentCapacity;
                if (character.HealthDifference() >= Capacity)
                {
                    spentCapacity = Capacity - character.HealthDifference();
                    character.CurrentHealth = 0;
                }
                else
                {
                    character.CurrentHealth -= Capacity;
                    spentCapacity = Capacity;
                }

                Capacity -= spentCapacity;
            }
        }
    }
}