using System;
using System.Collections.Generic;
using Code.Controllers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Ability
{
    public class AbilityController: BaseController, IAbilitiesController
    {
        private readonly ResourcesPath _abilityUIPath = new ResourcesPath { PathResources = "Prefabs/AbilitiesUI" };
        
        private readonly AbilityRepository _abilityRepository;
        private readonly AbilityButtonView _abilityButtonView;
        private readonly AbilityModel _abilityModel;
        private readonly AbilityCollectionView _abilityCollectionView;
        private readonly List<AbilityItemConfig> _configs;

        private Sprite _sprite;
        private string _title;

        public AbilityController(List<AbilityItemConfig> configs, Transform placeForAbilities)
        {
            _abilityModel = new AbilityModel();
            _abilityRepository = new AbilityRepository(configs);
            _configs = configs;
            _abilityButtonView = LoadAbilityUI(placeForAbilities);
            ShowAbilities();
        }

        private AbilityButtonView LoadAbilityUI(Transform placeForAbilities)
        {
            var abilityView = Object.Instantiate(ResourcesLoader.LoadPrefab(_abilityUIPath), placeForAbilities, false);
            AddGameObject(abilityView);
            
            var tryAbilityButtonViewObj = abilityView.TryGetComponent<AbilityButtonView>(out var abilityButtonViewObject);
            if (!tryAbilityButtonViewObj)
            {
                throw new Exception("There is no AbilityButtonView component found");
            }

            return abilityButtonViewObject;
        }

        public void ShowAbilities()
        {
            foreach (var ability in _abilityRepository.Collection.Values)
            {
                _abilityModel.AddUsingAbility(ability);
                if (ability is BombAbility)
                {
                    foreach (var config in _configs)
                    {
                        if (config.Title.Equals("Bomb"))
                        {
                            _sprite = config.Sprite;
                            _title = config.Title;
                        }
                    }
                }
                _abilityButtonView.AddAbilityImage(_sprite, _title);
                _abilityButtonView.StartAbility(ability.Apply);
            }
        }

        public void HideAbility()
        {
            
        }
    }
}