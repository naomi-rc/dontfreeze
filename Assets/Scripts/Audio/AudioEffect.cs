using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    private void Step()
    {
        FindObjectOfType<AudioManager>().Play("Moving");
    }

    private void Land()
    {
        FindObjectOfType<AudioManager>().Play("Landing");
    }

    private void Defending()
    {
        FindObjectOfType<AudioManager>().Play("Defense");
    }

    private void Jump()
    {
        FindObjectOfType<AudioManager>().Play("Jump");
    }
}
