using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSound : MonoBehaviour
{

    [SerializeField] AudioClip calloutSound;
    private AudioSource source;
    private bool triggerPressed = false;

    //Ska flyttas med delen längst ned
    private bool hasPressed;

    // Start is called before the first frame update
    void Start()
    {
        hasPressed = false;
        source = GetComponent<AudioSource>();
        source.clip = calloutSound;
    }

    public void PlayObjectiveSound()
    {
        source.Play();
        Debug.Log("Objective is playing");
    }

    public void StopPlayingObjectiveSound()
    { 
        source.Pause(); 
        
    }


    //Logik som sen ska hamna i annat script men nu testar vi
    void Update()
    {
        
    }
}
