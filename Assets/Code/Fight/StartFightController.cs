using Code.Controllers;
using Code.Model;
using UnityEngine;

namespace Code.Fight
{
    public class StartFightController: BaseController
    {
        private StartFightView _startFightView;
        private ProfilePlayer _profilePlayer;

        public StartFightController(Transform placeForUI, ProfilePlayer profilePlayer, StartFightView startFightView)
        {
            _profilePlayer = profilePlayer;
            _startFightView = Object.Instantiate(startFightView, placeForUI);
            AddGameObject(_startFightView.gameObject);

            SubscribesButtons();
        }

        private void SubscribesButtons()
        {
            _startFightView.StartFightButtom.onClick.AddListener(StartFight);
        }

        private void StartFight()
        {
            _profilePlayer.CurrentState.Value = GameState.Fight;
        }

        protected override void OnDispose()
        {
            _startFightView.StartFightButtom.onClick.RemoveAllListeners();
            
            base.OnDispose();
        }
    }
}