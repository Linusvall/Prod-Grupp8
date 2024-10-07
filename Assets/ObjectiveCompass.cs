using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCompass : MonoBehaviour
{
    private GameObject fishMonger;
    private GameObject swampPortal;


    public int currentObjective;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            //objectiveList[currentObjective].PlayObjectiveSound();

            //Activate the "here i am" sound on the objective.

        }
    }
}
