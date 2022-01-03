using System.Collections.Generic;
using Code.Controllers;
using Code.Items;

namespace Code.Inventory
{
    public class InventoryController: BaseController, IInventoryController
    {
        private readonly InventoryModel _inventoryModel;
        private readonly InventoryView _inventoryView;
        private readonly IItemsRepository _itemsRepository;
        
        public InventoryController(List<ItemConfig> itemConfigs, UpgrateItemConfigDataSource itemConfigDataSource, InventoryView inventoryView)
        {
            _inventoryModel = new InventoryModel();
            _inventoryView = inventoryView;
            _itemsRepository = new ItemsRepository(itemConfigs, itemConfigDataSource);
        }
        
        public void ShowInventory()
        {
            foreach (var item in _itemsRepository.Items.Values)
            {
                _inventoryModel.EquipItem(item);
            }

            var equippedItem = _inventoryModel.GetEquippedItems();
            _inventoryView.Display(equippedItem);
        }

        public void HideInventory()
        {
            
        }
    }
}