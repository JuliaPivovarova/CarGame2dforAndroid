using JoostenProductions;
using UnityEngine;

namespace Code.Views
{
    public class InputTaps: BaseInputView
    {
        private Vector3 _direction = Vector3.zero;
        private Vector3 _offset = Vector3.zero;
        private float _distance = 0f;
        private Vector3 _tapPosition;
        
        public override void Init(SubscribeProperty<float> leftMove, SubscribeProperty<float> rightMove, float speed)
        {
            base.Init(leftMove, rightMove, speed);
            UpdateManager.SubscribeToUpdate(Move);
        }

        private void Move()
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    if (Camera.main is { })
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                        if (Physics.Raycast(ray, out var hit))
                        {
                            _offset = hit.transform.position - hit.point;
                            _distance = hit.distance;
                        }

                        _tapPosition = ray.origin + ray.direction * _distance + _offset;
                    }

                    _direction.x = _tapPosition.x;
                    _direction.z = _tapPosition.z;
                }
            }
            
            if (_direction.sqrMagnitude > 1)
            {
                _direction.Normalize();
            }
            
            OnRightMove(_direction.sqrMagnitude / 20 * Speed);
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
        }
    }
}