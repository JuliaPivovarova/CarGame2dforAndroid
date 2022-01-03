using System;
using System.Linq;
using Code.Analitics;
using Code.Controllers;
using Code.Items;
using Code.Model;
using UnityEngine;

namespace Code
{
    public class Root: MonoBehaviour
    {
        [SerializeField] private Transform placeForUI;
        [SerializeField] private Transform placeForVideo;
        [SerializeField] private float speed = 15f;
        [SerializeField] private UnityAdsTools unityAdsTools;
        [SerializeField] private ItemConfig[] itemConfigs;
        [SerializeField] private Transform Inventory;
        [SerializeField] private UpgrateItemConfigDataSource itemConfigDataSource;
        private MainController _mainController;

        private void Awake()
        {
            var profilePlayer = new ProfilePlayer(speed, unityAdsTools);
            profilePlayer.CurrentState.Value = GameState.Start;
            _mainController = new MainController(placeForUI, placeForVideo, profilePlayer, itemConfigs.ToList(), Inventory, itemConfigDataSource);
        }

        protected void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}