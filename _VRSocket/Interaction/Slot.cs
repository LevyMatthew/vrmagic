using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : Interactable
{
   private Socket socket = null;

    private void Awake()
    {
        socket = GetComponent<Socket>();
    }

    public override void StartInteraction(Hand hand)
    {
        Socket handSocket = hand.GetSocket();
        Moveable heldObject = handSocket.GetStoredObject();

        if (heldObject)
        {
            TryStore(heldObject);
        }
        else
        {
            TryRetrieve(handSocket);
        }
    }

    private void TryStore(Moveable objectToStore)
    {
        if (!socket.GetStoredObject())
           objectToStore.AttachNewSocket(socket);
    }

    private void TryRetrieve(Socket newSocket)
    {    
        Moveable objectToRetrieve = socket.GetStoredObject();

        if (objectToRetrieve)
           objectToRetrieve.AttachNewSocket(newSocket);

    }
}
