using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5.0f;
    public float duration = 3.0f;

    private Rigidbody rigidbody;
    private BoxCollider boxCollider;

    private Vector3 previous;
    private Vector3 velocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        Destroy(gameObject, duration);
        previous = transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
        velocity = (transform.position - previous) / Time.deltaTime;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.DrawRay(collision.contacts[0].point, velocity, Color.black, 10f);
        var direction = Vector3.Reflect(velocity.normalized, collision.contacts[0].normal);
        transform.rotation = Quaternion.LookRotation(direction);
        previous = transform.position;
    }
}
