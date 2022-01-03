using System;
using Code.Interfaces;
using UnityEngine;

namespace Code.Views
{
    public class TapeBackgroundView: MonoBehaviour
    {
        [SerializeField] private Background[] backgrounds;

        private IReadOlnySubscribeProperty<float> _diff;

        public void Init(IReadOlnySubscribeProperty<float> diff)
        {
            _diff = diff;
            _diff.SubscribeOnChange(Move);
        }

        private void Move(float value)
        {
            foreach (var background in backgrounds)
            {
                background.Move(-value);
            }
        }

        protected void OnDestroy()
        {
            _diff?.UnSubscribeOnChange(Move);
        }
    }
}