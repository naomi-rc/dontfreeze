using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private FloatVariable speed;

    [SerializeField]
    private FloatReference initialSpeed;

    [SerializeField]
    private FloatReference coldDebuff;

    [SerializeField]
    private BoolEventChannel onColdEvent = default;

    private void Awake()
    {
        speed.value = initialSpeed.value;

        onColdEvent.OnEventRaised += SetCold;
    }

    public void SetCold(bool isCold)
    {
        if (isCold)
        {
            speed.value -= coldDebuff.value;
        }
        else
        {
            speed.value += coldDebuff.value;
        }
    }
}
