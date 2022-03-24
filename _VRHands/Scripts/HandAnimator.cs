using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandAnimator : MonoBehaviour
{
    public SteamVR_Action_Single m_GrabAction = null;
    public float gripFistFraction = 0.75f;
    private Animator m_Animator = null;
    private SteamVR_Behaviour_Pose m_Pose = null;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        
        m_GrabAction[m_Pose.inputSource].onChange += Grab;
    }

    private void OnDestroy()
    {
        m_GrabAction[m_Pose.inputSource].onChange -= Grab;
    }

    private void OnTriggerEnter(Collider other)
    {
        m_Animator.SetBool("Point", true);

    }

    private void OnTriggerExit(Collider other)
    {
        m_Animator.SetBool("Point", false);
    }

    private void Grab(SteamVR_Action_Single action, SteamVR_Input_Sources source, float axis, float delta)
    {
        m_Animator.SetFloat("GrabBlend", axis * gripFistFraction);
    }

    private void Fist(SteamVR_Action_Single action, SteamVR_Input_Sources source, float axis, float delta)
    {
        m_Animator.SetFloat("GrabBlend", axis * gripFistFraction);
    }
}
