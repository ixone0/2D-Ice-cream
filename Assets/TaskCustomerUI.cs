using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCustomerUI : MonoBehaviour
{
    public Customer customer;
    public Transform[] transform;
    public string[] NameIceCream;
    public GameObject[] ICECREAMOB;
    public GameObject IcecreamOb;
    public GameObject DRINKOB;
    public GameObject CUPCAKEOB;
    public GameObject CorrectOB;
    public Transform parentObject;
    public bool check;
    public bool check2;
    public bool[] IsEmpty;
    public bool checkset1 = false;
    public bool checkset2 = false;
    public bool checkset3 = false;
    void Start()
    {
        check = false;
        IsEmpty = new bool[2];
        IsEmpty[0] = false;
        IsEmpty[1] = false;
    }

    // Update is called once per frame
    void Update()
    {
        SystemTask();
    }

    void SystemTask()
    {
        Setting();
        if (customer.NumberTask == 1 && !check)
        {
            Task1();
        }

        if (customer.NumberTask > 1 && !check2)
        {
            Task2();
        }

        if (IsEmpty[0] && IsEmpty[1])
        {
            check2 = true;
        }
    }

    void Task1()
    {
        if (customer.IceCreamRandom.Length == 1)
        {
            Instantiate(IcecreamOb, transform[0].position, Quaternion.identity, parentObject);
        }
        if (customer.IceCreamRandom.Length == 2)
        {
            Instantiate(ICECREAMOB[customer.IceCreamRandom[0]], transform[0].position, Quaternion.identity, parentObject);
            Instantiate(ICECREAMOB[customer.IceCreamRandom[1]], new Vector3(transform[0].position.x - 0.05f, transform[0].position.y + 0.15f, transform[0].position.z), Quaternion.identity, parentObject);
        }
        if (customer.IceCreamRandom.Length == 3)
        {          
            Instantiate(ICECREAMOB[customer.IceCreamRandom[0]], transform[0].position, Quaternion.identity, parentObject);
            Instantiate(ICECREAMOB[customer.IceCreamRandom[1]], new Vector3(transform[0].position.x - 0.05f, transform[0].position.y + 0.15f, transform[0].position.z), Quaternion.identity, parentObject);
            Instantiate(ICECREAMOB[customer.IceCreamRandom[2]], new Vector3(transform[0].position.x, transform[0].position.y + 0.25f, transform[0].position.z), Quaternion.identity, parentObject);
        }
        check = true;
    }

    void Task2()
    {
        if (!customer.icecreamServed)
        {
            IsEmpty[0] = true;
            if (customer.IceCreamRandom.Length == 1)
            {
                Instantiate(IcecreamOb, transform[1].position, Quaternion.identity, parentObject);
            }
            if (customer.IceCreamRandom.Length == 2)
            {
                Instantiate(ICECREAMOB[customer.IceCreamRandom[0]], transform[1].position, Quaternion.identity, parentObject);
                Instantiate(ICECREAMOB[customer.IceCreamRandom[1]], new Vector3(transform[1].position.x - 0.05f, transform[1].position.y + 0.15f, transform[1].position.z), Quaternion.identity, parentObject);    
            }
            if (customer.IceCreamRandom.Length == 3)
            {          
                Instantiate(ICECREAMOB[customer.IceCreamRandom[0]], transform[1].position, Quaternion.identity, parentObject);
                Instantiate(ICECREAMOB[customer.IceCreamRandom[1]], new Vector3(transform[1].position.x - 0.05f, transform[1].position.y + 0.15f, transform[1].position.z), Quaternion.identity, parentObject);
                Instantiate(ICECREAMOB[customer.IceCreamRandom[2]], new Vector3(transform[1].position.x, transform[1].position.y + 0.25f, transform[1].position.z), Quaternion.identity, parentObject);
            }
        }

        if (!IsEmpty[0])
        {
            if (!customer.drinkingServed)
            {
                Instantiate(DRINKOB, transform[1].position, Quaternion.identity, parentObject);
                IsEmpty[0] = true;
            }
            else if (!customer.cupcakeServed && customer.drinkingServed)
            {
                Instantiate(CUPCAKEOB, transform[1].position, Quaternion.identity, parentObject);
                IsEmpty[0] = true;
            }
        }

        if (!IsEmpty[1])
        {
            if (!customer.drinkingServed)
            {
                Instantiate(DRINKOB, transform[2].position, Quaternion.identity, parentObject);
                IsEmpty[1] = true;
            }
            else if (!customer.cupcakeServed)
            {
                Instantiate(CUPCAKEOB, transform[2].position, Quaternion.identity, parentObject);
                IsEmpty[1] = true;
            }
        }
    }
    
    void Setting()
    {
        if (customer.NumberTask == 1)
        {
            if (customer.icecreamServed && !checkset1)
            {
                Instantiate(CorrectOB, transform[0].position, Quaternion.identity, parentObject);
                checkset1 = true;
            }
        }
        if (customer.NumberTask > 1)
        {
            if (customer.icecreamServed && !checkset2)
            {
                Instantiate(CorrectOB, transform[1].position, Quaternion.identity, parentObject);
                checkset2 = true;
            }
            if (customer.cupcakeServed && customer.drinkingServed && !checkset3)
            {
                Instantiate(CorrectOB, transform[2].position, Quaternion.identity, parentObject);
                checkset3 = true;
            }
        }

        if (!customer.IsHaveTopping)
        {
            if(customer.IceCreamRandom[0] == 0)
            {
                IcecreamOb = ICECREAMOB[0];
            }
            if(customer.IceCreamRandom[0] == 1)
            {
                IcecreamOb = ICECREAMOB[1];
            }
        }
        if (customer.IsHaveTopping)
        {
            if(customer.IceCreamRandom[0] == 0 && customer.ToppingNumber == 0)
            {
                IcecreamOb = ICECREAMOB[2];
            }
            if(customer.IceCreamRandom[0] == 0 && customer.ToppingNumber == 1)
            {
                IcecreamOb = ICECREAMOB[3];
            }
            if(customer.IceCreamRandom[0] == 1 && customer.ToppingNumber == 0)
            {
                IcecreamOb = ICECREAMOB[4];
            }
            if(customer.IceCreamRandom[0] == 1 && customer.ToppingNumber == 1)
            {
                IcecreamOb = ICECREAMOB[5];
            }
        }
    }
}
