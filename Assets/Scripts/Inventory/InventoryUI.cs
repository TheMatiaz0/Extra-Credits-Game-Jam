using Cyberultimate;
using Cyberultimate.Unity;
using System;
using UnityEngine;

public class InventoryUI : MonoSingleton<InventoryUI>
{
	private Slot[] slots = new Slot[5];
	private Cint selectedSlot = 0;

	public Sprite emptyImage;
	public Sprite testImage;

	private const int alphaKeyCodesOffset = 49;

	private const string pressToUse = "Press RMB to Use";

	public void Start()
	{
		for (var i = 0; i < 5; i++)
		{
			slots[i] = transform.GetChild(i).GetComponent<Slot>();
		}
		Refresh();
	}

	public void Refresh()
	{
		for (var i = 0; i < slots.Length; i++)
		{
			var item = Inventory.Instance.GetItem((Cint)(uint)i);
			if (item == null)
			{
				slots[i].image.sprite = emptyImage;
			}
			else
			{
				slots[i].image.sprite = item.Icon;
			}
			slots[i].Selected = selectedSlot == i;
		}
	}

	private void Select(Cint i)
	{
		selectedSlot = i;
		Refresh();
	}

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Q))
		{
            Inventory.Instance.RemoveItem(selectedSlot,true);
        }

		if (Input.GetMouseButtonDown(1))
		{
			Cint slot = 0;
			Item it = null;

			it = Inventory.Instance.GetItem(slot = selectedSlot);

			if (it == null)
			{
				return;
			}


			it.Logic?.Do();
			if (it.Logic?.remove ?? false)
			{
				Inventory.Instance.RemoveItem(slot);
			}
		}

		ScrollWheel();
		AlphaKeys();
	}

	private void ScrollWheel()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			selectedSlot--;
			Refresh();
			ShowCurrentItem();
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			selectedSlot += selectedSlot < 4 ? (Cint)1 : Cint.Zero;
			Refresh();
			ShowCurrentItem();
		}
	}

	private void AlphaKeys()
	{
		var refresh = false;
		for (var i = alphaKeyCodesOffset; i < alphaKeyCodesOffset + 6; i++)
		{
			if (Input.GetKeyDown((KeyCode)i))
			{
				Select((Cint)(uint)i - alphaKeyCodesOffset);
				refresh = true;
			}
		}

		if (refresh)
		{
			Refresh();
			ShowCurrentItem();
		}
	}

	private void ShowCurrentItem()
	{
		var it = Inventory.Instance.GetItem(selectedSlot);
		if (it?.Useable ?? false)
		{
			UIManager.Instance.ShowPopupText($"{it?.Name} ({pressToUse})" ?? "");
		}
		else
		{
			UIManager.Instance.ShowPopupText($"{it?.Name}" ?? "");
		}

	}
}
