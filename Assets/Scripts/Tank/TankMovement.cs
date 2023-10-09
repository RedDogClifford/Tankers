using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int tankNumber = 1;
    public float speed = 12f;
    public float turnSpeed = 180f;

    [HideInInspector] public Rigidbody rigidbody;

    private float movementInputValue;
    private float turnInputValue;

    private string movementAxisName;
    private string turnAxisName;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        movementAxisName = "Vertical" + tankNumber;
        turnAxisName = "Horizontal" + tankNumber;

    }

    // Update is called once per frame
    private void Update()
    {
        //Store value of input axes
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);
    }

    private void FixedUpdate()
    {
        Move();

        Turn();
    }

    private void Move()
    {
        Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;

        rigidbody.MovePosition(rigidbody.position + movement);
    }

    private void Turn()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
    }
}
