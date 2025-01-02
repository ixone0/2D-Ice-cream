using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskVarious : MonoBehaviour
{
    private bool taskcheck = false;
    public bool[] IceCreamBool;
    public bool[] IceCreamCheck;
    public bool ToppingBool = false;
    public string[] Flavour;
    public string[] Topping; // Array to store 2 toppings
    public Transform[] customerTransforms;
    private bool CheckIfTaskCorrect = false;
    public Customer customer;
    public string[] IceCreamFlavour;
    public string[] IceCreamToppings; // Array to store possible toppings
    public int NumFlavourInThisIceCream;
    public int NumToppingInThisIceCream;
    private int click = 0;

    // Movement variables
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 controlPoint;
    public float moveSpeed = 2f;
    public float curveOffset = 1.5f;
    private bool moveToTarget = false;
    private float journeyLength;
    private float startTime;
    private Customer nowcustomer;
    public GameObject platePrefab;  // Reference to the plate prefab
    private GameObject currentPlate; // Holds the currently active plate

    void Start()
    {
        moveSpeed = 25f;
        SystemTaskVarious();
    }

    void Update()
    {   
        if (click == 2)
        {
            SystemTaskVarious();
        }

        if (moveToTarget && currentPlate != null)
        {
            MovePlateToCustomer();
        }
    }

    void OnMouseDown()
    {   
        click += 1;
        FindTaskPerson();
    }

    void SystemTaskVarious()
    {
        click = 0;
        NumFlavourInThisIceCream = 0;
        NumToppingInThisIceCream = 0;
        IceCreamBool = new bool[1];
        Flavour = new string[1];
        Topping = new string[1];  // Only allow one topping for now
        ToppingBool = false; // Only one topping allowed
        IceCreamCheck = new bool[1];
        IceCreamFlavour = new string[] { "GreanTea", "Plum", "lemon", "strawberry", "latte", "cappuccino" };
        IceCreamToppings = new string[] { "Boba", "Whippedcream", "nuts", "chocolate chips" }; // Example toppings

        // Destroy old plate and ice creams
        if (currentPlate != null)
        {
            Destroy(currentPlate);
        }

        // Instantiate a new plate
        currentPlate = Instantiate(platePrefab, transform.position, Quaternion.identity);
    }


    public Transform GetPlateTransform()
    {
        if (currentPlate == null)
        {
            Debug.LogError("Plate transform not found!");
            return transform; // Default to TaskVarious position
        }
        return currentPlate.transform;
    }

    void FindTaskPerson()
    {  
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Customer");
        foreach (GameObject obj in objects)
        {
            Customer customer = obj.GetComponent<Customer>();
            if (!customer.icecreamServed 
                && NumFlavourInThisIceCream == customer.IceCreamNumber ) // Check number of toppings
            {
                // Check if the flavors match
                bool correctFlavours = false;
                if (Flavour[0] == customer.IceCreamTask[0])
                {
                    correctFlavours = true;
                }
                

                // Check if the toppings match
                bool correctToppings = false;
                if(customer.IsHaveTopping)
                {
                    if (Topping[0] == customer.ToppingTask[0])
                    {
                        correctToppings = true;
                    }
                }
                if(!customer.IsHaveTopping)
                {
                    correctToppings = true;
                }
                
                // If both flavors and toppings are correct
                if (correctFlavours && correctToppings)
                {
                    click = 0;

                    // Set up movement to the customer
                    startPosition = currentPlate.transform.position;
                    targetPosition = customer.transform.position + new Vector3(1.3f, 1f, 0);
                    Vector3 midPoint = (startPosition + targetPosition) / 2;
                    Vector3 direction = (targetPosition - startPosition).normalized;
                    Vector3 perpendicularDirection = Vector3.Cross(direction, Vector3.up).normalized;
                    controlPoint = midPoint + perpendicularDirection * curveOffset;

                    journeyLength = Vector3.Distance(startPosition, targetPosition);
                    startTime = Time.time;

                    moveToTarget = true;
                    customer.icecreamServed = true;
                    nowcustomer = customer;
                    break;
                }
            }
        }
    }


    void MovePlateToCustomer()
    {
        float distCovered = (Time.time - startTime) * moveSpeed;
        float t = distCovered / journeyLength;

        Vector3 currentPosition = Mathf.Pow(1 - t, 2) * startPosition + 
                                  2 * (1 - t) * t * controlPoint + 
                                  Mathf.Pow(t, 2) * targetPosition;

        currentPlate.transform.position = currentPosition;

        if (t >= 1f)
        {
            moveToTarget = false;
            nowcustomer.OnIcecreamArrived();

            // Destroy the flavors on the plate
            GameObject[] FlavourOb = GameObject.FindGameObjectsWithTag("FlavourOb");
            foreach (GameObject flavour in FlavourOb)
            {
                Destroy(flavour);
            }

            // Reset for the next task
            SystemTaskVarious();

            // Optionally, add any additional logic when the plate reaches the customer
        }
    }
}
