using System;
using Code.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers
{
    public class CarController : BaseController
    {
        private readonly ResourcesPath _viewPath = new ResourcesPath { PathResources = "Prefabs/Car" };
        private readonly CarView _carView;

        public CarController()
        {
            _carView = LoadView();
        }

        private CarView LoadView()
        {
            var objectView = Object.Instantiate(ResourcesLoader.LoadPrefab(_viewPath));
            AddGameObject(objectView);
            var tryCarViewObj = objectView.TryGetComponent<CarView>(out var carViewObject);
            if (!tryCarViewObj)
            {
                throw new Exception("There is no CarView component found");
            }
            
            return carViewObject;
        }

        public GameObject GetViewObject()
        {
            return _carView.gameObject;
        }
    }
}