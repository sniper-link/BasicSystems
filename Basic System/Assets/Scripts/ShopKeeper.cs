using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    const int SHOPSIZE = 8;

    public Pickup[] shopItemList = new Pickup[SHOPSIZE];
    public GameObject itemPosGroup;
    private GameObject[] itemPosList = new GameObject[SHOPSIZE];
    public GameObject shopKeeperPos;

    private void Awake()
    {
        if (shopItemList.Length > SHOPSIZE)
        {
            Debug.Log("Shop can't carry more than 9 items");
        }
        CheckItemPosList();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void CheckItemPosList()
    {
        for (int i = 0; i < SHOPSIZE; i++)
        {
            itemPosList[i] = itemPosGroup.transform.GetChild(i).gameObject;
            Debug.Log(itemPosList[i].name);
        }
        
    }

}
