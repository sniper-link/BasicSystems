using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : ScriptableObject
{
    public string keyItemName;
    public bool inBag;

    public KeyItem(string name)
    {
        keyItemName = name;
        inBag = true;
    }
}
