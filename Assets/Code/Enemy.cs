using UnityEngine;

namespace Code
{
    public class Enemy: IEnemy
    {
        private string _name;

        private int _moneyPlayer;
        private int _healthPlayer;
        private int _powerPlayer;
        private int _crimeRatePlayer;

        public float Power
        {
            get
            {
                var power = _healthPlayer + _powerPlayer * 0.5f + _crimeRatePlayer - _moneyPlayer * 0.5f;
                return power;
            }
        }

        public Enemy(string name)
        {
            _name = name;
        }
        
        public void Update(DataPlayer dataPlayer, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Health:
                    var dataHealth = (Health)dataPlayer;
                    _healthPlayer = dataHealth.CountHealth;
                    break;
                case DataType.Money:
                    var dataMoney = (Money)dataPlayer;
                    _moneyPlayer = dataMoney.CountMoney;
                    break;
                case DataType.Power:
                    var dataPower = (Power)dataPlayer;
                    _powerPlayer = dataPower.CountPower;
                    break;
                case DataType.CrimeRate:
                    var dataCrimeRate = (CrimeRate)dataPlayer;
                    _crimeRatePlayer = dataCrimeRate.CountCrimeRate;
                    break;
            }
            
            Debug.Log($"Enemy {_name}, change {dataType}");
        }
    }
}