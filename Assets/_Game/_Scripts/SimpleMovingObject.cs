using DG.Tweening;
using UnityEditor;
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

#if UNITY_EDITOR
    [CustomEditor(typeof(SimpleMovingObject))]
    public class SimpleMovingObjectEditor : Editor
    {
        private SimpleMovingObject _target;


        private void OnEnable()
        {
            _target = (SimpleMovingObject)target;
        }


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set current position as end"))
            {
                WritePosition(_target.transform.localPosition);
            }
        }


        private void WritePosition(Vector2 value)
        {
            SerializedProperty property = null;

            property = serializedObject.FindProperty("_endPosition");

            property.vector2Value = value;

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
