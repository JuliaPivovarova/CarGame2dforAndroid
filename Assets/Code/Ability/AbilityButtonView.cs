using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.Ability
{
    public class AbilityButtonView: MonoBehaviour
    {
        [SerializeField] private Image abilityImage;
        [SerializeField] private Button abilityButton;
        [SerializeField] private TextMeshProUGUI abilityText;

        public void AddAbilityImage(Sprite ability, string abilityName)
        {
            abilityImage.sprite = ability;
            abilityText.text = abilityName;
        }
        
        public void StartAbility(UnityAction applyAbility)
        {
            abilityButton.onClick.AddListener(applyAbility);
        }

        private void OnDestroy()
        {
            abilityButton.onClick.RemoveAllListeners();
        }
    }
}