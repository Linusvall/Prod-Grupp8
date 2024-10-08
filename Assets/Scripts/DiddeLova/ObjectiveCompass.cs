using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectiveCompass : MonoBehaviour
{
    private bool audioCompassActivated = false;
    private AudioSource source;
    
    void Start()
    {
        source = GetComponent<AudioSource>();

    }
    
    void Update()
    {

        if (Gamepad.current.rightTrigger.value >= 0.1 && !source.isPlaying)
        {
            Debug.Log(source.isPlaying);
    
            source.Play();
         
        }

        else if (Gamepad.current.rightTrigger.value < 0.1) {

            source.Stop ();
        }

    }

    public void ChangeAudioCompassPosition(GameObject newObject, AudioClip newClip)
    {
        source.clip = newClip;
        gameObject.transform.position = newObject.transform.position;
    }


}
