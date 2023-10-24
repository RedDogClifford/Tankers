using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5.0f;
    public float duration = 3.0f;
    public float damage = 100f;
    public string opposingTag = string.Empty;

    private Rigidbody bulletbody;
    private BoxCollider boxCollider;

    private Vector3 previous;
    private Vector3 velocity;

    ~Bullet()
    {
        //Animation


        transform.gameObject.SetActive(false);
    }

    private void Awake()
    {
        bulletbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        bulletbody.MovePosition(transform.position + transform.forward * Time.deltaTime * speed * 1.5f);
        Destroy(gameObject, duration);
        previous = transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        bulletbody.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
        velocity = (transform.position - previous) / Time.deltaTime;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint collider = collision.contacts[0];
        string tag = collider.otherCollider.tag;
        switch (tag) {
            case "Wall":
                //Debug.DrawRay(collision.contacts[0].point, velocity, Color.black, 10f);
                var direction = Vector3.Reflect(velocity.normalized, collider.normal);
                transform.rotation = Quaternion.LookRotation(direction);
                previous = transform.position;
                break;
            case "Enemy":
            case "Player":
                DamageEnemy(collider, tag);
                break;
            case "PlayerBullet":
            case "EnemyBullet":
                BulletCollision(collider.otherCollider);
                break;
        }
    }

    private void DamageEnemy(ContactPoint collider, string tag)
    {
        if(tag == opposingTag)
        { 
            TankHealth targetHealth = collider.otherCollider.gameObject.GetComponent<TankHealth>();

            if (!targetHealth) return;

            targetHealth.TakeDamage(damage);

            Destroy(gameObject);
        }
    }

    private void BulletCollision(Collider collider) 
    {
        //Ensure its not destroying same bullet type
        if(collider.tag != transform.tag)
        {
            Destroy(collider.gameObject);       
            Destroy(gameObject);
        }
    }
}
