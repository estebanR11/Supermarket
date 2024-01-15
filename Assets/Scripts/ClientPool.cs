using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientPool : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] GameObject[] clients;
    float actualTime;
    bool areAllActive;
    // Update is called once per frame
    void Update()
    {
        if (actualTime < timer)
        {
            actualTime += Time.deltaTime;
        }
        else
        {
            if(!clientsActive())
            {
                actualTime = 0;
                for (int i = 0; i < clients.Length; i++)
                {
                    if (!clients[i].activeInHierarchy)
                    {
                        clients[i].SetActive(true);
                        break;
                    }
                }
            }
     
        }
    }

    bool clientsActive()
    {
        for (int i = 0; i < clients.Length; i++)
        {
            if (!clients[i].activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
}
