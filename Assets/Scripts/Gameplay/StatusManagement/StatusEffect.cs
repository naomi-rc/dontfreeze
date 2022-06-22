using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IBaseEffect
{
}

public abstract class StatusEffect : ScriptableObject, IBaseEffect
{
    [SerializeField]
    private string _title;

    [SerializeField]
    private string _description;

    [SerializeField]
    private Texture2D _icon;

    [SerializeField]
    private StatusEventChannel statusEventChannel = default;

    public UnityAction OnActivateEvent;
    public UnityAction OnDeactivateEvent;

    public string title
    {
        get => _title;
    }

    public string description
    {
        get => _description;
    }

    public Texture2D icon
    {
        get => _icon;
    }

    public void Activate()
    {
        OnActivateEvent?.Invoke();
        statusEventChannel.RaiseStatusApplied(this);
    }

    public void Deactivate()
    {
        OnDeactivateEvent?.Invoke();
        statusEventChannel.RaiseStatusRemoved(this);
    }
}
