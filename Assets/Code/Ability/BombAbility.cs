using UnityEngine;

namespace Code.Ability
{
    public class BombAbility: IAbility
    {
        private readonly AbilityItemConfig _config;
        private float _destroyTime = 1.5f;

        public BombAbility(AbilityItemConfig config)
        {
            _config = config;
        }
        
        public void Apply()
        {
            var bomb = Object.Instantiate(_config.View);
            var rigidbody = bomb.Rigidbody;
            rigidbody.AddForce(Vector2.right * _config.Value, ForceMode2D.Impulse);
            Object.Destroy(bomb.gameObject, _destroyTime);
        }

        public AbilityItemConfig GetConfig()
        {
            return _config;
        }
    }
}