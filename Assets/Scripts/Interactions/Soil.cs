using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Soil : InteractableObject
{
    public override string InteractionName => "Dig soil";

    public uint soilAmount = 30;
    private float interactionTimeStateful;

    protected void Awake()
    {
        interactionTimeStateful = interactionTime;
    }

    public override void KeyDown()
    {
        base.KeyDown();

        if (GameManager.Instance.StaminaSys.Stamina.Value > 0)
        {
            AudioManager.Instance.PlaySFX("shovel");
        }

        Item it = null;
        if ((it = Inventory.Instance.GetItemCurrentlySelected()) != null)
        {
            interactionTime = interactionTimeStateful;
            interactionTime -= it.UseTakeLessTime;
        }
    }

    protected override void OnInteract()
    {
        Item selectedItem = Inventory.Instance.GetItemCurrentlySelected();
        string neededItemTag = itemsNeeded[0][0].tag;

        if (Inventory.Instance.GetAllItemsByTag(neededItemTag).Any(x => x != selectedItem))
        {
            UIManager.Instance.ShowPopupText("You need to select the shovel item");
            return;
        }

        Inventory.Instance.AddResource(soilAmount, PlantSystem.PlantResources.Soil); //zależnie od rodzaju łopaty
        selectedItem.Durability.TakeValue(1);
        InventoryUI.Instance.Refresh();
    }
}
