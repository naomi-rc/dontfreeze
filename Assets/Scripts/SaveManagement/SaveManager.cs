using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private SceneEventChannel onLoadEvent = default;

    [SerializeField]
    private VoidEventChannel onLoadSaveEvent = default;

    [SerializeField]
    private InventoryDatabase playerInventory;

    private SaveObject saveObject = new SaveObject();

    private void OnEnable()
    {
        onLoadEvent.OnEventRaised += UpdateSave;
        onLoadSaveEvent.OnEventRaised += LoadSave;
    }

    private void OnDisable()
    {
        onLoadEvent.OnEventRaised -= UpdateSave;
        onLoadSaveEvent.OnEventRaised -= LoadSave;
    }

    private void UpdateSave(SceneObject scene)
    {
        Debug.Log("Updating save..");

        saveObject.playerLocation = scene;

        saveObject.inventoryEntries.Clear();
        foreach (var entry in playerInventory.entries)
        {
            saveObject.inventoryEntries.Add(entry);
        }

        FileManager.Write(saveObject.ToJson());
    }

    private void LoadSave()
    {
        FileManager.Read(out string json);
        saveObject.FromJsonOverwrite(json);

        playerInventory.entries.Clear();
        foreach (var entry in saveObject.inventoryEntries)
        {
            playerInventory.entries.Add(entry);
        }

        onLoadEvent.Raise(saveObject.playerLocation);
    }
}
