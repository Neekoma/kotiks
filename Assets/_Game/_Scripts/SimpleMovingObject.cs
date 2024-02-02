using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Vald
{
    public class SimpleMovingObject : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _endPosition;

        [SerializeField]
        private float _moveDuration;

        private Vector2 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }


        public void MoveToTarget()
        {
            if (DOTween.IsTweening(this))
                DOTween.KillAll();

            transform.DOMove(_endPosition, _moveDuration);
        }
        public void ReturnToStartPosition()
        {
            if (DOTween.IsTweening(this))
                DOTween.KillAll();

            transform.DOMove(_startPosition, _moveDuration);
        }
    }
}
