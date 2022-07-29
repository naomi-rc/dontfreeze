using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "SceneObject", menuName = "Scenes/SceneObject", order = 0)]
public class SceneObject : ScriptableObject
{
    [SerializeField]
    private AssetReference _reference;
    public AssetReference reference
    {
        get => _reference;
    }

    public SceneType type = SceneType.None;

    // Other scene related data
}

public enum SceneType
{
    Cutscene,
    Level,
    Menu,
    None
}