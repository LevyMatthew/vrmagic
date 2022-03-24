using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class InputManager : MonoBehaviour
{
    [Header("Trigger")]
    public SteamVR_Action_Boolean TriggerAction = null;
    public UnityEvent OnTriggerDown = new UnityEvent();
    public UnityEvent OnTriggerUp = new UnityEvent();

    [Header("Gripper")]
    public SteamVR_Action_Boolean GripperAction = null;
    public UnityEvent OnGripperDown = new UnityEvent();
    public UnityEvent OnGripperUp = new UnityEvent();

    [Header("Touchpad")]
    public SteamVR_Action_Boolean TouchpadAction = null;
    public UnityEvent OnTouchpadDown = new UnityEvent();
    public UnityEvent OnTouchpadUp = new UnityEvent();

    private SteamVR_Behaviour_Pose Pose = null;

    private void Awake()
    {
        Pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
    }

    private void Update()
    {
        if (TriggerAction.GetStateDown(Pose.inputSource))
            OnTriggerDown.Invoke();

        if (TriggerAction.GetStateUp(Pose.inputSource))
            OnTriggerUp.Invoke();

        if (GripperAction.GetStateDown(Pose.inputSource))
            OnGripperDown.Invoke();

        if (GripperAction.GetStateUp(Pose.inputSource))
            OnGripperUp.Invoke();

        if (TouchpadAction.GetStateDown(Pose.inputSource))
            OnTouchpadDown.Invoke();

        if (TouchpadAction.GetStateUp(Pose.inputSource))
            OnTouchpadUp.Invoke();
    }
}
