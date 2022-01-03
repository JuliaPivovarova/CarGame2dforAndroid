using System.Collections.Generic;

namespace Code.Items
{
    public interface IItemsRepository
    {
        IReadOnlyDictionary<int, IItem> Items { get; }
    }
}