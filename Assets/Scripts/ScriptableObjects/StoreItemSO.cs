using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "StoreItem", menuName = "Store Menu/Item")]
public class StoreItemSO : ScriptableObject
{
        public string itemName;
        public float price;
        public GameObject itemPrefab;
  
}
[System.Serializable]
public enum ItemType
{
    Cleaning,
    FastFood,
    Meat,
    Freezer,
    Vegetables_and_fruits,
    Sodas,
    Misc
}
