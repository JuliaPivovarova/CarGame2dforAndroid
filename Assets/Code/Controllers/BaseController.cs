using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controllers
{
    public class BaseController : IDisposable
    {
        private List<BaseController> _mainControllers = new List<BaseController>();
        private List<GameObject> _gameObjects = new List<GameObject>();
    
        private bool _isDisposed;
    
        public void Dispose()
        {
            if (_isDisposed)
                return;
            
            _isDisposed = true;

            foreach (var mainController in _mainControllers)
            {
                mainController?.Dispose();
            }
            _mainControllers.Clear();

            foreach (var cashedGameObject in _gameObjects)
            {
                Object.Destroy(cashedGameObject);
            }
            _gameObjects.Clear();

            OnDispose();
        }

        protected void AddController(BaseController mainController)
        {
            _mainControllers.Add(mainController);
        }

        protected void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        protected virtual void OnDispose()
        {
            
        }
    }
}
