using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums; 

public class Fish : MonoBehaviour
{

    public float Weight;
    public float MaxStamina; 
    public float CurrentStamina { get; set; }

    public float RechargeRate; 
    public float Agression;
    private readonly static System.Random rand = new();
    public float StaminaEnumerator;
    public float AgressionEnumerator; 
    public Direction CurrentDirection { get; private set; }
    

    // Start is called before the first frame update
    void Start()
    {
        CurrentStamina = MaxStamina; 
    }

    // Update is called once per frame
    void Update()
    {
        if(IsStaminaDelpeted())
        {
            StaminaEnumerator += Time.deltaTime;
            if (StaminaEnumerator >= RechargeRate)
            {
                StaminaEnumerator = 0;
                CurrentStamina = MaxStamina;
            }
            return; 
        }

        AgressionEnumerator += Time.deltaTime; 

        if(AgressionEnumerator >= Agression)
        {
            AgressionEnumerator = 0;
            CurrentDirection = ListOfDirections[rand.Next(ListOfDirections.Length - 1)];
            return; 
        }

    }

    public bool IsStaminaDelpeted()
    {
        return CurrentStamina <= 0; 
    }

}
