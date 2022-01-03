using UnityEngine;

namespace Code.Items
{
    [CreateAssetMenu(fileName = "UpgrateItemConfig", menuName = "UpgrateItemConfig")]
    public class UpgrateItemConfig: ScriptableObject
    {
        [SerializeField] private ItemConfig itemConfig;
        [SerializeField] private UpgrateType upgrateType;
        [SerializeField] private float valueUpgrate;
        [SerializeField] private Sprite sprite;

        public int Id => itemConfig.Id;
        public UpgrateType UpgrateType => upgrateType;
        public float ValueUpgrate => valueUpgrate;
        public Sprite Sprite => sprite;
    }
}