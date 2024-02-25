using UnityEngine;


namespace Vald
{
    public abstract class InteractableObject : MonoBehaviour, IInteractable
    {
        public abstract void Interact(Player player);
    }
}
