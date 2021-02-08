using EDU_OOP_RPG.Artifacts;
using EDU_OOP_RPG.Exceptions;

namespace EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces.Artifacts
{
    public class BasiliskEye : AbstractArtifact, ITargetSpell
    {
        public BasiliskEye(int capacity, bool reusable) : base(capacity, reusable)
        {
        }

        public void Cast(Character character)
        {
            if (Capacity > 0)
            {
                if (character.State != States.Dead) throw new RpgException("Цель не мертва");

                character.State = States.Paralyzed;

                if (!Reusable) Capacity = 0;
            }
        }
    }
}