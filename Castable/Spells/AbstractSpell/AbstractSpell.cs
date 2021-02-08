namespace EDU_OOP_RPG.Spells.BaseSpells
{
    public abstract class AbstractSpell
    {
        protected AbstractSpell()
        {
        }

        protected AbstractSpell(int manaCost)
        {
            ManaCost = manaCost;
        }

        public AbstractSpell(int manaCost, bool isVerbal, bool isMotor)
        {
            ManaCost = manaCost;
            IsVerbal = isVerbal;
            IsMotor = isMotor;
        }

        public int ManaCost { get; set; }

        public bool IsVerbal { get; set; }

        public bool IsMotor { get; set; }

        public override string ToString()
        {
            return GetType().Name + " Mana cost: " + ManaCost;
        }
    }
}