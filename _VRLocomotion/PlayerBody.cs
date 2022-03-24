using UnityEngine;

public class PlayerBody : CharacterBody
{

    
}

[RequireComponent(typeof(CapsuleCollider))]
public class CharacterBody : MonoBehaviour
{
    public float mass = 1f;
    public float groundFriction = 1f;
    public float airFriction = 0.05f;
    public bool useGravity = true;
    public bool stickyFeet = true;
    public bool grounded = false;

    public CharacterController characterController;


    public Vector3 acceleration;
    public Vector3 velocity;
    public Vector3 netForce;

    private CapsuleCollider capsuleCollider;
    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        characterController = GetComponent<CharacterController>();
        netForce = Vector3.zero;
        velocity = Vector3.zero;
    }

    public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Force)
    {
        if (forceMode == ForceMode.Force)
            netForce += force;

        else if (forceMode == ForceMode.Acceleration)
            netForce += force * mass;

        else if (forceMode == ForceMode.VelocityChange)
            velocity += force;

        else if (forceMode == ForceMode.Impulse)
            velocity += force / mass;
    }

    public void MovePosition(Vector3 move)
    {
        //transform.position = position;
        CollisionFlags flags = characterController.Move(move);

        if ((flags & CollisionFlags.Below) != 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    public void ApplyGroundFriction(Vector3 surfaceNormal)
    {
        velocity = Vector3.MoveTowards(velocity, Vector3.zero, groundFriction * Time.fixedDeltaTime);
    }

    public void ApplyAirFriction()
    {
        AddForce(-airFriction * velocity.magnitude * velocity, ForceMode.Force);
    }

    public void FixedUpdate()
    {
        if (useGravity && !grounded)
        {
            AddForce(Vector3.down * 9.81f, ForceMode.Acceleration);
        }

        ApplyAirFriction();

        float dt = Time.fixedDeltaTime;
        Vector3 acceleration = netForce / mass;
        velocity += acceleration * dt;

        if (grounded)
        {
            velocity.y = Mathf.Max(velocity.y, 0f); //Rejecting with normal up. For general terrain, use collision normal
            ApplyGroundFriction(Vector3.up);
            if (stickyFeet)
            {
                velocity.x = 0f;
                velocity.z = 0f;
            }
        }

        MovePosition(velocity * dt);
        netForce = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
    }
}