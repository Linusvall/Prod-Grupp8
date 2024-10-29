using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour
{


    [SerializeField] AudioSource m_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
     if(m_AudioSource == null)
        {
            m_AudioSource =GetComponent<AudioSource>();
        }
        m_AudioSource.volume = SettingsLoader.GetInstance().GetSettings().ClockVolume;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
