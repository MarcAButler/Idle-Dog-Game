using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Spawning
{
    // The item for the particular spawner
    public GameObject item;

    // The amount of items of that type allowed to spawn before no more will spawn
    public int spawnCap;

    //[HideInInspector]
    //public int itemCount = 0;

    // How quickly each item will spawn
    public float spawnFrequency;

    // The amount of the item that spawns per spawn
    public int spawnAmount;

    public GameObject nextUnlockable;

    float defaultWaitTime = 5f;


    void spawnAccordingToSpawnAmount(int spawnAmount, GameObject itemType)
    {
        for (int i = 0; i < spawnAmount; i++)
        {

            Debug.Log("[INSIDEFUNC]item to spawn: " + itemType);
            // Prevents from going over the cap
            if ((itemCount + spawnAmount) > spawnCap)
            {
                // itemCount = spawnCap

                // Items remaining to completely fill the spawnCap
                int itemsToSpawn = spawnCap - itemCount;

                for (int j = 0; j < itemsToSpawn; j++)
                {
                    spawnObject(item);
                    itemCount++;
                    Debug.Log("[item_count]: " + itemCount);
                }

                // Break out of the for loop
                break;
            }
            //Debug.Log("itemCount: " + itemCount);//item.GetComponent<ItemProperties>().itemCount);


            spawnObject(item);
            itemCount++;

            Debug.Log("[item_count]: " + itemCount);

        }

    }

    // Adapter function as a coroutine to call spawnObject(item)
    IEnumerator Spawn()
    {
        while(true)
        {
            while (itemCount < spawnCap)
            {
                spawnAccordingToSpawnAmount(spawnAmount, item);
                /*spawnObject(item);
                itemCount++;

                Debug.Log("[item_count]: " + itemCount);*/

                Debug.Log("[OUTSIDEFUNC]item to spawn: " + item);


                yield return new WaitForSeconds(defaultWaitTime / spawnFrequency);

            }

            //Debug.Log("item_count is exceeded!! [item_count]: " + itemCount);

            yield return null;
        }

        
    }




    void Start()
    {
        StartCoroutine(Spawn());
    }
}
