using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDHandler : MonoBehaviour
{

    // Todo: use a database for managing status effect (similar to inventory)
    private Dictionary<StatusEffect, StatusEffectSlot> statusEffects = new Dictionary<StatusEffect, StatusEffectSlot>();

    private BaseBar healthBar;
    private VisualElement distance;
    private Label distanceLabel;
    private VisualElement compass;
    private VisualElement statuses;
    private GameObject checkpoint;
    private GameObject player;

    [SerializeField]
    private FloatVariable playerHealth = default;

    [SerializeField]
    private StatusEventChannel playerStatusEventChannel = default;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var rootElement = GetComponent<UIDocument>().rootVisualElement;

        healthBar = rootElement.Q<BaseBar>("HealthBar");
        distance = rootElement.Q<VisualElement>("Distance");
        distanceLabel = distance.Q<Label>("DistanceLabel");
        compass = rootElement.Q<VisualElement>("Compass");
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

        var checkpoint = GameObject.FindGameObjectWithTag("Checkpoint");
        if (checkpoint != null)
        {
            this.checkpoint = checkpoint;
        }
        else
        {
            distance.style.display = DisplayStyle.None;
            compass.style.display = DisplayStyle.None;
            Debug.Log("Checkpoint not found!");
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

    private void Update()
    {
        if (checkpoint != null && player != null)
        {
            distanceLabel.text = Vector3.Distance(player.transform.position, checkpoint.transform.position).ToString("F0") + "m";

            var rotationDegrees = Vector3.Angle(player.transform.forward, checkpoint.transform.position - player.transform.position);
            if (Vector3.Dot(player.transform.right, checkpoint.transform.position - player.transform.position) < 0)
            {
                rotationDegrees = 360.0f - rotationDegrees;
            }
            if (Vector3.Dot(player.transform.forward, checkpoint.transform.position - player.transform.position) < 0)
            {
                rotationDegrees = 180.0f - rotationDegrees;
            }

            compass.style.rotate = new StyleRotate(new Rotate(rotationDegrees));
        }
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
