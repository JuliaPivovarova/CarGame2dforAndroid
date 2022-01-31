using System;
using System.Collections.Generic;
using Code.Controllers;
using Code.Items;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Inventory
{
    public class InventoryController: BaseController, IInventoryController
    {
        private readonly ResourcesPath _inventoryUIPath = new ResourcesPath { PathResources = "Prefabs/InventoryUI" };
        
        private readonly InventoryModel _inventoryModel;
        private readonly InventoryView _inventoryView;
        private readonly IItemsRepository _itemsRepository;

        public InventoryController(List<ItemConfig> itemConfigs, UpgrateItemConfigDataSource itemConfigDataSource, Transform placeForInventory)
        {
            _inventoryModel = new InventoryModel();
            _inventoryView = LoadInventoryUI(placeForInventory);
            _itemsRepository = new ItemsRepository(itemConfigs, itemConfigDataSource);
            ShowInventory();
        }

        private InventoryView LoadInventoryUI(Transform placeForInventory)
        {
            var inventoryView = Object.Instantiate(ResourcesLoader.LoadPrefab(_inventoryUIPath), placeForInventory, false);
            AddGameObject(inventoryView);
            
            var tryAbilityButtonViewObj = inventoryView.TryGetComponent<InventoryView>(out var inventoryViewObject);
            if (!tryAbilityButtonViewObj)
            {
                throw new Exception("There is no AbilityButtonView component found");
            }

            return inventoryViewObject;
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