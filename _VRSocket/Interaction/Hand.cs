using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Socket))]
public class Hand : MonoBehaviour
{
    private Socket socket = null;
    private SteamVR_Behaviour_Pose pose = null;

    public List<Interactable> contactInteractables = new List<Interactable>();

    private void Awake()
    {
        socket = GetComponent<Socket>();
        pose  = GetComponent<SteamVR_Behaviour_Pose>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AddInteractable(other.gameObject);
    }

    private void AddInteractable(GameObject newObject)
    {
        Interactable newInteractable = newObject.GetComponent<Interactable>();
        if (newInteractable != null)
            contactInteractables.Add(newInteractable);
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveInteractable(other.gameObject);
    }

    private void RemoveInteractable(GameObject newObject)
    {
        Interactable existingInteractable = newObject.GetComponent<Interactable>();
        if (existingInteractable != null)
            contactInteractables.Remove(existingInteractable);
    }

    public void TryInteraction()
    {
        print("Hand Trying Interaction");
        if (TryNearestInteraction())
            return;

        HeldInteraction();
    }

    private bool TryNearestInteraction()
    {
        if (contactInteractables.Count == 0)
            print("No Contact Interactables");

        Interactable nearestObject = Utility.GetNearestInteractable(transform.position, contactInteractables);

        if (nearestObject)
            nearestObject.StartInteraction(this);

        return false;
    }

    private void HeldInteraction()
    {   
        Moveable heldObject = socket.GetStoredObject();

        if (heldObject)
            heldObject.Interaction(this);
    }
    
    public void StopInteraction()
    {
        Moveable heldObject = socket.GetStoredObject();

        if (heldObject)
            heldObject.EndInteraction(this);
    }

    public Socket GetSocket()
    {
        return socket;
    }

    public SteamVR_Behaviour_Pose GetPose()
    {
        return pose;
    }

}