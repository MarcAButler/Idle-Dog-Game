using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Store : MonoBehaviour
{
    public static GameObject lastStorePage;

    void Start()
    {
        lastStorePage = GameObject.Find("StorePage1");
        Debug.Log("~lastStorePage~ " + lastStorePage);
    }
    void OnDisable()
    {
        Debug.Log("DISABLED STORE PAGE...");
        //lastStorePage = GameObject.Find("StorePage1");
    }

    //GameObject lastStorePage;

    static bool shopShowing = false;

    static float currentSpawnFrequencyUpgrade = 0f;
    public float spawnFrequencyUpgradeAmount = 0.25f;
    //[!] Apt to change! - consider using table! Move to spawner as a property??
    static int currentSpawnFrequencyUpgradeCost = 10;
    static int currentSpawnCapUpgradeCost = 10;
    static int currentSpawnAmountUpgradeCost = 10;
    static int unlockItemCost = 10;

    // The way that currentSpawnFrequencyUpgradeCost gets updated every sucessful purchase
    public int upgradeCostIncreasement = 5;

    // CONSIDER MAKING TABLE / DICTONARY FOR TIERS OF SPAWN AMOUNT INCREASE
    // (Spawn cap will not increase by +1 every upgrade; it may increase by +5 at some point)
    static int currentSpawnCapUpgrade = 0;
    public int spawnCapUpgradeAmount = 1;

    static int currentAmountUpgrade = 0;
    public int spawnAmountUpgradeAmount = 1;





    // [STORE PAGE NUMBERS]
    // - Current page variable
    static string storePageNumberCurrent = "1";
    // - The last page navigatable too
    static string storePageNumberLimit;
    // - The Last Store Page Object Accessed

    // Either takes down or brings up the menu
    public void switchShopState()
    {
        Debug.Log("[LAST STORE PAGE]: " + lastStorePage);

        // Flips the menu on or off
        if (shopShowing)
        {
            shopShowing = !shopShowing;
            //Debug.Log("Menu Not Showing");
        }
        else
        {
            shopShowing = true;
            //Debug.Log("Menu Showing");
        }

            lastStorePage.SetActive(shopShowing);
    }

    // Increments the page number
    // - Takes a direction[RIGHT, LEFT] and an adjustement[prevPageNumber, nextPageNumber]
    public void updatePageNumber(string direction, int adjustment)
    {
       // Debug.Log("[UPDATE PAGE NUMBER: BEFORE IF - lastStorePage] " + lastStorePage);
        //Debug.Log("[UPDATE PAGE NUMBER: BEFORE IF - direction] " + direction);
        if (direction == "RIGHT")
        {
            
            /*Debug.Log("[PAGE NUMBER CHANGE - RIGHT] " + this.transform.parent.GetChild(adjustment - 1).Find("StorePageNumber").GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text[0]);// = $"1/{storePageNumber}";
            string oldPageNumber = this.transform.parent.GetChild(adjustment - 1).Find("StorePageNumber").GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text[0] + " ";
            int adjustedPageNumberInt = int.Parse(oldPageNumber) + 1;
            string adjustedPageNumber = adjustedPageNumberInt.ToString();*/

            int adjustedPageNumberInt = int.Parse(storePageNumberCurrent) + 1;
            string adjustedPageNumber = adjustedPageNumberInt.ToString();
            storePageNumberCurrent = adjustedPageNumber;
            //Debug.Log("[UPDATE PAGE NUMBER: BEFORE ASSIGNEMENT - lastStorePage] " + lastStorePage);
            //Debug.Log("[UPDATE PAGE NUMBER: BEFORE ASSIGNEMENT - adjustment] " + adjustment);

            //Debug.Log("[this.transform.parent.GetChild(adjustment).gameObject] " + this.transform.parent.GetChild(adjustment - 1).gameObject);
            Debug.Log("[this] " + GameObject.Find("StorePage" + adjustment).GetComponentsInChildren<Transform>(true));
            // [?] Update the last page accessed
            lastStorePage = GameObject.Find("StorePage" + adjustment); //this.transform.parent.GetChild(adjustment - 1).gameObject;

            //Debug.Log("[UPDATE PAGE NUMBER: AFTER ASSIGNMENT - lastStorePage] " + lastStorePage);


            this.transform.parent.GetChild(adjustment - 1).Find("StorePageNumber").GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = $"{storePageNumberCurrent}/{storePageNumberLimit}";
        }
        else if (direction == "LEFT")
        {
            /*Debug.Log("[PAGE NUMBER CHANGE - LEFT] " + this.transform.parent.GetChild(adjustment - 1).Find("StorePageNumber").GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text[0]);
            string oldPageNumber = this.transform.parent.GetChild(adjustment - 1).Find("StorePageNumber").GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text[0] + " ";
            int adjustedPageNumberInt = int.Parse(oldPageNumber);
            string adjustedPageNumber = adjustedPageNumberInt.ToString();
            Debug.Log("[ADJUSTEDPAGENUMBER]" + adjustedPageNumber);*/

            int adjustedPageNumberInt = int.Parse(storePageNumberCurrent) - 1;
            string adjustedPageNumber = adjustedPageNumberInt.ToString();
            storePageNumberCurrent = adjustedPageNumber;

            // [?] Update the last page accessed
            lastStorePage = this.transform.parent.GetChild(adjustment - 1).gameObject;

            this.transform.parent.GetChild(adjustment - 1).Find("StorePageNumber").GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = $"{storePageNumberCurrent}/{storePageNumberLimit}";
        }
    }


    // Turn Shop Page
    public void turnShopPage(string direction)
    {
        if (direction == "RIGHT")
        {
            Debug.Log("[RIGHT PRESSED]");
            Debug.Log(this.name);
           

            int currentPageNumber = int.Parse(this.name[this.name.Length - 1] + " ");
            int nextPageNumber = currentPageNumber + 1;

            GameObject unlockUpgrade = this.transform.parent.GetChild(currentPageNumber - 1).Find("UnlockItemUpgrade").gameObject ;//Find("UnlockItemUpgrade").gameObject;

            
            ///Debug.Log("[IS ACTIVE]: " + unlockUpgrade.activeSelf);
            if (!unlockUpgrade.activeSelf)
                //Debug.Log("[IS ACTIVE IS TRUE]");
                try
                {
                    ///Debug.Log("[PAGE SWITCHING]: " + this.transform.parent.GetChild(currentPageNumber - 1).gameObject);
                    // Sees if there is a corresponding page number and throws an exception to prevent further code from executing
                    // - [PSUEDO CODE] if (currentPageNumber == EXISTS)
                    if (this.transform.parent.GetChild(nextPageNumber - 1).gameObject != null)
                    // Closes the current page
                    this.transform.parent.GetChild(currentPageNumber - 1).gameObject.SetActive(false);
                    // Opens the next page
                    this.transform.parent.GetChild(nextPageNumber - 1).gameObject.SetActive(true);

                    //Debug.Log("[TURN SHOP PAGE - lastStorePage] " + lastStorePage);

                    // UPDATE THE PAGE NUMBER
                    updatePageNumber(direction, nextPageNumber);                    
                }
                catch (System.Exception)
                {
                    Debug.Log("[PAGE DOES NOT EXIST]");
                    //throw; // Shows error message
                }
            else
                Debug.Log("[HAVE NOT PURCHASED UPGRADE]");
                

            

        }
        else if (direction == "LEFT")
        {
            Debug.Log("[LEFT PRESSED]");
            Debug.Log(this.name);

            int currentPageNumber = int.Parse(this.name[this.name.Length - 1] + " ");
            int prevPageNumber = currentPageNumber - 1;

            
            try
            {
                ///Debug.Log("[PAGE SWITCHING]: " + this.transform.parent.GetChild(currentPageNumber - 1).gameObject);
                // Sees if there is a corresponding page number and throws an exception to prevent further code from executing
                // - [PSUEDO CODE] if (currentPageNumber == EXISTS)
                if (this.transform.parent.GetChild(prevPageNumber - 1).gameObject != null)
                // Closes the current page
                this.transform.parent.GetChild(currentPageNumber - 1).gameObject.SetActive(false);
                // Opens the next page
                this.transform.parent.GetChild(prevPageNumber - 1).gameObject.SetActive(true);

                updatePageNumber(direction, prevPageNumber);

            }
            catch (System.Exception)
            {
                Debug.Log("[PAGE DOES NOT EXIST]");
                //throw; // Shows error message
            }
        }
    }


    // Checks if the player has enough of a currency compared to the cost of a purchase
    // Calls the subtractCurrencyFunction from CurrencyManager.cs with a given currencyType
    bool verifyPurchase(int currencyAmount, int cost, string currencyType, string upgradeCostType)
    {
        if (currencyAmount >= cost)
        {
            // Subtracts from the players Currency
            GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>().getCurrency(currencyType, cost, true);

            // Increases Next Upgrade Cost by a given increment
            //upgradeCostType += upgradeCostIncreasement;

            switch(upgradeCostType)
            {
                case "currentSpawnFrequencyUpgradeCost":
                    currentSpawnFrequencyUpgradeCost += upgradeCostIncreasement;
                    break;
                case "currentSpawnCapUpgradeCost":
                    currentSpawnCapUpgradeCost += upgradeCostIncreasement;
                    break;
                case "currentSpawnAmountUpgradeCost":
                    currentSpawnAmountUpgradeCost += upgradeCostIncreasement;
                    break;

            }

            // Continues with upgrade
            return true;
        }
        else
        {
            // does not upgrade
            return false;
        }
    }


    // Buys the spawn frequency increase
    public void purchaseSpawnFrequencyIncrease(GameObject spawner)
    {
       
        // Determine currency type - Used to grab the correct currency in the Currency Manager
        string itemType = spawner.GetComponent<Spawner>().item.name;
        string currencyType = $"current{itemType}Currency";

        int currencyAmount = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>().getCurrency(currencyType, currentSpawnFrequencyUpgradeCost);

        Debug.Log("Currency BEFORE Verification: " + currencyAmount);

        // Check funds
        bool canBuy = verifyPurchase(currencyAmount, currentSpawnFrequencyUpgradeCost, currencyType, "currentSpawnFrequencyUpgradeCost");

        currencyAmount = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>().getCurrency(currencyType, currentSpawnFrequencyUpgradeCost);

        Debug.Log("Currency AFTER Verification: " + currencyAmount);

        if (canBuy)
        {

            GameObject.Find(spawner.name).GetComponent<Spawner>().spawnFrequency += spawnFrequencyUpgradeAmount;

            // [OLD] spawner.GetComponent<Spawner>().spawnFrequency += spawnFrequencyUpgradeAmount;

            // Useful for displaying how far the player has progressed
            currentSpawnFrequencyUpgrade += spawnFrequencyUpgradeAmount;

            Debug.Log("PURCHASED SPAWN FREQUENCY INCREASE");
        }
        else
        {
            Debug.Log("INSUFICIENT FUNDS; CANNOT PURCHASE UPGRADE");
        }
    }

    // Buys the spawn cap increase
    public void purchaseSpawnCapIncrease(GameObject spawner)
    {
       

        // Determine currency type - Used to grab the correct currency in the Currency Manager
        string itemType = spawner.GetComponent<Spawner>().item.name;
        string currencyType = $"current{itemType}Currency";

        int currencyAmount = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>().getCurrency(currencyType, currentSpawnCapUpgradeCost);

        Debug.Log("Currency BEFORE Verification: " + currencyAmount);

        // Check funds
        bool canBuy = verifyPurchase(currencyAmount, currentSpawnCapUpgradeCost, currencyType, "currentSpawnCapUpgradeCost");

        currencyAmount = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>().getCurrency(currencyType, currentSpawnCapUpgradeCost);

        Debug.Log("Currency AFTER Verification: " + currencyAmount);

        if (canBuy)
        {

            GameObject.Find(spawner.name).GetComponent<Spawner>().spawnCap += spawnCapUpgradeAmount;

            // [OLD] spawner.GetComponent<Spawner>().spawnCap += spawnCapUpgradeAmount;

            // Useful for displaying how far the player has progressed
            currentSpawnCapUpgrade += spawnCapUpgradeAmount;

            Debug.Log("PURCHASED SPAWN CAP INCREASE");
        }
        else
        {
            Debug.Log("INSUFICIENT FUNDS; CANNOT PURCHASE UPGRADE");
        }

        /*// [!] Check if player has necessary currency!!

        // CODE TO DO SPAWN CAP INCREASE
        spawner.GetComponent<Spawner>().spawnCap += spawnCapUpgradeAmount;

        // Useful for displaying how far the player has progressed
        currentSpawnCapUpgrade += spawnCapUpgradeAmount;*/
    }

    // Buys the spawn cap increase
    public void purchaseSpawnAmountIncrease(GameObject spawner)
    {

        // Determine currency type - Used to grab the correct currency in the Currency Manager
        string itemType = spawner.GetComponent<Spawner>().item.name;
        string currencyType = $"current{itemType}Currency";

        int currencyAmount = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>().getCurrency(currencyType, currentSpawnAmountUpgradeCost);

        Debug.Log("Currency BEFORE Verification: " + currencyAmount);

        // Check funds
        bool canBuy = verifyPurchase(currencyAmount, currentSpawnAmountUpgradeCost, currencyType, "currentSpawnAmountUpgradeCost");

        currencyAmount = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>().getCurrency(currencyType, currentSpawnAmountUpgradeCost);

        Debug.Log("Currency AFTER Verification: " + currencyAmount);

        if (canBuy)
        {
          

            // CODE TO DO SPAWN AMOUNT INCREASE
            GameObject.Find(spawner.name).GetComponent<Spawner>().spawnAmount += spawnAmountUpgradeAmount;


            // [OLD] CODE TO DO SPAWN AMOUNT INCREASE
            // spawner.GetComponent<Spawner>().spawnAmount += spawnAmountUpgradeAmount;

            // Useful for displaying how far the player has progressed
            currentAmountUpgrade += spawnAmountUpgradeAmount;

            Debug.Log("PURCHASED SPAWN AMOUNT INCREASE");

        }
        else
        {
            Debug.Log("INSUFICIENT FUNDS; CANNOT PURCHASE UPGRADE");
        }
        
    }

    public void purchaseUnlockableItem(GameObject spawner)
    {

        // Determine currency type - Used to grab the correct currency in the Currency Manager
        string itemType = spawner.GetComponent<Spawner>().item.name;
        string currencyType = $"current{itemType}Currency";

        int currencyAmount = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>().getCurrency(currencyType, unlockItemCost);

        Debug.Log("Currency BEFORE Verification: " + currencyAmount);

        // Check funds
        bool canBuy = verifyPurchase(currencyAmount, unlockItemCost, currencyType, "unlockItemCost");

        // [!] Use another effcient method than find gameobject
        currencyAmount = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>().getCurrency(currencyType, unlockItemCost);

        Debug.Log("Currency AFTER Verification: " + currencyAmount);

        if (canBuy)
        {

            //Debug.Log("GAMEOBJECT:");
            //Debug.Log(this.transform.parent.name);

            this.transform.parent.gameObject.SetActive(false);
            // CODE TO DO SPAWN AMOUNT INCREASE
            //spawner.GetComponent<Spawner>().spawnAmount += spawnAmountUpgradeAmount;

            // Useful for displaying how far the player has progressed
            //currentAmountUpgrade += spawnAmountUpgradeAmount;
            GameObject nextUnlockable = spawner.GetComponent<Spawner>().nextUnlockable;
            GetComponent<Spawning>().unlockSpawner(nextUnlockable);

            // Update page text [1/#]
            // - For loop and function
            // Full text such as [1/1]
            string oldPages = this.transform.parent.transform.parent.name;
            // New text
            oldPages = this.transform.parent.transform.parent.name[oldPages.Length - 1] + " ";//'9';
            // Convert string to int
            int oldPagesInt = int.Parse(oldPages);

            string newPages = (oldPagesInt + 1).ToString();

            // Update current page number limit globbaly
            storePageNumberLimit = newPages;

            Debug.Log("[newPages DEBUG] " + newPages);


            ///Debug.Log("[PAGE NUMBER DEBUG] " + this.transform.parent.transform.parent.transform.Find("StorePageNumber").GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text);
            // Gets the value of the StorePageNumbers text so it may be updated
            ///this.transform.parent.transform.parent.transform.Find("StorePageNumber").GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = $"1/{newPages}";

            // [!] Update All Store Pages with the current page variable
            // [1] Find the store
            // [2] Iterate through all pages of the store
            GameObject store = GameObject.Find("Store");
            foreach (Transform storePage in store.transform)
                //Debug.Log("[STOREPAGE] " + storePage.name);
                ///Debug.Log("[STOREPAGE] " + storePage.transform.Find("StorePageNumber").GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text);
                storePage.transform.Find("StorePageNumber").GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = $"1/{storePageNumberLimit}";


            // [EditDate | 5 / 7 / 2021]: Set Active of the next unlockablespawner object located in the hierarchy (currently)
            //Debug.Log("[SET ACTIVE NEW SPAWNER] " + nextUnlockable.name);
            // [!] Use another effcient method than find gameobject
            // Sets the spawner to active
            ///GameObject.Find(nextUnlockable.name + "Spawner").SetActive(true);

            Debug.Log("UNLOCKED ITEM SUCCESSFULLY");

        }
        else
        {
            Debug.Log("INSUFICIENT FUNDS; CANNOT PURCHASE UPGRADE");
        }

    }


    // Detects if an SpaceKey was pressed
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("SPACE PRESSED");
            switchShopState();
        }

        // Updates the cost displayed in the UI of the store
        //[!] Do not hardcode values!!
        // Get the text for the Cost
        try
        {
            GameObject.Find("BoneFrequencyCost").transform.GetChild(0).GetComponent<Text>().text = currentSpawnFrequencyUpgradeCost.ToString();
            GameObject.Find("BoneSpawnCapCost").transform.GetChild(0).GetComponent<Text>().text = currentSpawnCapUpgradeCost.ToString();
            GameObject.Find("BoneSpawnAmountCost").transform.GetChild(0).GetComponent<Text>().text = currentSpawnAmountUpgradeCost.ToString();
            GameObject.Find("BoneUnlockItemCost").transform.GetChild(0).GetComponent<Text>().text = unlockItemCost.ToString();
        }
        catch
        { 
        }

        Debug.Log("[LAST STORE PAGE]: " + Store);


        //Debug.Log("textTEST: " + GameObject.Find("BoneFrequencyCost").transform.GetChild(0).GetComponent<Text>().text);
    }
}
