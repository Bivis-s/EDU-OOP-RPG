using EDU_OOP_RPG.Artifacts;
using EDU_OOP_RPG.Characters;
using EDU_OOP_RPG.Exceptions;

namespace EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces.Artifacts
{
    public class DeadWaterBottle : AbstractArtifact, ITargetSpell
    {
        public DeadWaterBottle(int capacity, bool reusable) : base(capacity, reusable)
        {
        }

        public void Cast(Character character)
        {
            if (character is Wizard)
            {
                var wizard = (Wizard) character;

                if (wizard.ManaDifference() < Capacity)
                    wizard.CurrentHealth = wizard.MaxHealth;
                else
                    wizard.CurrentHealth += Capacity;

                if (!Reusable) Capacity = 0;
            }
            else
            {
                throw new RpgException("Живая вода не действует на сущность не имеющую маны");
            }
        }
    }
}