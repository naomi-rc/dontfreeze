namespace AI
{
    public class Sequence : Node
    {
        public Sequence() : base("Sequence") { }
        public Sequence(string name) : base(name) { }
        public override Status Process()
        {
            foreach (var c in children)
            {
                Status s = c.Process();
                if (s == Status.Running || s == Status.Failure)
                {
                    return s;
                }

            }
            return Status.Success;
        }
    }
}