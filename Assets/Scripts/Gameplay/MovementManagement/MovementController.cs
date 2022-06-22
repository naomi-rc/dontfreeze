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
    private PermanentStatus coldStatusEffect = default;

    private void Awake()
    {
        speed.value = initialSpeed.value;

        coldStatusEffect.OnActivateEvent += ApplyCold;
        coldStatusEffect.OnDeactivateEvent += RemoveCold;
    }

    private void OnDisable()
    {
        coldStatusEffect.OnActivateEvent -= ApplyCold;
        coldStatusEffect.OnDeactivateEvent -= RemoveCold;
    }

    public void ApplyCold()
    {
        speed.value -= coldStatusEffect.value;
    }
    public void RemoveCold()
    {
        speed.value += coldStatusEffect.value;
    }
}
