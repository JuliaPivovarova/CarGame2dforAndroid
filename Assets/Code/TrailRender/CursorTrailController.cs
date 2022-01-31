using Code.Controllers;
using UnityEngine;

namespace Code.TrailRender
{
    public class CursorTrailController : BaseController
    {
        public CursorTrailController()
        {
            _view = LoadView();
            _view.Init();
        }
        
        
        private readonly ResourcesPath _viewPath = new ResourcesPath {PathResources = "Prefabs/trailCursor"};
        private CursorTrailView _view;

        private CursorTrailView LoadView()
        {
            GameObject objView = Object.Instantiate(ResourcesLoader.LoadPrefab(_viewPath));
            AddGameObject(objView);
            return objView.GetComponent<CursorTrailView>();
        } 
    }
}

