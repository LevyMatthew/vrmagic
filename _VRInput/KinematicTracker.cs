using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KinematicTracker : MonoBehaviour
{
    public UnityEvent punchEvent;
    public UnityEvent pullEvent;

    public int framesPerMeasurement = 1;

    public Transform body;

    public float punchVelocityThreshold = 2f;
    public float pullVelocityThreshold = 2f;
    public float punchAngularSensitivity = 0.5f;
    public float pullAngularSensitivity = 0.5f;
    public float punchRadiusReleaseThreshold = 0.5f;
    public float pullRadiusReleaseThreshold = 0.5f;

    public int maxRecordSize = 25;
    public float debounceTime = 0.5f;

    List<Vector3> previousLocalPositions;
    List<Vector3> previousLocalVelocities;
    List<Vector3> previousLocalAccelerations;

    public Vector3 localPosition;
    public Vector3 localVelocity;
    public Vector3 localAcceleration;

    List<Vector3> previousPositions;
    List<Vector3> previousVelocities;
    List<Vector3> previousAccelerations;

    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;

    private int frameCounter;
    private float lastPunchEventTime;
    private float lastPullEventTime;

    private void Start()
    {
        frameCounter = 0;
        lastPunchEventTime = Time.time;
        lastPullEventTime = Time.time;

        previousPositions = new List<Vector3>();
        previousVelocities = new List<Vector3>();
        previousAccelerations = new List<Vector3>();

        previousLocalPositions = new List<Vector3>();
        previousLocalVelocities = new List<Vector3>();
        previousLocalAccelerations = new List<Vector3>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (frameCounter % framesPerMeasurement == 0)
        {
            MeasureLocalPosition();
            MeasureLocalVelocity();
            MeasureLocalAcceleration();

            MeasurePosition();
            MeasureVelocity();
            MeasureAcceleration();
            TickPunchEvent();
        }
        frameCounter += 1;
    }



    private void TickPunchEvent()
    {
        if (Time.time < lastPunchEventTime + debounceTime)
            return;

        if (localVelocity.sqrMagnitude <= punchVelocityThreshold * punchVelocityThreshold)
            return;

        if (Vector3.Dot(velocity, body.forward) < punchAngularSensitivity)
            return;

        punchEvent.Invoke();
        lastPunchEventTime = Time.time;
    }

    private void TickPullEvent()
    {
        if (Time.time < lastPullEventTime + debounceTime)
            return;

        if (localVelocity.sqrMagnitude <= pullVelocityThreshold * pullVelocityThreshold)
            return;

        if (Vector3.Dot(localVelocity, body.forward) > -pullAngularSensitivity)
            return;

        pullEvent.Invoke();
        lastPullEventTime = Time.time;
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
            return num / (float)stepSize;
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

        position = transform.position;

        previousPositions.Insert(0, position);
        while (previousPositions.Count > maxRecordSize)
        {
            previousPositions.RemoveAt(previousPositions.Count - 1);
        }

    }

    private void MeasureLocalAcceleration()
    {
        if (previousLocalVelocities.Count < 2)
            return;
        localAcceleration = FiniteDifferentiation(previousLocalVelocities, 1);
        previousLocalAccelerations.Insert(0, localAcceleration);
        while (previousLocalAccelerations.Count > maxRecordSize)
        {
            previousLocalAccelerations.RemoveAt(previousLocalAccelerations.Count - 1);
        }
    }

    private void MeasureLocalVelocity()
    {
        if (previousLocalPositions.Count < 2)
            return;
        localVelocity = FiniteDifferentiation(previousLocalPositions, 1);
        previousLocalVelocities.Insert(0, localVelocity);
        while (previousLocalVelocities.Count > maxRecordSize)
        {
            previousLocalVelocities.RemoveAt(previousLocalVelocities.Count - 1);
        }
    }

    private void MeasureLocalPosition()
    {
        localPosition = transform.localPosition;
        previousLocalPositions.Insert(0, localPosition);
        while (previousLocalPositions.Count > maxRecordSize)
        {
            previousLocalPositions.RemoveAt(previousLocalPositions.Count - 1);
        }
    }


}
