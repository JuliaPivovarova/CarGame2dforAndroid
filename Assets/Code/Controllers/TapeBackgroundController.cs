using Code.Interfaces;
using Code.Views;
using UnityEngine;

namespace Code.Controllers
{
    public class TapeBackgroundController: BaseController
    {
        private readonly ResourcesPath _viewPath = new ResourcesPath { PathResources = "Prefabs/background" };
        private TapeBackgroundView _view;
        private readonly SubscribeProperty<float> _diff;
        private readonly IReadOlnySubscribeProperty<float> _leftMove;
        private readonly IReadOlnySubscribeProperty<float> _rightMove;

        public TapeBackgroundController(IReadOlnySubscribeProperty<float> leftMove, IReadOlnySubscribeProperty<float> rightMove)
        {
            _view = LoadView();
            _diff = new SubscribeProperty<float>();

            _leftMove = leftMove;
            _rightMove = rightMove;
            
            _view.Init(_diff);
            
            _leftMove.SubscribeOnChange(Move);
            _rightMove.SubscribeOnChange(Move);
        }

        private void Move(float value)
        {
            _diff.Value = value;
        }

        private TapeBackgroundView LoadView()
        {
            var objView = Object.Instantiate(ResourcesLoader.LoadPrefab(_viewPath));
            AddGameObject(objView);

            return objView.GetComponent<TapeBackgroundView>();
        }

        protected override void OnDispose()
        {
            _leftMove.UnSubscribeOnChange(Move);
            _rightMove.UnSubscribeOnChange(Move);
            
            base.OnDispose();
        }
    }
}