using System.Collections;
using System.Collections.Generic;
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

    public Vector3 acceleration;
    public Vector3 velocity;
    public Vector3 netForce;

    private bool grounded;
    private CapsuleCollider capsuleCollider;
    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        grounded = false;
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

    public void MovePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void UpdateGrounded()
    {

        if (!grounded && transform.position.y < 0f)
        {
            Vector3 position = transform.position;
            position.y = 0f;
            netForce = Vector3.zero;
            MovePosition(position);
            grounded = true;
        }

        if (grounded && transform.position.y > 0f)
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
        UpdateGrounded();

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
        }
        transform.position += velocity * dt;
        netForce = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
    }
}