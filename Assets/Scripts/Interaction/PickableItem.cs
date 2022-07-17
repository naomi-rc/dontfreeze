using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public InventoryItem inventoryItem;

    public GameObject pickableItemModel;

    private void Update()
    {
        pickableItemModel.transform.Rotate(new Vector3(0, 1, 0));
    }
}
