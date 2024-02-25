using DG.Tweening;
using System;
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
        private bool _isPressedOnStart;

        private bool _isFreeToInteract; // pressed on start only

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
            OnActivation?.Invoke();
        }


        public override void Deactivation()
        {
            _isActive = false;
            OnDeactivation?.Invoke();
        }


        private void Start()
        {
            StartSetup();
        }


        private void FixedUpdate()
        {
            CheckPressure();
        }


        private void CheckPressure()
        {
            if (_isFreeToInteract)
            {
                if (_isActive == false && overlap != null)
                {
                    Activation();

                    MoveTween(_activatedPosition);
                }

                else if (_isActive == true && overlap == null)
                {
                    Deactivation();

                    MoveTween(_deactivatedPosition);
                }
            }
        }


        private void MoveTween(Vector2 target)
        {
            if (DOTween.IsTweening(target))
                DOTween.KillAll();

            transform.DOLocalMove(target, 1);
        }


        private void StartSetup()
        {
            if (_isPressedOnStart)
                SetupToPressed();
            else
                SetupToDefault();
        }


        private void SetupToDefault()
        {
            transform.localPosition = _deactivatedPosition;
            _isFreeToInteract = true;
        }


        public void SetupToPressed()
        {
            _isFreeToInteract = false;

            MoveTween(_activatedPosition);

            Activation();
        }


        public void FreeToInteract()
        {
            _isFreeToInteract = true;

            MoveTween(_deactivatedPosition);

            Deactivation();
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, _overlapRadius);
        }
    }

#if UNITY_EDITOR
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
#endif
}

