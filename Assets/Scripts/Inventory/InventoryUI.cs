using Cyberultimate.Unity;
using UnityEngine;

public class InventoryUI : MonoSingleton<InventoryUI>
{
    private Slot[] slots = new Slot[5];
    private int currentlySelected = 0;

    public Sprite emptyImage;

    public void Start()
    {
        for(var i=0; i<transform.childCount; i++)
        {
            slots[i] = transform.GetChild(i).GetComponent<Slot>();
        }
        Refresh();
    }

    public void Refresh()
    {
        for (var i=0; i<slots.Length; i++)
        {
            var item = Inventory.Instance.GetItem(i);
            if (item == null)
            {
                slots[i].image.sprite = emptyImage;
            }
            else
            {
                slots[i].image.sprite = item.Icon;
            }
            slots[i].Selected = currentlySelected == i;
        }
    }

    private void Select(int i)
    {
        currentlySelected = i;
        Refresh();
    }

    private void Update()
    {
        var refresh = true;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Select(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Select(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Select(2);
        } else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Select(3);
        } else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Select(4);
        }
        else
        {
            refresh = false;
        }

        if (refresh) Refresh();
    }
}
