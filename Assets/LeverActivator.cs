using System;
using UnityEngine;
using UnityEngine.Events;


namespace Vald
{
    public class LeverActivator : MonoBehaviour
    {
        [SerializeField]
        private Transform _pivotTransform;

        [SerializeField]
        private HingeJoint2D _joint;

        public UnityEvent OnLeftPosition;

        public UnityEvent OnRightPosition;

        public UnityEvent OnNeutralPosition;

        [SerializeField]
        private Vector2 _rotations; // x for Left and y for Right

        private bool _isRight = true; // deactivated by default
        private bool _isLeft;


        private float handleRotation => _pivotTransform.eulerAngles.z;


        private void FixedUpdate()
        {
            CheckHandleRotation();
        }

        private void CheckHandleRotation()
        {
            if (handleRotation < 40)
            {
                if (_isRight || _isLeft)
                {
                    OnNeutralPosition?.Invoke();

                    _isLeft = false;
                    _isRight = false;
                }
            }

            else if (handleRotation >= _rotations.x && handleRotation <= _rotations.y && !_isLeft)
            {
                _isLeft = true;

                OnLeftPosition?.Invoke();
            }

            else if (handleRotation >= _rotations.y && !_isRight)
            {
                _isRight = true;

                OnRightPosition?.Invoke();
            }

        }


        public void TestRight()
        {
            Debug.Log("Right");
        }

        public void TestLeft()
        {
            Debug.Log("Left");
        }

        public void TestNeutral()
        {
            Debug.Log("Neutral");
        }
    }
}
