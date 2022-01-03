using System.Collections.Generic;
using Code.Items;

namespace Code.Inventory
{
    public interface IInventoryView
    {
        void Display(IReadOnlyList<IItem> items);
    }
}