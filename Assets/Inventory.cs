using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class Inventory : MonoBehaviour
{
    public Item CurrentItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item item)
    {
        CurrentItem = item;
    }

    public void RemoveItem()
    {
        CurrentItem = null;
    }

    public void Reset()
    {
        RemoveItem();
    }
}
