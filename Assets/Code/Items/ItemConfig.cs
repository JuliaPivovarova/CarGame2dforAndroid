using UnityEngine;

namespace Code.Items
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "ItemConfig")]
    public class ItemConfig: ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string title;

        public int Id => id;
        public string Title => title;
    }
}