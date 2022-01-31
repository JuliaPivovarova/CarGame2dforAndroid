using Code.Model;
using Code.Views;
using UnityEngine;

namespace Code.Controllers
{
    public class InputGameController: BaseController
    {
        private readonly ResourcesPath _viewPath = new ResourcesPath { PathResources = "Prefabs/endlessMove"};
        private BaseInputView _view;

        public InputGameController(SubscribeProperty<float> leftMove, SubscribeProperty<float> rightMove, CarModel car)
        {
            _view = LoadView();
            _view.Init(leftMove, rightMove, car.Speed);
        }

        private BaseInputView LoadView()
        {
            var objView = Object.Instantiate(ResourcesLoader.LoadPrefab(_viewPath));
            AddGameObject(objView);

            return objView.GetComponent<BaseInputView>();
        }
    }
}