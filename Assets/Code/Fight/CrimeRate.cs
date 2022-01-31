using UnityEngine;

namespace Code.Fight
{
    public class CrimeRate: DataPlayer
    {
        private int _countCrimeRate;
        private int _minCountCrimeRate;
        private int _maxCountCrimeRate;

        public CrimeRate(int minCountCrimeRate, int maxCountCrimeRate)
        {
            _minCountCrimeRate = minCountCrimeRate;
            _maxCountCrimeRate = maxCountCrimeRate;
        }
        
        public int CountCrimeRate
        {
            get => _countCrimeRate;
            set
            {
                if (_countCrimeRate != value)
                {
                    CountCrimeRateInBorders(value);
                }
            }
        }

        private void CountCrimeRateInBorders(int value)
        {
            if (_countCrimeRate < _minCountCrimeRate)
            {
                Debug.Log("Crime Rate can't be lower");
            }
            else if(_countCrimeRate > _maxCountCrimeRate)
            {
                Debug.Log("Crime Rate can't be higher");
            }
            else
            {
                _countCrimeRate = value;
                Notifier(DataType.CrimeRate);
            }
        }
    }
}