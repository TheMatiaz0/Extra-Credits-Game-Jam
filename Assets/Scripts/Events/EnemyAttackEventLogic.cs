
using System.Linq;
using Cyberultimate;
using UnityEngine;

public class EnemyAttackEventLogic : EventLogic
{
    public override void Activate()
    {
        var rnd = new System.Random();
        var arr = Inventory.Instance.Items.OrderBy(x => rnd.Next()).ToArray();
        var item = arr.Select((x, i) => new {item = x, index = i}).FirstOrDefault();
        if (item == null) return;
        
        Inventory.Instance.RemoveItem((uint)item.index);
        
        UIManager.Instance.ShowDialogText($"Enemies attacked you and stole your {item.item.Name}");
    }
}
