using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterPump : InteractableObject
{
    public override string InteractionName => "Collect water";

    public uint waterAmount = 30;
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
            AudioManager.Instance.PlaySFX("pump1");
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

        if (Inventory.Instance.CheckNotSelectedTrueItem(neededItemTag, selectedItem))
		{
            UIManager.Instance.ShowPopupText("You need to select the bottle item");
            return;
		}


        Inventory.Instance.AddResource(waterAmount, PlantSystem.PlantResources.Water);
        selectedItem?.Durability?.TakeValue(1);
        InventoryUI.Instance.Refresh();
    }
}
