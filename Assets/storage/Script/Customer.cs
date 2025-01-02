using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [Header("Setting")]
    public float moveSpeed = 5f; 
    public bool reachedTarget = false; 
    public Vector3 targetPosition;
    public float spawnDelay;
    public CustomerSystem customersystem;
    public Setting setting;
    public int spacecustomerindex;

    [Header("Task")]
    public string[] ToppingTask; // Store customer's desired topping
    public int ToppingNumber; // Number of toppings required
    public bool UIAppear = false;
    private bool checkUI = false;
    public GameObject UITask;
    public bool taskSuccess = false;
    public bool StartTime;
    public float time;
    public float NumberTask;
    public int IceCreamNumber;
    public int[] IceCreamRandom;
    public bool IsHaveTopping;
    public int randomnum;
    public int ToppingRandom;
    public int IceCreamNumberFlavour = 0;
    public string[] IceCreamFlavour;
    public string[] IceCreamTask;
    public string[] IceCreamToppings;
    public bool icecreamServed = true;
    public bool drinkingServed = true;
    public bool cupcakeServed = true;
    public bool taskCheck = true;
    public AudioSource soundsuccess;

    [Header("Lose")]
    public bool StartTimeCustomer;
    [SerializeField] private float timer;
    public float TimeLimit = 60f;
    [Header("Win")]
    public GameObject objectoriginal;
    public GameObject objectscelebrate;

    [Header("Task Bar")]
    public Slider taskbar;
    public float speed = 3f;
    public float celebrationOscillationSpeed = 13f;
    public bool isCupcakeArrived = false;
    public bool isIcecreamArrived = false;
    public void OnCupcakeArrived()
    {
        isCupcakeArrived = true;
    }

    public void OnIcecreamArrived()
    {
        isIcecreamArrived = true;
    }

    private void Start()
    {
        celebrationOscillationSpeed = 18f;
        customersystem = GameObject.FindWithTag("System").GetComponent<CustomerSystem>();
        setting = GameObject.FindWithTag("System").GetComponent<Setting>();
        StartCoroutine(MoveToTarget()); 
        StartCoroutine(VerticalOscillation());
        SettingStart();
    }

    private void Update()
    {
        SystemTask();
    }

    void SettingStart()
    {
        randomnum = Random.Range(0,2);
        IceCreamNumberFlavour = 2;
        taskbar.gameObject.SetActive(false);
        checkUI = false;
        IceCreamFlavour = new string[] { "GreanTea", "Plum","lemon", "strawberry", "latte", "cappuccino" };
        IceCreamToppings = new string[] { "Boba", "Whippedcream", "nuts", "chocolate chips" }; // Example toppings
         // Set the flavor task (1 flavor per task)
         /*
        IceCreamNumber = 1; 
        IceCreamTask = new string[1];
        IceCreamTask[0] = IceCreamFlavour[Random.Range(0, 2)];
        icecreamServed = false;
        StartTimeCustomer = true;
        TimeLimit = setting.Timelimit;
        */
        ToppingTask = new string[1];
        NumberTask = Random.Range(1, 4);
        IceCreamNumber = 1;
        IceCreamRandom = new int[IceCreamNumber];
        IceCreamTask = new string[IceCreamNumber];
        StartTimeCustomer = true;
        TimeLimit = setting.Timelimit;

        if(randomnum == 0)
        {
            IsHaveTopping = true;
        }
        else
        {
            IsHaveTopping = false;
        }
        if (setting.stage == 1)
        {
            NumberTask = 1;
        }

        if (setting.stage == 2 || setting.stage == 3)
        {
            NumberTask = 3;
        }

        if (setting.stage > 3)
        {
            NumberTask = Random.Range(1, 4);
        }

        if (NumberTask == 1)
        {
            isCupcakeArrived = true;
        }
        if(StartTimeCustomer)
        {
            timer += Time.deltaTime;
        }
        
        for(int i = 0; i < IceCreamNumber; i++)
        {
            IceCreamRandom[i] = Random.Range(0, IceCreamNumberFlavour);
            IceCreamTask[i]= IceCreamFlavour[IceCreamRandom[i]];
        }

        if (IsHaveTopping)
        {
            ToppingNumber = Random.Range(0, 2);
            ToppingTask[0] = IceCreamToppings[ToppingNumber];
        }
        
        if(NumberTask == 1)
        {
            icecreamServed = false;
            drinkingServed = true;
            cupcakeServed = true;
        }
        if(NumberTask == 2)
        {
            icecreamServed = false;
            drinkingServed = false;
            cupcakeServed = true;
        }
        if(NumberTask == 3)
        {
            icecreamServed = false;
            drinkingServed = true;
            cupcakeServed = false;
        }   
    }

    void SystemTask()
    {
        if (UIAppear && !checkUI)
        {
            UITask.SetActive(true);
            checkUI = true;
            StartTime = true;
            taskbar.gameObject.SetActive(true);
        }

        if (StartTime)
        {
            timer += Time.deltaTime;
            taskbar.maxValue = TimeLimit;
            taskbar.value = timer;
        }

        if (!UIAppear)
        {
            UITask.SetActive(false);
            taskbar.gameObject.SetActive(false);
        }

        if (reachedTarget)
        {
            UIAppear = true;
            taskbar.gameObject.SetActive(true);
        }

        if (taskSuccess && isCupcakeArrived && isIcecreamArrived)
        {
            StartCoroutine(CelebrateThenWalkAway());
            reachedTarget = false;
            customersystem.spaceCustomerOccupied[spacecustomerindex] = false;
            UIAppear = false;
            setting.score += 1;
            taskSuccess = false;
            taskbar.gameObject.SetActive(false);
        }

        if(icecreamServed && drinkingServed && cupcakeServed && taskCheck)
        {
            taskSuccess = true;
            taskCheck = false;
        }

        if(timer >= TimeLimit && StartTime)
        {
            StartTime = false;
            setting.GameOver = true;
        }
    }

    private Coroutine verticalOscillationCoroutine;

    private IEnumerator CelebrateThenWalkAway()
    {
        soundsuccess.Play();
        float celebrationDuration = 2f;
        float originalOscillationSpeed = 3f;

        if (verticalOscillationCoroutine != null)
        {
            StopCoroutine(verticalOscillationCoroutine);
        }

        speed = celebrationOscillationSpeed;
        verticalOscillationCoroutine = StartCoroutine(VerticalOscillation());

        float elapsedTime = 0f;
        objectoriginal.SetActive(false);
        objectscelebrate.SetActive(true);
        while (elapsedTime < celebrationDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        objectscelebrate.SetActive(false);
        objectoriginal.SetActive(true);
        speed = originalOscillationSpeed;
        verticalOscillationCoroutine = StartCoroutine(VerticalOscillation());

        StartCoroutine(WalkAway());
    }

    private IEnumerator VerticalOscillation()
    {
        while (true)
        {
            float amplitude = 0.16f;
            float yOffset = Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = new Vector3(transform.position.x, 0.8f + yOffset, transform.position.z);
            yield return null;
        }
    }

    private IEnumerator MoveToTarget()
    {
        yield return new WaitForSeconds(spawnDelay);
        bool horizontalReached = false;

        while (!horizontalReached)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(targetPosition.x, 0.8f, transform.position.z),
                moveSpeed * Time.deltaTime
            );

            if (transform.position.x == targetPosition.x)
            {
                horizontalReached = true;
                reachedTarget = true;
            }

            yield return null;
        }
    }

    private IEnumerator WalkAway()
    {
        Vector3 walkAwayTarget = transform.position;
        walkAwayTarget.x = Random.Range(-10f, 10f);
        targetPosition = walkAwayTarget;
        yield return new WaitForSeconds(0.5f);
        reachedTarget = false;

        while (!reachedTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                reachedTarget = true;
                Destroy(gameObject);
            }

            yield return null;
        }
    }
}
