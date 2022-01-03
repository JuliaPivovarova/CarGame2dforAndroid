﻿using System;
using Code.Model;
using Code.Views;
using JoostenProductions;
using UnityEngine;
using UnityEngine.Video;
using Object = UnityEngine.Object;

namespace Code.Controllers
{
    public class MainMenuController : BaseController
    {
        private readonly ResourcesPath _viewPath = new ResourcesPath { PathResources = "Prefabs/mainMenu" };
        private readonly ResourcesPath _trailPath = new ResourcesPath { PathResources = "Prefabs/Trail"};
        private readonly ResourcesPath _videoPath = new ResourcesPath { PathResources = "Prefabs/Video"};
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _mainMenuView;
        private GameObject _trail;
        private VideoPlayer _video;
        private bool _videoPlaied = false;
        
        private float _distance;
        private Vector3 _offset;

        public MainMenuController(Transform placeForUI, Transform placeForVideo, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _mainMenuView = LoadView(placeForUI, placeForVideo);
            _mainMenuView.Init(StartGame);
            UpdateManager.SubscribeToUpdate(Update);
            _video.Play();
        }

        private MainMenuView LoadView(Transform placeForUI, Transform placeForVideo)
        {
            var objectView = Object.Instantiate(ResourcesLoader.LoadPrefab(_viewPath), placeForUI, false);
            AddGameObject(objectView);
            objectView.SetActive(false);
            _trail = Object.Instantiate(ResourcesLoader.LoadPrefab(_trailPath));
            AddGameObject(_trail);
            var video = Object.Instantiate(ResourcesLoader.LoadPrefab(_videoPath), placeForVideo, false);
            AddGameObject(video);
            _video = video.GetComponent<VideoPlayer>();
            _video.targetCamera = Camera.main;
            _video.renderMode = VideoRenderMode.CameraNearPlane;
            
            var tryMainMenuViewObj = objectView.TryGetComponent<MainMenuView>(out var mainMenuViewObject);
            if (!tryMainMenuViewObj)
            {
                throw new Exception("There is no MainMenuView component found");
            }
            
            return mainMenuViewObject;
        }

        private void StartGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
            _profilePlayer.AnaliticsTools.SendMessage("start_game", ("time", Time.realtimeSinceStartup));
            _profilePlayer.AdsShower.ShowBanner();
        }

        public void Update()
        {
            if (_video.isPlaying && !_videoPlaied)
            {
                _videoPlaied = true;
            }
            if (_video.gameObject.activeInHierarchy && !_video.isPlaying && _videoPlaied)
            {
                _video.gameObject.SetActive(false);
                _mainMenuView.gameObject.SetActive(true);
            }
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Moved)
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        _offset = hit.transform.position - hit.point;
                        _distance = hit.distance;
                    }

                    _trail.transform.position = ray.origin + ray.direction * _distance + _offset;
                }
            }
        }

        protected override void OnDispose()
        {
            UpdateManager.UnsubscribeFromUpdate(Update);
            base.OnDispose();
        }
    }
}