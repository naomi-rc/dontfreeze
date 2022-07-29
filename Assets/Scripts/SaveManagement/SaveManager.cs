using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private string fileName = "SaveGameInfo.frz";

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
        if (scene.type != SceneType.Level) return;

        Debug.Log("Updating save...");

        saveObject.playerLocation = scene;

        saveObject.inventoryEntries.Clear();
        foreach (var entry in playerInventory.entries)
        {
            saveObject.inventoryEntries.Add(entry);
        }

        FileManager.Write(fileName, saveObject.ToJson());
    }

    private void LoadSave()
    {
        FileManager.Read(fileName, out string json);
        saveObject.FromJsonOverwrite(json);

        if (!saveObject.isValid())
        {
            Debug.LogWarningFormat("The save file failed to produce a valid game state.");
            return;
        }

        playerInventory.entries.Clear();
        foreach (var entry in saveObject.inventoryEntries)
        {
            playerInventory.entries.Add(entry);
        }

        onLoadEvent.Raise(saveObject.playerLocation);
    }
}
