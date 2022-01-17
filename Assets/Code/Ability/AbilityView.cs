using UnityEngine;

namespace Code.Ability
{
    public class AbilityView: MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody;
        [SerializeField] private Sprite sprite;

        public Rigidbody2D Rigidbody => rigidbody;
        public Sprite Sprite => sprite;
    }
}