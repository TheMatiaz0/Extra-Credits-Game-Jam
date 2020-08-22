using Cyberultimate;
using Cyberultimate.Unity;
using UnityEngine;

public class InventoryUI : MonoSingleton<InventoryUI>
{
	private Slot[] slots = new Slot[5];
	private Cint selectedSlot = 0;

	public Sprite emptyImage;
	public Sprite testImage;

	private const int alphaKeyCodesOffset = 49;

	public void Start()
	{
		for (var i = 0; i < transform.childCount; i++)
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
				slots[i].image.sprite = item.icon;
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
		if (Input.GetKeyDown(KeyCode.G))
		{
			var item = Inventory.Instance.AllGameItems["Shovel"];

			Inventory.Instance.AddItem(item);
		}
		else if (Input.GetKeyDown(KeyCode.H))
		{
			Inventory.Instance.RemoveItem(selectedSlot);
		}

		ScrollWheel();
		AlphaKeys();
	}

	private void ScrollWheel()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			selectedSlot += selectedSlot < 4 ? (Cint)1 : Cint.Zero;
			Refresh();
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			selectedSlot--;
			Refresh();
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

		if (refresh) Refresh();
	}
}
