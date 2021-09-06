using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour//Spawning
{
    public Text boneCurrencyText;
    public Text meatCurrencyText;
    public Text carrotCurrencyText;

    // USEFUL FOR TESTING - Change in inspector!

    // [Note]: currentBoneCurrency is made static so that ALL OBJECTS will carry this value and not reset the total count
    public static int currentBoneCurrency = 0;
    public static int currentMeatCurrency = 0;
    public static int currentCarrotCurrency = 0;
    //public int boneMultiplier = 1;


    // Get Currency from a string - typically used by store.cs
    // Currently Handled with if/else statements
    // willSubtract determines if subtractCurrency() is called while it is getting Currency defaulted at false
    public int getCurrency(string currency, int cost, bool willSubtract=false)
    {
        if (currency == "currentBoneCurrency")
        {
            if (willSubtract)
                currentBoneCurrency -= cost;
            return currentBoneCurrency;
        }
        else if (currency == "currentMeatCurrency")
        {
            if (willSubtract)
                currentMeatCurrency -= cost;
            return currentMeatCurrency;
        }
        else if (currency == "currentCarrotCurrency")
        {
            if (willSubtract)
                currentCarrotCurrency -= cost;
            return currentCarrotCurrency;
        }
        else
        {
            Debug.Log("CURRENCY NOT FOUND; Are you missing currency?");
            
            // [!] Should never happen!!
            return -1;
        }
    }

    // Get Currency from a string - typically used by store.cs
    // Subtracts from the given currency
    void subtractCurrency(string currencyType)
    {
        

    }


    // Determine currency type - Currently handled with if-else statments: rework may be necessary
    void determineCurrencyType(GameObject obj)
    {
        if (obj.name == "Bone(Clone)")
        {
            // Get the objects value
            currentBoneCurrency += obj.GetComponent<ItemProperties>().value;
    
        }
        else if (obj.name == "Meat(Clone)")
        {
            // Get the objects value
            currentMeatCurrency += obj.GetComponent<ItemProperties>().value;
        }
        else if (obj.name == "Carrot(Clone)")
        {
            // Get the objects value
            currentCarrotCurrency += obj.GetComponent<ItemProperties>().value;
        }
        else
        {
            Debug.Log("Cannot Determine Currency type of object");
        }

    }



    // [!] Add parameter for currency type! - Currently only bones are supported
    // Takes an object with a value property (obtained from ItemProperties) that had been spawned
    public void addToCurrency(GameObject obj)//float boneSize)
    {
        determineCurrencyType(obj);

    }

    void Update()
    {
        // [DELETE?]
        boneCurrencyText.text = currentBoneCurrency.ToString();
        meatCurrencyText.text = currentMeatCurrency.ToString();
        carrotCurrencyText.text = currentCarrotCurrency.ToString();

    }

}
