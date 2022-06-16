using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                //Debug.Log($"Node {c.name} is {s}");
                return s;
            }
                
        }
        return Status.Success;
    }
}
