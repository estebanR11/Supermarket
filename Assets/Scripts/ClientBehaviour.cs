using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClientBehaviour : MonoBehaviour
{
    [SerializeField] int amountOfItems;
    [SerializeField] List<StoreItemSO> itemsBought;
    [SerializeField] List<Transform> itemsLocations;
    [SerializeField] List<ItemType> itemType;

    [SerializeField]ClientState actualState;

    private NavMeshAgent agent;
    [SerializeField] private int currentTargetIndex = 0;
    [SerializeField] QueueManager queueManager;
    [SerializeField] CashRegisterManager cashPosition;
    [SerializeField] Transform exit;
    private void Awake()
    {
        queueManager = GameObject.FindGameObjectWithTag("Queue").GetComponent<QueueManager>();
        cashPosition = GameObject.FindGameObjectWithTag("Cash").GetComponent<CashRegisterManager>();
        exit = GameObject.FindGameObjectWithTag("Exit").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        transform.position = exit.transform.position;
        itemsLocations = new List<Transform>();
        amountOfItems = UnityEngine.Random.Range(2, 8);
        for (int i = 0; i < amountOfItems; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(ItemType)).Length);
            ItemType randomItemType = (ItemType)randomIndex;
            itemType.Add(randomItemType);
            itemsLocations.Add(InterestPointManager.Instance.InterestPointByType(itemType[i]));

        }
        ChangeState(ClientState.Buying);
    }
    private void Start()
    {

  

         
    }
    private void Update()
    {
        if (actualState == ClientState.IDLE){ return;}

        if (actualState == ClientState.Buying)
        {          
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                InterestPoints actualPoint = itemsLocations[currentTargetIndex - 1].GetComponent<InterestPoints>();
                actualPoint.ClientInteract();

                if(actualPoint.IsThereItems())
                 itemsBought.Add(actualPoint.LastItemOnList());

                if (currentTargetIndex < itemsLocations.Count)
                {                
                    agent.SetDestination(itemsLocations[currentTargetIndex].position);                 
                    currentTargetIndex++;
                }
                else
                {                    
                    ChangeState(ClientState.Queueing);
                }
            }
        }
        if(actualState == ClientState.Leaving)
        {
            if (agent.remainingDistance < 0.1f)
            {
                this.gameObject.SetActive(false);
            }
        }
        

    }

    public void ChangeState(ClientState state)
    {
        actualState = state;
        switch (state)
        {
            case ClientState.Buying:
         
                currentTargetIndex = 0;
                if (currentTargetIndex < itemsLocations.Count)
                {
                    agent.SetDestination(itemsLocations[currentTargetIndex].position);
                    currentTargetIndex++;
                }
                break;

            case ClientState.Queueing:
                agent.SetDestination(queueManager.OcupySpot(this).position);
                break;
            case ClientState.Paying:
                agent.SetDestination(cashPosition.GetClientPos().position);
                if(cashPosition.GetEmpty())
                  cashPosition.ClientArrives(this);
                break;
            case ClientState.Leaving:
                agent.SetDestination(exit.position);
                break;
            case ClientState.IDLE: break;
        }
    }

    public void MoveToTheNextQueueSpot(Transform newPosition)
    {
        agent.SetDestination(newPosition.position);
    }

    public List<StoreItemSO> itemsBoughtList()
    {
        return itemsBought;
    
    }
}
public enum ClientState
{
    IDLE,
    Buying,
    Queueing,
    Paying,
    Leaving
}
