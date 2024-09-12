using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class ShopKeeper : MonoBehaviour
{


    public CapsuleCollider CapCollider;
    public string SoundEffectToPlayOnEnter;
    public string SoundEffectToPlayOnExit;
    public string SoundEffectToPlayRandomly;
    public int ChanceToPlaySoundEffect;
    public float FrequencyToPlayEffect;
    private float SoundEffectEnumerator = 0;
    private bool HasExitedTheCollider = true; 

    private static readonly System.Random rand = new(); 

    // Start is called before the first frame update
    void Start()
    {
        if(CapCollider != null)
        {
            return; 
        }
        if(!TryGetComponent<CapsuleCollider>(out CapCollider))
        {
            Debug.Log("No collider on object");
            enabled = false;
            return;
        }


    }

    // Update is called once per frame
    void Update()
    {
        SoundEffectEnumerator += Time.deltaTime; 

        if (SoundEffectEnumerator > FrequencyToPlayEffect)
        {
            // should we add randomness to it?
            if(AudioManager.instance != null)
            {
                //AudioManager.instance.Play(SoundEffectToPlayRandomly);
            }

            SoundEffectEnumerator = 0;
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Someone entered my area~"); 
        if (!other.gameObject.CompareTag("Player"))
        {
            return; 
        }

        if(AudioManager.instance == null)
        {
            Debug.Log("Error: Can not play sound effect. No audio manager in scene");
            return; 
        }

        if (HasExitedTheCollider)
        {
            AudioManager.instance.Play(SoundEffectToPlayOnEnter, gameObject);
        }
        HasExitedTheCollider = false;


    }


    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Someone exited  my area~");
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (AudioManager.instance == null)
        {
            Debug.Log("Error: Can not play sound effect. No audio manager in scene");
            return;
        }
        if(!HasExitedTheCollider)
        {
            AudioManager.instance.Play(SoundEffectToPlayOnExit, gameObject);
        }
        HasExitedTheCollider = true; 
    }
}
