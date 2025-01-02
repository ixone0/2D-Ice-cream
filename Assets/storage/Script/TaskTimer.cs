using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTimer : MonoBehaviour
{
    public string Name;
    public bool StartTimer = false;
    public float time = 10f; // Default timer duration
    public GameObject objectPrefab; // Reference to the prefab
    public Transform[] PlacePositions; // Array of available positions
    private bool[] positionOccupied; // Tracks if a position is occupied
    private bool taskRunning = false; // Tracks if a task is currently running
    private int placedDrinks = 0; // Count of drinks placed

    void Start()
    {
        // Initialize the position tracking array
        positionOccupied = new bool[PlacePositions.Length];
        ResetTask();
    }

    void Update()
    {
        if (StartTimer && taskRunning)
        {
            time -= Time.deltaTime;

            if (time <= 0f)
            {
                PlaceDrink();
                ResetTimer();
            }
        }
    }

    void OnMouseDown()
    {
        if (!taskRunning && placedDrinks < PlacePositions.Length)
        {
            StartTimer = true;
            taskRunning = true;
        }
        else if (placedDrinks >= PlacePositions.Length)
        {
            Debug.Log("All positions are occupied!");
        }
    }

    void PlaceDrink()
    {
        // Find the first available position
        for (int i = 0; i < PlacePositions.Length; i++)
        {
            if (!positionOccupied[i])
            {
                positionOccupied[i] = true; // Mark the position as occupied
                placedDrinks++; // Increment the count of placed drinks

                // Instantiate and place the drink at the position
                Instantiate(objectPrefab, PlacePositions[i].position, Quaternion.identity);
                Debug.Log($"Drink placed at position {i + 1}");
                return;
            }
        }

        Debug.LogWarning("No available positions to place the drink!");
    }

    void ResetTimer()
    {
        StartTimer = false;
        taskRunning = false;
        time = 10f; // Reset the timer
    }

    public void ResetTask()
    {
        StartTimer = false;
        taskRunning = false;
        time = 10f; // Reset the timer
        placedDrinks = 0;

        // Reset the occupied status for all positions
        for (int i = 0; i < positionOccupied.Length; i++)
        {
            positionOccupied[i] = false;
        }
    }

    // Optional: Free a position when a drink is picked up
    public void FreePosition(int index)
    {
        if (index >= 0 && index < positionOccupied.Length && positionOccupied[index])
        {
            positionOccupied[index] = false;
            placedDrinks--;
            Debug.Log($"Position {index + 1} is now free.");
        }
    }
}
