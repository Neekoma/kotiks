using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlatformEffector2D))]
public abstract class MonoPlatform : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    public virtual void DefaultBehaviour() { }
    public abstract void InteractWithPlayer();


    private void FixedUpdate()
    {
        DefaultBehaviour();
    }


    public Rigidbody2D rb => _rb;
}