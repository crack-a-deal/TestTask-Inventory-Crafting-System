using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private InventoryCollection inventory;
    [SerializeField] private CraftCollection craft;
    [SerializeField] private CraftItem item;

    [Inject]
    private InventoryService inventoryService;

    [Inject]
    private CraftingService craftingService;

    private void Awake()
    {
        inventory = inventoryService.Inventory;
        craft = craftingService.CraftingModel;
        item = craftingService.Item;
    }
}
