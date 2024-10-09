using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidPortalOpen : MonoBehaviour
{

    private AudioSource source;
    [SerializeField] private AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        source= GetComponent<AudioSource>();
        source.PlayOneShot(clip);
    }
}
