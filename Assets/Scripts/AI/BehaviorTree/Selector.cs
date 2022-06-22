public class Selector : Node
{
    public Selector() : base("Selector") { }
    public Selector(string name) : base(name) { }
    public override Status Process()
    {
        foreach (var c in children)
        {
            Status s = c.Process();
            if (s == Status.Running || s == Status.Success)
            {
                return s;
            }
                
        }
        return Status.Failure;
    }
}
