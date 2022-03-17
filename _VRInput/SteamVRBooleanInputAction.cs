using UnityEngine;
using Valve.VR;

public class SteamVRBooleanInputAction : BooleanInputAction
{
    public SteamVR_Action_Boolean steamVRInputAction;
    public SteamVR_Input_Sources inputSource;

    public override bool FallingEdge()
    {
        return steamVRInputAction.GetStateDown(inputSource);
    }

    public override bool RisingEdge()
    {
        return steamVRInputAction.GetStateUp(inputSource);
    }

    public override bool GetState()
    {
        return steamVRInputAction.GetState(inputSource);
    }
}