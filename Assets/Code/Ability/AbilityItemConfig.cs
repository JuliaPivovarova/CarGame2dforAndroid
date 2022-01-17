using Code.Items;
using UnityEngine;

namespace Code.Ability
{
    [CreateAssetMenu(fileName = "AbilityItemConfig", menuName = "AbilityItemConfig")]
    public class AbilityItemConfig: ScriptableObject
    {
        [SerializeField] private ItemConfig itemConfig;
        [SerializeField] private AbilityView view;
        [SerializeField] private AbilityType abilityType;
        [SerializeField] private float value;
        

        public ItemConfig ItemConfig => itemConfig;
        public AbilityView View => view;
        public AbilityType AbilityType => abilityType;
        public int Id => itemConfig.Id;
        public float Value => value;
        public Sprite Sprite => view.Sprite;
        public string Title => itemConfig.Title;
    }
}