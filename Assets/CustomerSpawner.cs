using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customerPrefabs;
    public Transform[] spawnPoints; // Have 2 Transform (Left and Right)
    public Transform[] spaceCustomers; // Have two Spaces in front of the cashier
    public float minSpawnDelay = 1f; // Minimum delay before spawning the next customer
    public float maxSpawnDelay = 3f; // Maximum delay before spawning the next customer
    public int customersPerChapter = 6; // Number of customers to serve in each chapter

    private WaitForSeconds spawnDelay; // Delay between customer spawns
    private int customersServed; // Number of customers served in the current chapter
    private bool[] spaceCustomerOccupied; // Array to track space customer occupancy

    private void Start()
    {
        spawnDelay = new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        spaceCustomerOccupied = new bool[spaceCustomers.Length];
        StartCoroutine(SpawnCustomers());
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
                    break;
                }
            }

            if (isSpaceAvailable)
            {
                // Select a random customer prefab
                GameObject randomPrefab = customerPrefabs[Random.Range(0, customerPrefabs.Length)];

                // Spawn a customer
                GameObject customer = Instantiate(randomPrefab, GetRandomSpawnPoint(), Quaternion.identity);
                CustomerController customerController = customer.GetComponent<CustomerController>();

                // Set the customer's target position and assign the next customer's spawn delay
                customerController.targetPosition = GetTargetPosition(customer.transform);
                customerController.spawnDelay = Random.Range(2f, 4f);

                customersServed++;
            }

            yield return spawnDelay;
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        // Get a random spawn point from the available spawn points
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex].position;
    }

    private Vector3 GetTargetPosition(Transform customerTransform)
    {
        // Check if any space customer is empty
        List<int> emptySpaceCustomers = new List<int>();
        for (int i = 0; i < spaceCustomerOccupied.Length; i++)
        {
            if (!spaceCustomerOccupied[i]) // Space customer is not occupied
            {
                emptySpaceCustomers.Add(i);
            }
        }

        if (emptySpaceCustomers.Count > 0)
        {
            // Select a random empty space customer
            int randomIndex = Random.Range(0, emptySpaceCustomers.Count);
            int spaceCustomerIndex = emptySpaceCustomers[randomIndex];

            // Set the target position as the position in front of the empty space customer
            Vector3 targetPosition = spaceCustomers[spaceCustomerIndex].position;
            targetPosition.y = customerTransform.position.y; // Maintain the same y position

            // Mark the space customer as occupied
            spaceCustomerOccupied[spaceCustomerIndex] = true;

            return targetPosition;
        }
        else
        {
            // No empty space customers, return a random spawn point as the target position
            return GetRandomSpawnPoint();
        }
    }
}