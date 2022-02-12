using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KinematicTracker : MonoBehaviour
{
    public UnityEvent punchEvent;

    public int framesPerMeasurement = 1;

    public float punchVelocityThreshold = 2f;
    public int maxRecordSize = 25;
    public float debounceTime = 0.5f;

    List<Vector3> previousPositions;
    List<Vector3> previousVelocities;
    List<Vector3> previousAccelerations;

    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;

    private int frameCounter;
    private float lastPunchEventTime;

    private void Start()
    {
        frameCounter = 0;
        lastPunchEventTime = Time.time;

        previousPositions = new List<Vector3>();
        previousVelocities = new List<Vector3>();
        previousAccelerations = new List<Vector3>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (frameCounter % framesPerMeasurement == 0)
        {
            MeasurePosition();
            MeasureVelocity();
            MeasureAcceleration();
            TickPunchEvent();
        }
        frameCounter += 1;
    }

    private void TickPunchEvent()
    {
        if (Time.time > lastPunchEventTime + debounceTime && velocity.sqrMagnitude > punchVelocityThreshold * punchVelocityThreshold)
        {
            punchEvent.Invoke();
            lastPunchEventTime = Time.time;
        }
    }

    private float MinusOneToPower(int n)
    {
        if (n % 2 == 0)
            return -1;
        return 1;
    }

    private int Combinations(int n, int r)
    {
        if (n == 0 || r == 0)
            return 1;
        else
            return Combinations(n - 1, r - 1) + Combinations(n - 1, r);
    }

    private Vector3 FiniteDifferentiation(List<Vector3> points, int order)
    {
        float stepSize = Time.fixedDeltaTime * framesPerMeasurement; //h
        Vector3 num = Vector3.zero;

        if (order == 1)
        {
            num = points[0] - points[1];
            return num / (float) stepSize;
        }
        else
        {
            int n = Math.Min(points.Count, order);

            for (int i = 0; i < n; i++)
            {
                num += MinusOneToPower(i) * Combinations(n, i) * points[i];
            }
            return num / Mathf.Pow(stepSize, order);
        }
    }

    private void MeasureAcceleration()
    {
        if (previousVelocities.Count < 2)
            return;
        acceleration = FiniteDifferentiation(previousVelocities, 1);
        previousAccelerations.Insert(0, acceleration);
        while (previousAccelerations.Count > maxRecordSize)
        {
            previousAccelerations.RemoveAt(previousAccelerations.Count - 1);
        }
    }

    private void MeasureVelocity()
    {        
        if (previousPositions.Count < 2)
            return;
        velocity = FiniteDifferentiation(previousPositions, 1);
        previousVelocities.Insert(0, velocity);
        while (previousVelocities.Count > maxRecordSize)
        {
            previousVelocities.RemoveAt(previousVelocities.Count - 1);
        }

    }

    private void MeasurePosition()
    {
        previousPositions.Insert(0, transform.localPosition);
        while (previousPositions.Count > maxRecordSize)
        {
            previousPositions.RemoveAt(previousPositions.Count - 1);
        }

    }
}
