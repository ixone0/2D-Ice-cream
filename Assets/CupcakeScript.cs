using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupcakeScript : MonoBehaviour
{
    public TaskTimer tasktimer;
    public bool taskcheck = false;
    public float moveSpeed = 2f; // Speed at which the drink will move
    public float curveOffset = 2f; // Offset distance for the curve control point

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 controlPoint; // Control point for the curve
    private bool moveToTarget = false; // Flag to start moving the drink
    private float journeyLength;
    private float startTime;
    private Customer targetCustomer; // Reference to the customer being served
    public AudioSource soundtaskcomplete;
    void Start()
    {
        moveSpeed = 25f; 
        curveOffset = 1.7f;
        GameObject drinkMachine = FindObjectName("Microwave");
        if (drinkMachine != null)
        {
            tasktimer = drinkMachine.GetComponent<TaskTimer>();
        }
    }

    GameObject FindObjectName(string objectName)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Microwave");
        foreach (GameObject obj in objects)
        {
            if (obj.name == objectName)
            {
                return obj;
            }
        }
        return null;
    }

    void OnMouseDown()
    {
        FindTaskPerson();
    }

    void FindTaskPerson()
    {
        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
        for (int i = 0; i < customers.Length; i++)
        {
            Customer customer = customers[i].GetComponent<Customer>();
            if (!customer.cupcakeServed)
            {
                targetCustomer = customer; // Set the target customer
                tasktimer.ResetTask();
                customer.cupcakeServed = true;

                // Set the target position with an offset to this customer's position
                startPosition = transform.position;
                targetPosition = customer.transform.position + new Vector3(1.3f, 1f, 0);
                
                // Calculate the control point for the curve
                Vector3 midPoint = (startPosition + targetPosition) / 2;
                Vector3 direction = (targetPosition - startPosition).normalized;
                Vector3 perpendicularDirection = Vector3.Cross(direction, Vector3.up).normalized;
                controlPoint = midPoint + perpendicularDirection * curveOffset;

                journeyLength = Vector3.Distance(startPosition, targetPosition);
                startTime = Time.time;

                moveToTarget = true;
                break;
            }
            else
            {
                Debug.Log("This customer has already been served a drink.");
            }
        }
    }

    void Update()
    {
        void OnDestroy()
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }

        if (moveToTarget)
        {
            // Calculate the percentage of journey completed
            float distCovered = (Time.time - startTime) * moveSpeed;
            float t = distCovered / journeyLength;

            // Calculate position along the Bezier curve
            Vector3 currentPosition = Mathf.Pow(1 - t, 2) * startPosition + 
                                      2 * (1 - t) * t * controlPoint + 
                                      Mathf.Pow(t, 2) * targetPosition;

            transform.position = currentPosition;

            // Stop moving when reaching the target position
            if (t >= 1f)
            {
                moveToTarget = false;

                if (targetCustomer != null)
                {
                    targetCustomer.OnCupcakeArrived(); // Notify customer
                }   
                soundtaskcomplete.Play();
                Destroy(gameObject);
            }
        }
    }
}
