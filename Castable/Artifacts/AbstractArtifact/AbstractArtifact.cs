namespace EDU_OOP_RPG.Artifacts
{
    public class AbstractArtifact
    {
        public AbstractArtifact(int capacity, bool reusable)
        {
            Capacity = capacity;
            Reusable = reusable;
        }

        public int Capacity { get; set; }

        public bool Reusable { get; set; }

        public override string ToString()
        {
            return GetType().Name + " " + Capacity + " reusable: " + Reusable;
        }
    }
}