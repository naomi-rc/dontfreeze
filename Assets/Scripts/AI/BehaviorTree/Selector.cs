using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                //Debug.Log($"Node {c.name} is {s}");
                return s;
            }
                
        }
        return Status.Failure;
    }
}
