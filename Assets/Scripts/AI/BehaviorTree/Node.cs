using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
    public enum Status { Success, Failure, Running }
    public Status status;
    public List<Node> children = new List<Node>();
    protected int currentChild = 0;
    public string name { get; }

    public Node(string n)
    {
        name = n;
    }

    public void AddChild(Node node)
    {
        children.Add(node);
    }

    public abstract Status Process();
}
