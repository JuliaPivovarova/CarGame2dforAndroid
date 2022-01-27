using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace Code.Tweens
{
    public class CustomButton: Button
    {
        public static string Duration => nameof(_duration);
        public static string Strength => nameof(_strength);
        
        [SerializeField] private float _duration = 2f;
        [SerializeField] private float _strength = 15f;

        private RectTransform _rectTransform;
        private bool _isShakeing;

        protected override void Awake()
        {
            base.Awake();

            _rectTransform = GetComponent<RectTransform>();
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            
            DoAnimation();
        }

        private void DoAnimation()
        {
            _rectTransform.DOShakeAnchorPos(_duration, _strength);
            
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            DoShakeButton();
        }

        private void DoShakeButton()
        {
            if (!_isShakeing)
            {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(_rectTransform.DOShakeRotation(_duration, _strength));
                _isShakeing = true;
                
                sequence.OnComplete( () =>
                {
                    sequence = null;
                    _isShakeing = false;
                });
            }

            
        }
    }
}