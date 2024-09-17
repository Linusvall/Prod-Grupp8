using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
  public List<AudioClip> tutorialClips = new List<AudioClip>();
    private AudioSource tutorialSource;
    private int currentTut;
    private List<int> passedTuts = new List<int>();

    private float input;

    // Start is called before the first frame update
    void Start()
    {
        tutorialSource = GetComponent<AudioSource>();
       // updateTut(0);
      
    }

    private void Update()
    {

        if (Input.GetButtonDown("Compass"))
        {
            if(currentTut == 2)
            {
                updateTut(3);
            }
        }

        //  if(Input.getaxis)
        input = Input.GetAxis("Compass");
        if (Input.GetButtonDown("Compass"))
        {
            Debug.Log("JEOL WOHOOO");
        }


        if ((Input.GetButtonDown("Repeat")))
        {
            PlayClip();
        }


        /*
        if ((Input.GetKeyDown(KeyCode.Y)))
        {
            PlayClip();
        }
        */
        

    }


    public void updateTut(int tut)
    {
        if (!passedTuts.Contains(tut))
        {
            passedTuts.Add(tut);
            currentTut = tut;
            PlayClip();
        }
        if(tut == 0)
        {
            
        }
        
    }

    private void PlayClip()
    {
        tutorialSource.clip = tutorialClips[currentTut];
        tutorialSource.Play();

    }
   
    private void OnClipFinished(int tut)
    {
        if(tut == 0)
        {
            updateTut(1);
        }
    }

}
