using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    protected State currentState;
    [SerializeField]
    public InputReader inputReader;

    [SerializeField]
    public Animator animator;

    private void Awake()
    {
        inputReader.EnableGameplayInput();
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentState = GetInitialState();
        if(currentState != null)
        {
            currentState.Enter();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(currentState != null)
        {
            currentState.Update();
        }
    }

    public void ChangeState(State newState)
    {
        //Debug.Log("Ancien state = " + currentState.ToString());

        currentState.Exit();

        currentState = newState;
        //Debug.Log("Nouveau state = " + currentState.ToString());

        currentState.Enter();
    }

    protected virtual State GetInitialState()
    {
        return null;
    }
}
