using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    public enum Action
    {
        Turning,
        Moving,
        Standing
    }
    public Action action = Action.Standing;
   
    public int tankNumber = 1;
    public int difficultyLevel = 1;

    [HideInInspector] public float rotationThreshold = 1.0f;
    [HideInInspector] public float rotationSpeed = 100f;

    //Difficulty 1 tanks
    public float locationRange = 10.0f; //Range of random location

    [HideInInspector] public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
    }
    
    // Update is called once per frame
    private void Update()
    {
        switch (action)
        {
            case Action.Standing:
                break;
            case Action.Moving:
                MovingAction();
                break;
            case Action.Turning:
                TurningAction();
                break;
        }
    }

    public void MovingAction()
    {
        //If path is near end or there's no path in queue
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            switch (difficultyLevel)
            {
                case 1:
                    RandomMovement();
                    break;
            }
        }
    }

    private void TurningAction()
    {
        var pathDirection = navMeshAgent.steeringTarget; //Direction of nav agent path
        Vector3 direction = (pathDirection - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        if(Quaternion.Angle(transform.rotation, lookRotation) <= rotationThreshold)
        {
            navMeshAgent.isStopped = false;
            action = Action.Moving;
        }
    }

    private void RandomMovement()
    {
        Vector3 point;
        if (RandomPosition(transform.position, locationRange, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);

            navMeshAgent.SetDestination(point);
            navMeshAgent.isStopped = true;
            action = Action.Turning;
        }
    }

    private bool RandomPosition(Vector3 center, float range, out Vector3 result)
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
}
