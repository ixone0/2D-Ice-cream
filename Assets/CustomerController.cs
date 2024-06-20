using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public float moveSpeed = 5f; // The movement speed of the customer

    private bool reachedTarget = false; // Flag to check if the customer has reached the target position
    public Vector3 targetPosition;
    public float spawnDelay;

    private void Start()
    {
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        yield return new WaitForSeconds(spawnDelay);

        while (!reachedTarget)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the customer has reached the target position
            if (transform.position == targetPosition)
            {
                reachedTarget = true;
                // Perform any actions or behaviors when the customer reaches the target position
            }

            yield return null;
        }
    }
}