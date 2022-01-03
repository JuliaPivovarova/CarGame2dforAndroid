using JoostenProductions;
using UnityEngine;

namespace Code.Views
{
    public class InputTaps: BaseInputView
    {
        Vector3 direction = Vector3.zero;
        Vector3 offset = Vector3.zero;
        float distance = 0f;
        Vector3 tapPosition;
        
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
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        offset = hit.transform.position - hit.point;
                        distance = hit.distance;
                    }

                    tapPosition = ray.origin + ray.direction * distance + offset;
                    direction.x = tapPosition.x;
                    direction.z = tapPosition.z;
                }
            }
            
            if (direction.sqrMagnitude > 1)
            {
                direction.Normalize();
            }
            
            OnRightMove(direction.sqrMagnitude / 20 * _speed);
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
        }
    }
}