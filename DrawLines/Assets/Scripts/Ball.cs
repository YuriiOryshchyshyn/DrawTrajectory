using UnityEngine;

public class Ball : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rigidbody2d;
    [HideInInspector] public CircleCollider2D circleCollider2D;

    [HideInInspector] public Vector2 position { get => transform.position; }

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void Push(Vector2 force)
    {
        rigidbody2d.AddForce(force, ForceMode2D.Impulse);
    }

    public void ActivateRb()
    {
        rigidbody2d.isKinematic = false;
    }

    public void DesactivateRb()
    {
        rigidbody2d.velocity = Vector2.zero;
        rigidbody2d.angularVelocity = 0f;
        rigidbody2d.isKinematic = true;
    }
}
