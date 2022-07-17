namespace AI
{
    public class BehaviorTree : Node
    {
        public BehaviorTree() : base("Tree") { }

        public BehaviorTree(string n) : base(n) { }

        public override Status Process()
        {
            return children[currentChild].Process();
        }
    }
}
