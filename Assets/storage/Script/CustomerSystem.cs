using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSystem : MonoBehaviour
{

    public GameObject[] customerPrefabs;
    public Transform[] spawnPoints; // Have 2 Transform (Left and Right)
    public Transform[] spaceCustomers; // Have two Spaces in front of the cashier
    public float minSpawnDelay = 1f; // Minimum delay before spawning the next customer
    public float maxSpawnDelay = 3f; // Maximum delay before spawning the next customer
    public int customersPerChapter = 2; // Number of customers to serve in each chapter
    private WaitForSeconds spawnDelay; // Delay between customer spawns
    public int customersServed; // Number of customers served in the current chapter
    public bool[] spaceCustomerOccupied; // Array to track space customer occupancy
    public Setting setting;
    int x = 0;

    private void Start()
    {
        spawnDelay = new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        spaceCustomerOccupied = new bool[spaceCustomers.Length]; // i set to have 2 bools now
        StartCoroutine(SpawnCustomers());
    }

    void Update()
    {
        customersPerChapter = setting.customersPerChapter;
    }

    private IEnumerator SpawnCustomers()
    {
        while (customersServed < customersPerChapter)
        {
            // Check if any space customer is empty
            bool isSpaceAvailable = false;
            for (int i = 0; i < spaceCustomerOccupied.Length; i++)
            {
                if (!spaceCustomerOccupied[i]) // Space customer is not occupied
                {
                    isSpaceAvailable = true;
                    x = i;
                    break;
                }
            }

            if (isSpaceAvailable)
            {
                GameObject randomPrefab = customerPrefabs[Random.Range(0, customerPrefabs.Length)];

                // Spawn a customer
                GameObject customer = Instantiate(randomPrefab, GetRandomSpawnPoint(), Quaternion.identity);
                Customer customerController = customer.GetComponent<Customer>();

                // Set the customer's target position and assign the next customer's spawn delay
                customerController.spacecustomerindex = x;
                customerController.targetPosition = GetTargetPosition();
                customerController.spawnDelay = Random.Range(2f, 4f);
                customersServed++;
            }

            yield return spawnDelay;
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex].position;
    }
    

    private Vector3 GetTargetPosition()
    {
        spaceCustomerOccupied[x] = true;
        Vector3 targetPosition = spaceCustomers[x].position;
        return targetPosition;
    }
    

}