using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    private void Step()
    {
        FindObjectOfType<AudioManager>().Play("Moving");
        //Debug.Log("Step true");
    }

    private void Land()
    {
        FindObjectOfType<AudioManager>().Play("Landing");
        //Debug.Log("Landing true");
    }

    private void Jump()
    {
        FindObjectOfType<AudioManager>().Play("Jump");
        Debug.Log("Jumping true");
    }
}
