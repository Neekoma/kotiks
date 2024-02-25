using UnityEngine;


namespace Vald
{
    public abstract class Activator : MonoBehaviour
    {
        public virtual void Activation() { }
        public virtual void Deactivation() { }
    }

    public class ActivatorException : System.Exception
    {
        public ActivatorException(string message) : base(message){}
    }
}
