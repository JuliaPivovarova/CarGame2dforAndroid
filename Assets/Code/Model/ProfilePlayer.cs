using Code.Analitics;

namespace Code.Model
{
    public class ProfilePlayer
    {
        public SubscribeProperty<GameState> CurrentState { get; }
        public CarModel CurrentCar { get; }
        public IAnaliticsTools AnaliticsTools { get; }
        public IAdsShower AdsShower { get; }

        public ProfilePlayer(float speed, UnityAdsTools unityAdsTools)
        {
            CurrentCar = new CarModel(speed);
            CurrentState = new SubscribeProperty<GameState>();
            AnaliticsTools = new UnityAnaliticsTools();
            AdsShower = unityAdsTools;
        }
    }
}