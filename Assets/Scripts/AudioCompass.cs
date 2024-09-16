using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioCompass: MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float offset;
    private Transform playerpos;
    [SerializeField] string audioClipName;
    AudioSource audioSrc;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();  
    }
    void Update()
    {
        playerpos = player.transform;
        this.transform.position = new Vector3(playerpos.position.x, playerpos.position.y,playerpos.position.z + offset);

        if (Input.GetKey(KeyCode.Joystick1Button10))
        {
            Debug.Log("AudioCompass tried to play");
            if (!audioSrc.isPlaying)
            {
                AudioManager.instance.Play(audioClipName, this.gameObject);
            }
       

        }
    }

    


}
