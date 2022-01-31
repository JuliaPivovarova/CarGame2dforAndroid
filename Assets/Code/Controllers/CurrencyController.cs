using Code.Rewards;
using UnityEngine;

namespace Code.Controllers
{
    public class CurrencyController: BaseController
    {
        public CurrencyController(Transform PlaceForUI, CurrencyView currencyView)
        {
            var currencyViewInstance = Object.Instantiate(currencyView, PlaceForUI);
            AddGameObject(currencyViewInstance.gameObject);
        }
        
        
    }
}