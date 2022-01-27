using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Tweens
{
    public class TweenFight : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _enemy;
        [SerializeField] private float _duration;
        [SerializeField] private float _durationShake;
        [SerializeField] private float _shakeStrenth;
        [SerializeField] private float _newPositionPlayer;
        [SerializeField] private float _newPositionEnemy;
        [SerializeField] private Button _fight;
        [SerializeField] private Button _goWithoutFight;
        [SerializeField] private float _goWithoutFightDuration;
        [SerializeField] private float _goAwayPlayerPosition;

        private float _startXPositionPlayer = -500; // local position X
        private float _startXPositionEnemy = 500; // local position X

        private void Awake()
        {
            _fight.onClick.AddListener(ComplexTweenFight);
            _goWithoutFight.onClick.AddListener(TweenGoWithoutFight);
        }

        private void OnDestroy()
        {
            _fight.onClick.RemoveAllListeners();
            _goWithoutFight.onClick.RemoveAllListeners();
        }

        private void ComplexTweenFight()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_player.DOLocalMoveX(_newPositionPlayer, _duration).SetEase(Ease.OutElastic));
            sequence.Append(_player.DOShakePosition(_durationShake, _shakeStrenth));
            sequence.Insert(0, _enemy.DOLocalMoveX(_newPositionEnemy, _duration).SetEase(Ease.OutElastic));
            sequence.Insert(_duration, _enemy.DOShakePosition(_durationShake, _shakeStrenth));
            sequence.Append(_player.DOLocalMoveX(_startXPositionPlayer, _duration));
            sequence.Insert(_duration + _durationShake, _enemy.DOLocalMoveX(_startXPositionEnemy, _duration));

            sequence.OnComplete( () =>
            {
                sequence = null;
            });
        }

        private void TweenGoWithoutFight()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_player.DOLocalMoveX(_goAwayPlayerPosition, _goWithoutFightDuration));
            sequence.Append(_player.DOLocalMoveX(_startXPositionPlayer, _goWithoutFightDuration / 10));
            
            sequence.OnComplete( () =>
            {
                sequence = null;
            });
        }
    }
}