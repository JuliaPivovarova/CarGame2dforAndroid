using UnityEngine;

namespace Code.Views
{
    public abstract class BaseInputView: MonoBehaviour
    {
        private SubscribeProperty<float> _leftMove;
        private SubscribeProperty<float> _rightMove;

        protected float _speed;

        public virtual void Init(SubscribeProperty<float> leftMove, SubscribeProperty<float> rightMove, float speed)
        {
            _leftMove = leftMove;
            _rightMove = rightMove;
            _speed = speed;
        }

        protected void OnLeftMove(float value)
        {
            _leftMove.Value = value;
        }
        
        protected void OnRightMove(float value)
        {
            _rightMove.Value = value;
        }
    }
}