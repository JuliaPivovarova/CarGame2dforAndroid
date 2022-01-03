using UnityEngine;

namespace Code
{
    public class Background: MonoBehaviour
    {
        [SerializeField] private float leftBorder;
        [SerializeField] private float rightBorder;
        [SerializeField] private float relativeSpeedRate;

        public void Move(float value)
        {
            transform.position += Vector3.right * value * relativeSpeedRate;
            var position = transform.position;

            if (position.x <= leftBorder)
            {
                transform.position = new Vector3(rightBorder - (leftBorder - position.x), position.y, position.z);
            }
            else if(position.x >= rightBorder)
            {
                transform.position = new Vector3(leftBorder + (rightBorder - position.x), position.y, position.z);
            }
        }
    }
}