using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class InterestPoints : MonoBehaviour
{
    [SerializeField] Transform layoutTransform;
    [SerializeField] ItemType itemType;
    int ammountOfProducts =0;
    [SerializeField] int maxAmmount;
    [SerializeField] StorageItemsDataSO randomGameobjects;

    [SerializeField] TextMeshProUGUI nameOfProduct;

    List<StoreItemSO> productsOnShelf;
    List<GameObject> productsOnShelfGO;
    private void Start()
    {
        productsOnShelf = new List<StoreItemSO>();
        productsOnShelfGO   = new List<GameObject>();
        for(int i= 0;i <maxAmmount;i++)
        {
            int RandomNumber = Random.Range(0, randomGameobjects.item.Length);
            GameObject item = Instantiate(randomGameobjects.item[RandomNumber].itemPrefab);
            item.transform.parent = layoutTransform;
            ammountOfProducts++;
            productsOnShelfGO.Add(item);
            productsOnShelf.Add(randomGameobjects.item[RandomNumber]);
        }

      //  nameOfProduct.text = itemType.ToString();
    }

    public void PlayerInteract()
    {
        if(ammountOfProducts < maxAmmount)
        {
            float value = productsOnShelf[ammountOfProducts - 1].price * 0.8f;
            GameManager.Instance.ReStock(value);
            productsOnShelfGO[ammountOfProducts - 1].SetActive(true);
            ammountOfProducts++;
        
        }
        
    }

    public void ClientInteract()
    {
         if(IsThereItems())
        {
            productsOnShelfGO[ammountOfProducts - 1].SetActive(false);
            ammountOfProducts--;

        }

    }

    public bool IsThereItems()
    {
        if(ammountOfProducts <=0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public StoreItemSO LastItemOnList()
    {
        return productsOnShelf[ammountOfProducts-1];
    }
    
    public ItemType GetGroceryType()
    {
        return itemType;
    }

}
