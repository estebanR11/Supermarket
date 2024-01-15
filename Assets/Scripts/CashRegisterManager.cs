using JetBrains.Annotations;
using Pinwheel.Griffin.SplineTool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CashRegisterManager : MonoBehaviour
{
    bool isEmpty = true;
    bool hasInstantiated = false;
    [SerializeField] QueueManager queueManager;
    [SerializeField]ClientBehaviour clientOnCash;
    [SerializeField] Transform clientProducts;
    [SerializeField] Transform clientPos;
    List<StoreItemSO> clientItems;
    List<GameObject> products;

    bool count = false;
    AudioSource asource;
    private void Start()
    {
        asource = GetComponent<AudioSource>();  
        products= new List<GameObject> ();
    }
    public void ClientArrives(ClientBehaviour newClient)
    {
        isEmpty = false;
        clientOnCash = newClient;
    }

    public void ClientLeaves()
    {       
        StopAllCoroutines();
        clientItems = new List<StoreItemSO>();
        foreach(GameObject product in products)
        {
            Destroy(product);
        }
        clientOnCash.ChangeState(ClientState.Leaving);
        clientOnCash = null;
        isEmpty = true;
        hasInstantiated = false;
        count = false;

    }

    public bool GetEmpty()
    {
        return isEmpty;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (isEmpty)
            {
                if (queueManager.IsPeopleOnQueue())
                    queueManager.LeaveQueue();
            }
            else
            {
                clientItems = clientOnCash.itemsBoughtList();
                for (int i =0;i< clientItems.Count && !hasInstantiated;i++)
                {                  
                   GameObject product = Instantiate(clientItems[i].itemPrefab);
                    product.transform.parent = clientProducts;
                    products.Add(product);
                }
                if(!count)
                {count=true;
                    StartCoroutine(Paying());
                    hasInstantiated = true;
                }
         
            }


        }
    }

    IEnumerator Paying()
    {
        float totalValue = 0.0f;
        for (int i = 0; i < clientItems.Count; i++)
        {
            totalValue += clientItems[i].price;
        }

            yield return new WaitForSeconds(5.0f);
        asource.Play();
        GameManager.Instance.SellProduct(totalValue);
        ClientLeaves();
    }

    public Transform GetClientPos()
    {
        return clientPos;
    }
}
