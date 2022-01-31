using JoostenProductions;
using UnityEngine;

namespace Code.Views
{
    public class InputAcceleration : BaseInputView
    {
        public override void Init(SubscribeProperty<float> leftMove, SubscribeProperty<float> rightMove, float speed)
        {
            base.Init(leftMove, rightMove, speed);
            UpdateManager.SubscribeToUpdate(Move);
        }

        private void Move()
        {
            var direction = Vector3.zero;
            direction.x = -Input.acceleration.y;
            direction.z = Input.acceleration.x;

            if (direction.sqrMagnitude > 1)
            {
                direction.Normalize();
            }
            
            OnRightMove(direction.sqrMagnitude / 20 * Speed);
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
        }
    }
}