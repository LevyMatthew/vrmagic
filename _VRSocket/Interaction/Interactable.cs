using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected bool isAvailable = true;

    public virtual void StartInteraction(Hand hand)
    {
        print("StartInteract");
    }

    public virtual void Interaction(Hand hand)
    {
        print("Interact");
    }

    public virtual void EndInteraction(Hand hand)
    {
        print("EndInteract");
    }

    public bool GetAvailability()
    {
        return isAvailable;
    }
}
