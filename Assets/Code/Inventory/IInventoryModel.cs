using System.Collections.Generic;
using Code.Items;

namespace Code.Inventory
{
    public interface IInventoryModel
    {
        IReadOnlyList<IItem> GetEquippedItems();
        void EquipItem(IItem item);
        void UEquipItem(IItem item);
    }
}