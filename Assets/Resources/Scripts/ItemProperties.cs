using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemProperties : MonoBehaviour
{

    public int value;

    // The next unlockable item that can be purchased with the item currency
    //public string nextUnlockableItem;

    // Number of that item exists
    [NonSerialized]
    public int itemCount = 0;


    // [!] Add Constructor
    public ItemProperties()
    {
        
    }

}

// [?] SUB CLASSES
// boneClass
