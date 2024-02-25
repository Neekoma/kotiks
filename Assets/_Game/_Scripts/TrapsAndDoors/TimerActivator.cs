using System.Collections;
using UnityEngine;
using UnityEngine.Events;


namespace Vald
{
    public class TimerActivator : Activator, IActivationTarget
    {
        [SerializeField]
        private byte[] _timerPeriods;

        [SerializeField]
        private byte _startPeriod;

        private byte _currentPeriod = 0;

        [SerializeField]
        [Tooltip("Будет ли повторяться цикл таймера по истечению времени")]
        private bool _isPeriodic;

        [SerializeField]
        [Tooltip("Таймер начнет работу сразу после загрузки сцены в случае, если true")]
        private bool _startAfterSceneLoading;

        public UnityEvent<byte> OnTimerTick;
        public UnityEvent OnTimerEnd;
        public UnityEvent OnTimerStart;
        public UnityEvent OnTimerInterrupted;

        private Coroutine _timerCoroutine;


        private void Awake()
        {
            if (_timerPeriods.Length == 0)
                throw new ActivatorException("Timer should have at lest 1 time period");
        }


        private void Start()
        {
            _currentPeriod = _startPeriod;

            if (_startAfterSceneLoading)
                StartTimer();
        }


        private void OnEnable()
        {
            OnTimerTick.AddListener(HandleTimerTick);
        }


        private void OnDisable()
        {
            OnTimerTick.RemoveListener(HandleTimerTick);
        }


        public void StartTimer()
        {
            if (_currentPeriod == _startPeriod)
                OnTimerStart?.Invoke();

            _timerCoroutine = StartCoroutine(TimerCoroutine());
        }


        public void InterruptTimer()
        {
            StopCoroutine(_timerCoroutine);
            OnTimerInterrupted?.Invoke();
        }


        private void HandleTimerTick(byte period)
        {
            StopCoroutine(_timerCoroutine);

            if (period != _timerPeriods.Length - 1 || _isPeriodic) // If this is not the last tick or timer is periodic
                StartTimer();
            else
            {
                _timerCoroutine = null;
                OnTimerEnd?.Invoke();
            }
        }

        private IEnumerator TimerCoroutine()
        {
            yield return new WaitForSeconds(_timerPeriods[_currentPeriod]);

            _currentPeriod = (byte)(++_currentPeriod % _timerPeriods.Length);

            OnTimerTick?.Invoke(_currentPeriod);
        }

        public void SetActiveState(bool activationState)
        {
            if (activationState == false)
                InterruptTimer();

            else
            {
                InterruptTimer();
                StartTimer();
            }
        }
    }
}