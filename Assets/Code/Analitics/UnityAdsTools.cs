using UnityEngine;
using UnityEngine.Advertisements;

namespace Code.Analitics
{
    public class UnityAdsTools: MonoBehaviour ,IAdsShower
    {
        private const string GameAndroidId = "4520705";
        private const string BannerPlacementId = "Banner_Android";
        
        private void Start()
        {
            Advertisement.Initialize(GameAndroidId);
        }

        public void ShowBanner()
        {
            Advertisement.Show(BannerPlacementId);
        }
    }
}