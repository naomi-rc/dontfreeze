using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private float speed;
    public float Speed
    {
        get => speed;
    }

    [SerializeField]
    private FloatReference defaultSpeed;

    [SerializeField]
    private FloatReference sprintSpeed;

    [SerializeField]
    private PermanentStatus coldStatusEffect = default;

    private void Awake()
    {
        speed = defaultSpeed.value;

        coldStatusEffect.OnActivateEvent += ApplyCold;
        coldStatusEffect.OnDeactivateEvent += RemoveCold;
    }

    private void OnDisable()
    {
        coldStatusEffect.OnActivateEvent -= ApplyCold;
        coldStatusEffect.OnDeactivateEvent -= RemoveCold;
    }

    public void Run()
    {
        speed = sprintSpeed.value;
    }

    public void Walk()
    {
        speed = defaultSpeed.value;
    }

    public void ApplyCold()
    {
        speed -= coldStatusEffect.value;
    }
    public void RemoveCold()
    {
        speed += coldStatusEffect.value;
    }
}
