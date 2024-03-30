using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace MatchColorGame
{
    public class HelperHand : MonoBehaviour
    {
        [SerializeField] private GameObject _helperHand;

        private float _idleTime = 0f;
        private float _maxIdleTime = 8f;
        private bool _isHintShown = false;
        private bool _gameIsRunning = false;

        private float _moveDuration = 1f;
        private Sequence _sequence;

        private void Start()
        {
            DuckGame.OnGameStarted += SetGameStarted;
            DuckGame.OnGameEnd += SetGameEnded;
        }

        private void OnDestroy()
        {
            DuckGame.OnGameStarted -= SetGameStarted;
            DuckGame.OnGameEnd -= SetGameEnded;
        }

        private void Update()
        {
            if (!_gameIsRunning) return;

            if (Input.anyKey)
            {
                _idleTime = 0f;
                if (_isHintShown)
                {
                    HideHint();
                    _isHintShown = false;
                }
            }
            else
            {
                _idleTime += Time.deltaTime;

                if (_idleTime > _maxIdleTime && !_isHintShown)
                {
                    _isHintShown = true;
                    var pair = DuckGame.Singleton.GetMonochromePair();
                    ShowHint(pair.duck, pair.basket);
                }
            }
        }

        private void ShowHint(Duck duck, Basket basket)
        {
            _helperHand.SetActive(true);
            _helperHand.transform.position = duck.transform.position;

            _sequence = DOTween.Sequence()
                .Append(_helperHand.transform.DOMove(basket.transform.position, _moveDuration))
                .Append(_helperHand.transform.DOMove(duck.transform.position, 0))
                .SetLoops(-1);
        }

        private void HideHint()
        {
            _sequence.Kill();
            _helperHand.SetActive(false);
        }

        private void SetGameStarted() => _gameIsRunning = true;
        private void SetGameEnded() => _gameIsRunning = false;
    }
}