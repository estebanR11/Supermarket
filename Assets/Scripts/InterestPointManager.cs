using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterestPointManager : MonoBehaviour
{
    [SerializeField] InterestPoints[] allInterestPoints;

    public static InterestPointManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Transform InterestPointByType(ItemType type)
    {
        List<InterestPoints> list = new List<InterestPoints> ();

        for(int i=0; i<allInterestPoints.Length; i++)
        {
            if (allInterestPoints[i].GetGroceryType().Equals(type))
            {
                list.Add(allInterestPoints[i]); 
            }
        }
        Transform groceryPosition = list[Random.Range(0, list.Count)].transform;
        return groceryPosition;

    }
}
