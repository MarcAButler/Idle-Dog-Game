using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawning : ItemProperties
{
    // Used for unlockSpawner() function for adjustments
    static int initialCurrencyAdjustment = 20;
    // The UI adjustment added to initialCurrencyAdjustment
    static int currencyAdjustment = 20;

    float varyObjectSize(GameObject gameobject)
    {
        float newSize = Random.Range(1.0f, 3.0f);
        gameobject.transform.localScale = new Vector3(newSize, 1.0f, newSize);

        // Change mass according to the newSize
        gameobject.GetComponent<Rigidbody2D>().mass = newSize;

        // Return newSize so that updateValue (and potentionally spawnObject) may use it for further calculations
        return newSize;
    }

    // [?] Move to CurrencyManager? - Make public so this script can access?
    // [?] Consider making public? - change values in the inspector?
    // changes the value of the object by multiplying its size against its value
    void updateValue(GameObject obj, int value, float mass)
    {
        obj.GetComponent<ItemProperties>().value = value * Mathf.RoundToInt(mass);
    }

    public void spawnObject(GameObject spawnableObject)
    {
        GameObject obj = Instantiate(spawnableObject) as GameObject;
        obj.transform.position = new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(-4.5f, 4.5f));
        float newSize = varyObjectSize(obj);

        // Alias the value property of the object as value
        int value = obj.GetComponent<ItemProperties>().value;

        // Pass the objects value and object size (mass) into the updateValue() function
        // Updates the value of the object
        updateValue(obj, value, newSize);
    }

    // Helper function of unlockSpawner()
    // Changes currency name in the inspector as well as its children
    // Also Moves objects under the CurrencyManager in the inspector and adjusts to CurrencyManager's relative position
    void adjustCurrencyName(GameObject spawnerItem, GameObject currency)
    {
        Debug.Log("spawnerItem.name: " + spawnerItem.name);



        // Change name of Currency Type
        currency.name = $"{spawnerItem.name}Currency";

        // Move under CurrencyManager GameObject
        currency.transform.SetParent(GameObject.Find("CurrencyManager").transform, false);

        // Adjust position local to currencymanager
        currency.transform.localPosition = new Vector3(-38, -initialCurrencyAdjustment, 0);

        // Update currencyAdjustment to prepare for next time
        initialCurrencyAdjustment += currencyAdjustment;

        // CHANGE CHILDREN NAME - TEXT AND IMAGE
        currency.transform.GetChild(0).name = $"{spawnerItem.name}Text";
        currency.transform.GetChild(1).name = $"{spawnerItem.name}Image";


    }

    // Helper function to adjustImage()
    // Add text as reference in currency manager
    void referenceCurrency(GameObject spawnerItem, GameObject currency)
    {

        if (spawnerItem.name == "Bone")
        {
            currency.transform.parent.GetComponent<CurrencyManager>().boneCurrencyText = currency.transform.GetChild(0).GetComponent<Text>();
        }
        else if (spawnerItem.name == "Meat")
        {
            // ADD NEW TEXT AS REFERENCE IN CURRENCY MANAGER (just as BoneText is)
            currency.transform.parent.GetComponent<CurrencyManager>().meatCurrencyText = currency.transform.GetChild(0).GetComponent<Text>();
        }
        else if (spawnerItem.name == "Carrot")
        {
            // ADD NEW TEXT AS REFERENCE IN CURRENCY MANAGER (just as BoneText is)
            currency.transform.parent.GetComponent<CurrencyManager>().carrotCurrencyText = currency.transform.GetChild(0).GetComponent<Text>();
            
        }
        else
        {
            Debug.Log("FAILURE!");
        }
    }

    // Helper function of unlockSpawner()
    // Adjusts the image
    // Sets image to native size
    
    void adjustImage(GameObject spawnerItem, GameObject currency)
    {
        // CHANGE IMAGE
        // [!] Do not hardcode Images
        currency.transform.GetChild(1).GetComponent<Image>().sprite = spawnerItem.GetComponent<SpriteRenderer>().sprite;

        // SET NATIVE SIZE
        //[!] Do not hardcode Native Size
        currency.transform.GetChild(1).GetComponent<Image>().SetNativeSize();

        //Debug.Log("SpawnerItem: " + spawnerItem.GetComponent<Image>().sprite);

        /*// ADD NEW TEXT AS REFERENCE IN CURRENCY MANAGER (just as BoneText is)
        currency.transform.parent.GetComponent<CurrencyManager>().meatCurrencyText = currency.transform.GetChild(0).GetComponent<Text>();*/
        referenceCurrency(spawnerItem, currency);

    }


    // Instantiate a spawner game object that spawns certain items
    // Sets the default Spawn Amount to 1
    public void unlockSpawner(GameObject spawnerItem)
    {
        // [!]
        Debug.Log($"[spawnerItem]: {spawnerItem.name}Spawner");

        Debug.Log("UNLOCKING SPAWNER...");

        GameObject spawner = Instantiate(Resources.Load($"Prefabs/Spawners/{spawnerItem.name}Spawner")) as GameObject;


        // Set the spawner item
        spawner.GetComponent<Spawner>().item = spawnerItem;

        // [!] Do not hardcode spawner name!
        // Change the name of the spawner
        spawner.name = $"{spawnerItem.name}Spawner";



        // ADD CURRENCY
        // Create child object
        GameObject currency = Instantiate(Resources.Load("Prefabs/UI_UX/BoneCurrency")) as GameObject;

        adjustCurrencyName(spawnerItem, currency);
        adjustImage(spawnerItem, currency);


        // SET DEFAULT spawnAmount to 1 of the new spawner
        spawner.GetComponent<Spawner>().spawnAmount = 1;

    }

    // Sets the spawner item for a given spawner (bone, meat, etc.)
    // Used in conjunction with the unlockSpanwer function
    /*public void setSpawnerItem(GameObject spawnerItem)
    {
        //return spawnerItem;
    }*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
