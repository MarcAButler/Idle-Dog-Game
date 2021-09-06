using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inheritance from Currency Manager so that particular objects add to the currency when they go off border
public class Boundary : MonoBehaviour//CurrencyManager
{
    public GameObject obj;
    //public GameObject currencyManager;

    

    // Note that the boundries range from their negative complements (i.e. -7.5 / -4.5)
    float boundaryX = 10.1f;
    float boundaryY = 4.5f;


    void Start()
    {
        //CurrencyManager currencyManager = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>();

    }

    // Checks if the GameObject is a currency
    //  - Useful for the checkBoundary() function
    void checkCurrency(GameObject gameObject)
    {
        Debug.Log("checkCurrency gameObject: " + gameObject.name);
        string objectName = gameObject.name.Replace("(Clone)", "");


        if (gameObject.tag == "Currency")
        {
            //Debug.Log("Currency entered the border");           

            // [!] PASS THE OBJ CREATED IN SPAWNING            

            // Create currencyManager object to access addToCurrency object
            CurrencyManager currencyManager = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>();
            // Add to currency
            currencyManager.addToCurrency(gameObject);

            // [!]:[Will not work for multiple spawners] Find A The Spawner
            ItemProperties spawner = GameObject.Find($"{objectName}Spawner").GetComponent<ItemProperties>();
            // Reduce the itemCount of the item
            spawner.itemCount--;
            Debug.Log($"[{objectName}:item_count]: " + spawner.itemCount);

            Destroy(gameObject);

        }
        
    }

    void checkBoundary(GameObject gameObject)
    {
        // Checks if an object has hit the boudary
        if (gameObject.transform.position.x > boundaryX)
        {
            gameObject.transform.position = new Vector2(boundaryX, gameObject.transform.position.y);
            checkCurrency(gameObject);
        }
        if (gameObject.transform.position.x < -boundaryX)
        {
            gameObject.transform.position = new Vector2(-boundaryX, gameObject.transform.position.y);
            checkCurrency(gameObject);
        }
        if (gameObject.transform.position.y > boundaryY)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, boundaryY);
            checkCurrency(gameObject);
        }
        if (gameObject.transform.position.y < -boundaryY)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, -boundaryY);
            checkCurrency(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        checkBoundary(obj);
    }
}
