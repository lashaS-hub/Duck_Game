using System;
using System.Collections;
using UnityEngine;

namespace MatchColorGame
{
    public class DuckGame : MonoBehaviour
    {
        public static DuckGame Singleton;

        [SerializeField] private Basket[] _baskets;
        [SerializeField] private Duck[] _ducks;
        [SerializeField] private ColorData[] _colorDatas;
        [SerializeField] private HelperHand _helperHand;


        public static event Action<int> OnScoreUpdated;
        public static event Action OnGameStarted;
        public static event Action OnGameEnd;

        private int _score = 0;
        private int _maxScore = 5;


        private void Awake()
        {
            if (Singleton == null)
            {
                Singleton = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            StartCoroutine(StartGame());
        }

        public void OnDuckPlacedInBasket(Duck duck, Basket basket)
        {
            duck.Restart();
            basket.PlayerScored();

            _score++;
            OnScoreUpdated(_score);

            CheckGameStatus();
        }

        private void CheckGameStatus()
        {
            if (_score == _maxScore)
            {
                OnGameEnd();
            }
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < _baskets.Length; i++)
            {
                _baskets[i].Initialize(_colorDatas[i]);
            }

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < _ducks.Length; i++)
            {
                _ducks[i].Initialize(_colorDatas[i]);
            }

            OnGameStarted();
        }

        public (Duck duck, Basket basket) GetMonochromePair()
        {
            foreach (Duck duck in _ducks)
            {
                if (duck.IsDraggable)
                {
                    foreach (Basket basket in _baskets)
                    {
                        if (duck.ColorData.Color == basket.ColorData.Color)
                        {
                            return (duck, basket);
                        }
                    }
                }
            }

            return (null, null);
        }

        public void OnGameRestarted()
        {
            _score = 0;
            OnGameStarted();
        }
    }
}
