using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums; 

public class Fish : MonoBehaviour
{

    public int Weight;
    public float MaxStamina; 
    public float CurrentStamina { get; set; }
    public float RechargeRate; 
    public float Agression;
    private readonly static System.Random rand = new();
    public float StaminaEnumerator;
    public float AgressionEnumerator;

    public bool isFish = true;

    //Grim
    public string dialogID;

    public string SoundBite; 
   
    public Directions CurrentDirection { get; private set; }
    

    // Start is called before the first frame update
    void Start()
    {
        CurrentStamina = MaxStamina;
        CurrentDirection = ListOfDirections[rand.Next(ListOfDirections.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Stamina: " + CurrentStamina);
       
        /*
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
        */
        AgressionEnumerator += Time.deltaTime; 

        if(AgressionEnumerator >= Agression)
        {
            AgressionEnumerator = 0;
            CurrentDirection = ListOfDirections[rand.Next(ListOfDirections.Length)];
            return; 
        }

    }

    public bool IsStaminaDelpeted()
    {
        return CurrentStamina <= 0; 
    }

    public void LowerStamina(float amount)
    {
        CurrentStamina -= amount;
    }

    public void IncreaseStamina()
    {
        if (CurrentStamina < MaxStamina)
        {
            CurrentStamina += RechargeRate * Time.deltaTime;
        }
    }
}
