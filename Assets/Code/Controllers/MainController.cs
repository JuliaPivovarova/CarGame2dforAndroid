using System.Collections.Generic;
using Code.Ability;
using Code.Inventory;
using Code.Items;
using Code.Model;
using UnityEngine;

namespace Code.Controllers
{
    public class MainController: BaseController
    {
        private MainMenuController _mainMenuController;
        private GameController _gameController;
        private InventoryController _inventoryController;
        private readonly Transform _placeForUI;
        private readonly Transform _placeForVideo;
        private readonly Transform _placeForInventory;
        private readonly ProfilePlayer _profilePlayer;
        private readonly List<ItemConfig> _itemConfigs;
        private UpgrateItemConfigDataSource _itemConfigDataSource;
        private InventoryView _inventoryView;
        private readonly List<AbilityItemConfig> _abilityItemConfigs;
        private readonly Transform _placeForAbilities;
        private AbilityController _abilityController;

        public MainController(Transform placeForUI, Transform placeForVideo, ProfilePlayer profilePlayer, List<ItemConfig> itemConfigs, UpgrateItemConfigDataSource itemConfigDataSource, List<AbilityItemConfig> abilityItemConfigs, Transform placeForAbilities, Transform placeForInventory)
        {
            _placeForUI = placeForUI;
            _placeForVideo = placeForVideo;
            _profilePlayer = profilePlayer;
            _placeForInventory = placeForInventory;
            _itemConfigDataSource = itemConfigDataSource;
            _abilityItemConfigs = abilityItemConfigs;
            _placeForAbilities = placeForAbilities;
            _itemConfigs = itemConfigs;
            OnChangeGameState(_profilePlayer.CurrentState.Value);
            _profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        }

        private void OnChangeGameState(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
                    _mainMenuController = new MainMenuController(_placeForUI, _placeForVideo, _profilePlayer);
                    _gameController?.Dispose();
                    break;
                case GameState.Game:
                    _gameController = new GameController(_profilePlayer, _itemConfigs, _itemConfigDataSource, _abilityItemConfigs, _placeForAbilities, _placeForInventory);
                    _mainMenuController?.Dispose();
                    break;
                default:
                    _mainMenuController?.Dispose();
                    _gameController?.Dispose();
                    break;
            }
        }

        protected override void OnDispose()
        {
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
            _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
            base.OnDispose();
        }
    }
}