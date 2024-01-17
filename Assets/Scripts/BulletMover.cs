using UnityEngine;

public class BulletMover : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public void Init(float speed, Vector2 direction)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = direction * speed;
    }

    
}
