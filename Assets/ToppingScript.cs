using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingScript : MonoBehaviour
{
    public string NameOfThisFlavour;
    public GameObject ThisTopping;
    private TaskVarious taskVarious;

    void Start()
    {
        taskVarious = GameObject.FindWithTag("IceCream").GetComponent<TaskVarious>();
    }

    public void OnMouseDown()
    {
        if (taskVarious == null || taskVarious.NumFlavourInThisIceCream == 0) return;

        if (!taskVarious.ToppingBool && taskVarious.NumFlavourInThisIceCream == 1)
        {
            taskVarious.Topping[0] = NameOfThisFlavour;
            taskVarious.ToppingBool = true;
            taskVarious.NumToppingInThisIceCream++;

            GameObject newTopping = Instantiate(ThisTopping, taskVarious.customerTransforms[1].position, Quaternion.identity);
            newTopping.transform.SetParent(taskVarious.GetPlateTransform(), true);
        }
    }
}