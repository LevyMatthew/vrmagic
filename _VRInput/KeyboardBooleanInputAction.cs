using UnityEngine;

[CreateAssetMenu(fileName = "KeyboardKey", menuName = "KeyboardBooleanInputAction", order = 1)]
public class KeyboardBooleanInputAction : BooleanInputAction
{
    public KeyCode key;    

    public override bool FallingEdge()
    {
        return Input.GetKeyDown(key);
    }

    public override bool RisingEdge()
    {
        return Input.GetKeyUp(key);
    }

    public override bool GetState()
    {
        return Input.GetKey(key);
    }
}