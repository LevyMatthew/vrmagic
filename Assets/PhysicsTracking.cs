using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTracking : MonoBehaviour
{
    [SerializeField] public Transform origin;
    LinkedList<Vector3> positions;
    LinkedList<Vector3> velocities;
    LinkedList<Vector3> accelerations;
    private Vector3 origin_position = Vector3.zero;
    //  Queue<Quaternion> rotations;

    private void Awake()
    {
        positions = new LinkedList<Vector3>();
        if (origin) origin_position = origin.position;
        for (int i = 0; i < 3; i++) {
            positions.AddFirst(transform.position - origin_position);
            velocities.AddFirst(transform.position - origin_position);
        }
        //rotations = new Queue<Quaternion>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3[] x = new Vector3[3];

        float dt = Time.fixedDeltaTime;
        positions.AddFirst(transform.position - origin_position);
        if (positions.Count >= 2)
        {
            positions.CopyTo(x, 0);
            Vector3 v = (x[0] - x[1]) / dt;
            velocities.AddFirst(v);
        }
        if (velocities.Count >= 2)
        {
            velocities.CopyTo(x, 0);
            Vector3 a = (x[0] - x[1]) / dt;
            accelerations.AddFirst(a);
        }
        //rotations.Enqueue(transform.rotation - );
    }

    public Vector3 GetVelocity()
    {
        return velocities.First.Value;
    }

    public Vector3 GetPosition()
    {
        return positions.First.Value;
    }
}
