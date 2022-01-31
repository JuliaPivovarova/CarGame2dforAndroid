using System.Collections.Generic;
using Code.Controllers;

namespace Code.Items
{
    public class ItemsRepository: BaseController ,IItemsRepository
    {
        private Dictionary<int, IItem> _itemsMapById = new Dictionary<int, IItem>();
        private UpgrateItemConfig[] _upgrateItemConfigs;

        public IReadOnlyDictionary<int, IItem> Items => _itemsMapById;

        public ItemsRepository(List<ItemConfig> itemConfigs, UpgrateItemConfigDataSource itemConfigDataSource)
        {
            _upgrateItemConfigs = itemConfigDataSource.ItemConfigs;
            PopulateItems(itemConfigs);
        }

        protected override void OnDispose()
        {
            _itemsMapById.Clear();
        }

        private void PopulateItems(List<ItemConfig> itemConfigs)
        {
            foreach (var itemConfig in itemConfigs)
            {
                if (_itemsMapById.ContainsKey(itemConfig.Id)) continue;
                
                _itemsMapById.Add(itemConfig.Id, CreateItem(itemConfig));
            }
        }

        private IItem CreateItem(ItemConfig itemConfig)
        {
            var info = new ItemInfo();// { Title = itemConfig.Title }
            info.Title = itemConfig.Title;
            for (int i = 0; i < _upgrateItemConfigs.Length; i++)
            {
                if (itemConfig.Id == _upgrateItemConfigs[i].Id)
                {
                    info.Sprite = _upgrateItemConfigs[i].Sprite;
                }
            }
            return new Item
            {
                Id = itemConfig.Id,
                Info = info//new ItemInfo { Title = itemConfig.Title }
            };
        }
    }
}