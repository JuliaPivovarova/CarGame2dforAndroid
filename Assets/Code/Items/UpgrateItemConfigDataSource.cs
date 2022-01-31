using UnityEngine;

namespace Code.Items
{
    [CreateAssetMenu(fileName = "UpgrateItemConfigDataSource", menuName = "UpgrateItemConfigDataSource")]
    public class UpgrateItemConfigDataSource : ScriptableObject
    {
        [SerializeField] private UpgrateItemConfig[] itemConfigs;

        public UpgrateItemConfig[] ItemConfigs => itemConfigs;
    }
}