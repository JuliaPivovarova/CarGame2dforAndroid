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

        public GameController(ProfilePlayer profilePlayer, List<ItemConfig> itemConfigs, UpgrateItemConfigDataSource itemConfigDataSource, List<AbilityItemConfig> abilityItemConfigs, Transform placeForUI)
        {
            _itemConfigs = itemConfigs;
            _itemConfigDataSource = itemConfigDataSource;
            _abilityItemConfigs = abilityItemConfigs;


            var leftMoveDiff = new SubscribeProperty<float>();
            var rightMoveDiff = new SubscribeProperty<float>();
            
            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);
            
            var carController = new CarController();
            AddController(carController);
            
            var inventoryController = new InventoryController(_itemConfigs, _itemConfigDataSource, placeForUI);
            AddController(inventoryController);
            
            var abilityController = new AbilityController(_abilityItemConfigs, placeForUI);
            AddController(abilityController);
        }
    }
}