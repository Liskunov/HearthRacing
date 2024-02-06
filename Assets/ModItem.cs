using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModItem : MonoBehaviour
{
    public Slider slider { private set; get; }
    public InventorySlot inventorySlot { private set; get; }

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        inventorySlot = GetComponentInChildren<InventorySlot>();
    }
}
