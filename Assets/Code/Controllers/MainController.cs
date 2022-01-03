using System.Collections.Generic;
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
        private readonly Transform _inventory;
        private readonly ProfilePlayer _profilePlayer;
        private readonly List<ItemConfig> _itemConfigs;
        private UpgrateItemConfigDataSource _itemConfigDataSource;
        private InventoryView _inventoryView;

        public MainController(Transform placeForUI, Transform placeForVideo, ProfilePlayer profilePlayer, List<ItemConfig> itemConfigs, Transform inventory, UpgrateItemConfigDataSource itemConfigDataSource)
        {
            _placeForUI = placeForUI;
            _placeForVideo = placeForVideo;
            _profilePlayer = profilePlayer;
            _inventory = inventory;
            _inventoryView = inventory.GetComponent<InventoryView>();
            _itemConfigDataSource = itemConfigDataSource;
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
                    _inventoryController = new InventoryController(_itemConfigs, _itemConfigDataSource, _inventoryView);
                    _inventoryController.ShowInventory();
                    _inventory.gameObject.SetActive(true);
                    
                    _gameController = new GameController(_profilePlayer);
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