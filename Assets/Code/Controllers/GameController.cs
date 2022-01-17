using System.Collections.Generic;
using Code.Ability;
using Code.Inventory;
using Code.Items;
using Code.Model;
using UnityEngine;

namespace Code.Controllers
{
    public class GameController : BaseController
    {
        private readonly List<ItemConfig> _itemConfigs;
        private UpgrateItemConfigDataSource _itemConfigDataSource;
        private readonly List<AbilityItemConfig> _abilityItemConfigs;
        private readonly Transform _placeForAbilities;

        public GameController(ProfilePlayer profilePlayer, List<ItemConfig> itemConfigs, UpgrateItemConfigDataSource itemConfigDataSource, List<AbilityItemConfig> abilityItemConfigs, Transform placeForAbilities, Transform placeForInventory)
        {
            _itemConfigs = itemConfigs;
            _itemConfigDataSource = itemConfigDataSource;
            _abilityItemConfigs = abilityItemConfigs;
            _placeForAbilities = placeForAbilities;
            
            
            var leftMoveDiff = new SubscribeProperty<float>();
            var rightMoveDiff = new SubscribeProperty<float>();
            
            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);
            
            var carController = new CarController();
            AddController(carController);
            
            var inventoryController = new InventoryController(_itemConfigs, _itemConfigDataSource, placeForInventory);
            AddController(inventoryController);
            
            var abilityController = new AbilityController(_abilityItemConfigs, _placeForAbilities);
            AddController(abilityController);
        }
    }
}