using Code.Model;

namespace Code.Controllers
{
    public class GameController : BaseController
    {
        public GameController(ProfilePlayer profilePlayer)
        {
            var leftMoveDiff = new SubscribeProperty<float>();
            var rightMoveDiff = new SubscribeProperty<float>();
            
            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);
            
            var carController = new CarController();
            AddController(carController);
        }
    }
}