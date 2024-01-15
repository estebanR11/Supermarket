using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoreItem", menuName = "Store Menu/Item Container")]
public class StorageItemsDataSO : ScriptableObject
{
    public StoreItemSO[] item;
    public ItemType typeOfProduct;
}
