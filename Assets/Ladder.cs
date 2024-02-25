using System.Collections;
using UnityEngine;


namespace Vald
{
    public class Ladder : MonoBehaviour
    {
        private const float CELL_SIZE = 0.5f;

        [SerializeField]
        private Transform _ladderTransform;

        [SerializeField]
        private byte _cellsCountToMove = 1;

        [SerializeField]
        [Range(0.01f, 5f)]
        private float _moveSpeed;

        [SerializeField]
        private SpriteRenderer _renderer;

        [SerializeField]
        private Collider2D _trigger;

        private float spriteHeight => _renderer.size.y;


        public void MoveToTop() {

            StopAllCoroutines();
            StartCoroutine(MoveCoroutine(1));
        }


        public void MoveToBottom()
        {
            StopAllCoroutines();
            StartCoroutine(MoveCoroutine(0));
        }


        private IEnumerator MoveCoroutine(int direction) // 0 to bottom 1 to top
        {
            float targetHeight = 0;

            if (direction == 0)
                targetHeight = -1 * _cellsCountToMove * CELL_SIZE;

            _trigger.enabled = false;
            
            while (_ladderTransform.localPosition.y != targetHeight)
            {

                _ladderTransform.localPosition = Vector2.MoveTowards(_ladderTransform.localPosition, new Vector2(0, targetHeight), _moveSpeed * Time.deltaTime);

                var deltaPos = _moveSpeed * Time.deltaTime;

                _trigger.offset = direction == 0 ? new Vector2(0, _trigger.offset.y + deltaPos) : new Vector2(0, _trigger.offset.y + deltaPos * -1);

                if(direction == 1)
                    deltaPos = -deltaPos;

                Vector2 newSize = new Vector2(1, spriteHeight + deltaPos * 2);

                _renderer.size = newSize;

                yield return new WaitForEndOfFrame();
            }

            if (direction == 0)
            {
                _renderer.size = new Vector2(1, spriteHeight - spriteHeight % 0.5f);
                _trigger.enabled = true;
            }
            else
                _renderer.size = Vector2.one;

            yield return null;
        }
    }
}
