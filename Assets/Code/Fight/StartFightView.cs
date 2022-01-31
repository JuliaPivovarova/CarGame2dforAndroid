using UnityEngine;
using UnityEngine.UI;

namespace Code.Fight
{
    public class StartFightView: MonoBehaviour
    {
        [SerializeField] private Button _startFightButtom;

        public Button StartFightButtom => _startFightButtom;
    }
}