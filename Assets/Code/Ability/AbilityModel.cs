using System.Collections.Generic;

namespace Code.Ability
{
    public class AbilityModel
    {
        private readonly List<IAbility> _abilities = new List<IAbility>();

        public IReadOnlyList<IAbility> GetUsingAbilities()
        {
            return _abilities;
        }

        public void AddUsingAbility(IAbility ability)
        {
            if (_abilities.Contains(ability)) return;
            _abilities.Add(ability);
        }

        public void RemoveUsingAbility(IAbility ability)
        {
            if(!_abilities.Contains(ability)) return;
            _abilities.Remove(ability);
        }
    }
}