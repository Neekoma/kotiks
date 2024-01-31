using UnityEngine;

namespace Vald
{
    
    public class TimerTest : MonoBehaviour
    {
        public void SendMessageStart()
        {
            Debug.Log("Start message");
        }

        public void SendMessageTick(byte tick)
        {
            Debug.Log($"Tick message {tick}");
        }

        public void SendMessageEnd()
        {
            Debug.Log("End message");
        }

        public void SendMessageInterrupt()
        {
            Debug.Log("End message");
        }

    }
}
