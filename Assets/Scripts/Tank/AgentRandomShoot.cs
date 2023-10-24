using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentRandomShoot : MonoBehaviour
{
    public enum Action
    {
        Turning,
        Shooting,
        Idle
    }
    public Action action = Action.Idle;

    public int minDelayTime = 3;
    public int maxDelayTime = 5;
    public GameObject bulletPrefab;
    public float rotationSpeed = 100f;

    private BoxCollider bulletSpawnLocation;

    private float shootDelay = 3f;
    private float rotationThreshold = 1.0f;
    private float timer = 0f;
    private float locationRange = 10.0f; //Range of random location
    private Vector3 target;

    private void Awake()
    {
        bulletSpawnLocation = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        switch (action)
        {
            case Action.Idle:
                ShootDelay();
                break;
            case Action.Turning:
                TurningAction();
                break;
            case Action.Shooting:
                ShootBullet();
                break;
        }
    }

    private void ShootDelay()
    {
        timer += Time.deltaTime;
        
        if(timer >= shootDelay)
        {
            Vector3 point;
            if(SelectNewTarget(transform.position, locationRange, out point))
            {
                timer = 0f;
                target = point;
                action = Action.Turning;
            }
        }
    }

    //Rotate towards a playable surface to make turret slightly more accurate
    private bool SelectNewTarget(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 15; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private void TurningAction()
    {
        Vector3 direction = (target - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, lookRotation) <= rotationThreshold)
        {
            action = Action.Shooting;
        }
    }

    private void ShootBullet()
    {
        //Shoot bullet
        Instantiate(bulletPrefab, transform.TransformPoint(bulletSpawnLocation.center), transform.rotation);
        shootDelay = Random.Range(minDelayTime, maxDelayTime);
        action = Action.Idle;
    }
}
