using EDU_OOP_RPG.Artifacts;
using EDU_OOP_RPG.Exceptions;

namespace EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces.Artifacts
{
    public class FrogLegsDecoct : AbstractArtifact, ITargetSpell
    {
        public FrogLegsDecoct(int capacity, bool reusable) : base(capacity, reusable)
        {
        }

        public void Cast(Character character)
        {
            if (Capacity > 0)
            {
                if (character.State != States.Poisoned) throw new RpgException("Цель не отравлена");

                character.State = States.Weakened;

                if (!Reusable) Capacity = 0;
            }
        }
    }
}