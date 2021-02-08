namespace EDU_OOP_RPG.Spells.BaseSpells
{
    public abstract class AbstractSpell
    {
        private int manaCost;
        private bool isVerbal;
        private bool isMotor;

        protected AbstractSpell()
        {
        }

        protected AbstractSpell(int manaCost)
        {
            this.manaCost = manaCost;
        }

        public AbstractSpell(int manaCost, bool isVerbal, bool isMotor)
        {
            this.manaCost = manaCost;
            this.isVerbal = isVerbal;
            this.isMotor = isMotor;
        }

        public int ManaCost
        {
            get => manaCost;
            set => manaCost = value;
        }

        public bool IsVerbal
        {
            get => isVerbal;
            set => isVerbal = value;
        }

        public bool IsMotor
        {
            get => isMotor;
            set => isMotor = value;
        }
        
        public override string ToString()
        {
            return GetType().Name + " Mana cost: " + ManaCost;
        }
    }
}