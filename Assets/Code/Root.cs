using System.Linq;
using Code.Ability;
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
        [SerializeField] private UpgrateItemConfigDataSource itemConfigDataSource;
        [SerializeField] private AbilityItemConfig[] configs;
        [SerializeField] private Transform placeForAbilities;
        [SerializeField] private Transform placeForInventory;
        private MainController _mainController;

        private void Awake()
        {
            var profilePlayer = new ProfilePlayer(speed, unityAdsTools);
            profilePlayer.CurrentState.Value = GameState.Start;
            _mainController = new MainController(placeForUI, placeForVideo, profilePlayer, itemConfigs.ToList(), itemConfigDataSource, configs.ToList(), placeForAbilities, placeForInventory);
        }

        protected void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}