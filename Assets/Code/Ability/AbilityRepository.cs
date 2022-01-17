using System;
using System.Collections.Generic;
using Code.Controllers;
using Code.Interfaces;
using UnityEngine;

namespace Code.Ability
{
    public class AbilityRepository: BaseController, IRepository<int, IAbility>
    {
        private Dictionary<int, IAbility> _abilityMapById = new Dictionary<int, IAbility>();

        public IReadOnlyDictionary<int, IAbility> Collection => _abilityMapById;

        public AbilityRepository(List<AbilityItemConfig> configs)
        {
            PopulateItems(configs);
        }

        private void PopulateItems(List<AbilityItemConfig> configs)
        {
            foreach (var config in configs)
            {
                if (_abilityMapById.ContainsKey(config.Id))
                {
                    continue;
                }
                
                _abilityMapById.Add(config.Id, CreateAbility(config));
            }
        }

        private IAbility CreateAbility(AbilityItemConfig config)
        {
            switch (config.AbilityType)
            {
                case AbilityType.Gun:
                    return new BombAbility(config);
                default:
                    Debug.LogError("Not type ability");
                    return null;
            }
        }

        protected override void OnDispose()
        {
            _abilityMapById.Clear();
        }
    }
}