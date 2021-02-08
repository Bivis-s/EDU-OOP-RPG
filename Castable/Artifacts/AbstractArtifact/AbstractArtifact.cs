namespace EDU_OOP_RPG.Artifacts
{
    public class AbstractArtifact
    {
        private int capacity;
        private bool reusable;

        public AbstractArtifact(int capacity, bool reusable)
        {
            this.capacity = capacity;
            this.reusable = reusable;
        }

        public int Capacity
        {
            get => capacity;
            set => capacity = value;
        }

        public bool Reusable
        {
            get => reusable;
            set => reusable = value;
        }

        public override string ToString()
        {
            return GetType().Name + " " + Capacity + " reusable: " + Reusable;
        }
    }
}