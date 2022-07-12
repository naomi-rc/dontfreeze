using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "SceneSO", menuName = "Scenes/SceneSO", order = 0)]
public class SceneSO : ScriptableObject
{
    [SerializeField]
    private AssetReference _reference;

    public AssetReference reference
    {
        get => _reference;
    }

    public string GetName()
    {
        return _reference.editorAsset.name;
    }

    // Other scene related data
}
