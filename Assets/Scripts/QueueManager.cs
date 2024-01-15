using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QueueManager : MonoBehaviour
{
    [SerializeField] Transform[] queuePositions;
    int actualOcupied = -1;
    [SerializeField] List<ClientBehaviour> clientsOnQueue = new List<ClientBehaviour>();
    [SerializeField] CashRegisterManager cashRegister;
    public Transform OcupySpot(ClientBehaviour client)
    {
        actualOcupied++;
        clientsOnQueue.Add(client);

        return queuePositions[actualOcupied];
    }

    public void LeaveQueue()
    {
        clientsOnQueue[0].ChangeState(ClientState.Paying);
        actualOcupied--;
        clientsOnQueue.RemoveAt(0);
        for(int i = 0; i < clientsOnQueue.Count; i++)
        {
            ClientBehaviour client = clientsOnQueue[i];
            client.MoveToTheNextQueueSpot(queuePositions[i]);
        }
    }

    public bool IsPeopleOnQueue()
    {
        return actualOcupied > -1;
    }
}
