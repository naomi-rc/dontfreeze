using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    public Func<Status> ProcessFct;

    public Leaf(string name, Func<Status> processFct) : base(name)
    {
        ProcessFct = processFct;
    }

    public override Status Process()
    {
        if (ProcessFct != null)
            return ProcessFct.Invoke();

        return Status.Failure;
    }
}
