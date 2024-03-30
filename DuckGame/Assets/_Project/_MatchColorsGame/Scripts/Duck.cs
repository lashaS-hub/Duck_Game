using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MatchColorGame
{
    public class Duck : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private SpriteRenderer _duckRenderer;
        [SerializeField] private AudioClip _duckSound;
        [SerializeField] private Vector3 _scenePosition;
        [SerializeField] private GameObject _duckFooter;

        public ColorData ColorData { get; private set; }
        public bool IsDraggable => _isDraggable;

        private Coroutine _followMouseCoroutine;
        private Vector3 _startPosition;
        private bool _isDraggable = false;


        public void Initialize(ColorData colorData)
        {
            ColorData = colorData;
            _duckRenderer.color = ColorData.Color;
            _startPosition = transform.position;
            MoveToScenePosition();
        }

        private void MoveToScenePosition()
        {
            _isDraggable = false;
            transform.DOMove(_scenePosition, 2f).SetEase(Ease.InOutSine).OnComplete(() => _isDraggable = true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isDraggable) return;

            _isDraggable = false;

            OnTouchStart();

            if (_followMouseCoroutine != null)
            {
                StopCoroutine(_followMouseCoroutine);
            }
            _followMouseCoroutine = StartCoroutine(FollowMouse());
        }

        public void OnPointerUp(PointerEventData eventData)
        {

            if (_followMouseCoroutine != null)
            {
                StopCoroutine(_followMouseCoroutine);
            }

            bool jumpedInBasket = false;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down);

            foreach (var hit in hits)
            {
                Basket basket = hit.collider.GetComponent<Basket>();
                if (basket != null && basket.ColorData.ColorType == ColorData.ColorType)
                {
                    JumpIntoBasket(basket);
                    jumpedInBasket = true;
                    break;
                }
            }

            if (!jumpedInBasket)
            {
                BackToScenePosition();
            }

            OnTouchEnd();
        }

        private IEnumerator FollowMouse()
        {
            while (true)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = transform.position.z;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                worldPosition.z = transform.position.z;

                transform.position = worldPosition;

                yield return null;
            }
        }

        public void OnTouchStart()
        {
            _duckFooter.SetActive(false);
            transform.DOScale(Vector3.one * 1.2f, 0.2f);
        }

        public void OnTouchEnd()
        {
            _duckFooter.SetActive(true);
            transform.DOScale(Vector3.one, 0.2f);
        }

        private void BackToScenePosition()
        {
            _isDraggable = true;
            transform.position = _scenePosition;
        }

        public void JumpIntoBasket(Basket basket)
        {
            AudioController.Singleton.PlaySound(_duckSound);
            DuckGame.Singleton.OnDuckPlacedInBasket(this, basket);
        }

        internal void Restart()
        {
            transform.position = _startPosition;
            MoveToScenePosition();
        }
    }
}
