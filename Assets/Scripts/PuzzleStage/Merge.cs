using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int itemNum;
    public Sprite itemImg;
}
public class Merge : MonoBehaviour
{
    public List<Item> itemdata = new List<Item>();
    public GameObject itemPrefabs;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
