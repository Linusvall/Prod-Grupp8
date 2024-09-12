using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
  public List<AudioClip> tutorialClips = new List<AudioClip>();
    private AudioSource tutorialSource;
    private int currentTut;
    private List<int> passedTuts = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        tutorialSource = GetComponent<AudioSource>();
    }


    public void updateTut(int tut)
    {
        passedTuts.Add(currentTut);
        currentTut = tut;
        PlayClip();
        
    }

    private void PlayClip()
    {
        tutorialSource.clip = tutorialClips[currentTut];
        tutorialSource.Play();

    }
   
    

}
