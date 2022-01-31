using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.Views
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button buttonStart;
        [SerializeField] private Button buttonDailyRewards;
        [SerializeField] private Button buttonExit;
        
        public void Init(UnityAction startGame, UnityAction dailyReward)
        {
            buttonStart.onClick.AddListener(startGame);
            buttonDailyRewards.onClick.AddListener(dailyReward);
            buttonExit.onClick.AddListener(ExiGame);
        }

        private void ExiGame()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            buttonStart.onClick.RemoveAllListeners();
            buttonDailyRewards.onClick.RemoveAllListeners();
            buttonExit.onClick.RemoveAllListeners();
        }
    }
}