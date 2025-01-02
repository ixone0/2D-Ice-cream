using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlavourScript : MonoBehaviour
{
    public string NameOfThisFlavour;
    public GameObject ThisFlavour;
    private TaskVarious taskVarious;

    void Start()
    {
        taskVarious = GameObject.FindWithTag("IceCream").GetComponent<TaskVarious>();
    }

    void OnMouseDown()
    {
        if (taskVarious == null) return;

        for (int i = 0; i < taskVarious.IceCreamBool.Length; i++)
        {
            if (!taskVarious.IceCreamBool[i])
            {
                taskVarious.Flavour[i] = NameOfThisFlavour;
                taskVarious.IceCreamBool[i] = true;
                taskVarious.NumFlavourInThisIceCream++;

                GameObject newFlavour = Instantiate(ThisFlavour, taskVarious.customerTransforms[0].position, Quaternion.identity);
                newFlavour.transform.SetParent(taskVarious.GetPlateTransform(), true);
                break;
            }
        }
    }
}
