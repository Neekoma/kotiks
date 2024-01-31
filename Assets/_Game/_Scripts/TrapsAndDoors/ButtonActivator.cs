using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


namespace Vald
{
    public class ButtonActivator : Activator
    {
        [SerializeField]
        private Vector2 _deactivatedPosition;

        [SerializeField]
        private Vector2 _activatedPosition;

        [SerializeField]
        private LayerMask _whatCanPressOnButton;

        [SerializeField]
        private bool _isActive;

        [SerializeField]
        [Range(0.05f, 1)]
        private float _overlapRadius;

        public UnityEvent OnActivation;
        public UnityEvent OnDeactivation;

        private Collider2D overlap => Physics2D.OverlapCircle(transform.position, _overlapRadius, _whatCanPressOnButton);


        public override void Activation()
        {
            _isActive = true;
            MoveTween(_activatedPosition);
            OnActivation?.Invoke();
            Debug.Log("Activation");
        }

        public override void Deactivation()
        {
            _isActive = false;
            MoveTween(_deactivatedPosition);
            OnDeactivation?.Invoke();
            Debug.Log("Deactivation");
        }

        private void Start()
        {
            transform.localPosition = _deactivatedPosition;
        }

        private void FixedUpdate()
        {
            CheckPressure();
        }

        private void CheckPressure()
        {
            if (_isActive == false && overlap != null)
            {
                Activation();
            }
            else if (_isActive == true && overlap == null)
            {
                Deactivation();
            }
        }

        private void MoveTween(Vector2 target)
        {
            if (DOTween.IsTweening(target))
                DOTween.KillAll();

            transform.DOLocalMove(target, 1);

        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, _overlapRadius);
        }

    }

    [CustomEditor(typeof(ButtonActivator))]
    public class ButtonActivatorEditor : Editor
    {

        private ButtonActivator _target;

        private void OnEnable()
        {
            _target = (ButtonActivator)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Current position as deactivated"))
            {
                WritePosition(_target.transform.localPosition, 0);
            }

            if (GUILayout.Button("Current position as activated"))
            {
                WritePosition(_target.transform.localPosition, 1);
            }

            if (GUILayout.Button("To deactivated position"))
            {
                SetToPosition(serializedObject.FindProperty("_deactivatedPosition").vector2Value);
            }

            if (GUILayout.Button("To activated position"))
            {
                SetToPosition(serializedObject.FindProperty("_activatedPosition").vector2Value);
            }

        }

        private void WritePosition(Vector2 value, byte position)
        {
            SerializedProperty property = null;

            if (position == 0)
                property = serializedObject.FindProperty("_deactivatedPosition");

            if (position == 1)
                property = serializedObject.FindProperty("_activatedPosition");

            property.vector2Value = value;

            serializedObject.ApplyModifiedProperties();
        }

        private void SetToPosition(Vector2 target)
        {
            _target.transform.localPosition = (Vector3)target;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

