using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDHandler : MonoBehaviour
{

    // Todo: use a database for managing status effect (similar to inventory)
    private Dictionary<StatusEffect, StatusEffectSlot> statusEffects = new Dictionary<StatusEffect, StatusEffectSlot>();

    private BaseBar healthBar;
    private VisualElement statuses;

    [SerializeField]
    private FloatVariable playerHealth = default;

    [SerializeField]
    private StatusEventChannel playerStatusEventChannel = default;

    private void OnEnable()
    {
        var rootElement = GetComponent<UIDocument>().rootVisualElement;

        healthBar = rootElement.Q<BaseBar>("HealthBar");
        UpdateHealthBar(playerHealth.value);

        statuses = rootElement.Q<VisualElement>("StatusBar");
        foreach (var slot in statusEffects.Values)
        {
            statuses.Add(slot);
        }

        if (playerHealth is not null)
        {
            playerHealth.OnValueChanged += UpdateHealthBar;
        }

        playerStatusEventChannel.OnStatusAppliedEvent += AddStatusEffect;
        playerStatusEventChannel.OnStatuRemovedEvent += RemoveStatusEffect;
    }

    private void OnDisable()
    {
        if (playerHealth is not null)
        {
            playerHealth.OnValueChanged -= UpdateHealthBar;
        }

        playerStatusEventChannel.OnStatusAppliedEvent -= AddStatusEffect;
        playerStatusEventChannel.OnStatuRemovedEvent -= RemoveStatusEffect;
    }

    private void UpdateHealthBar(float value)
    {
        // We assume the value is a percentage
        healthBar.value = value;
    }

    private void AddStatusEffect(StatusEffect status)
    {
        if (!statusEffects.ContainsKey(status))
        {
            StatusEffectSlot slot = new StatusEffectSlot();

            slot.SetStatus(status);

            statuses.Add(slot);

            statusEffects.Add(status, slot);
        }

    }
    private void RemoveStatusEffect(StatusEffect status)
    {
        StatusEffectSlot slot;
        if (statusEffects.TryGetValue(status, out slot))
        {
            statuses.Remove(slot);
        }

        statusEffects.Remove(status);
    }
}
