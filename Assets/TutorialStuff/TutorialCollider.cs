using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollider : MonoBehaviour
{
    public int tutNumber;
    private GameObject tutorialManager;
    // Start is called before the first frame update
    void Start()
    {
        tutorialManager = GameObject.Find("TutorialManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            tutorialManager.GetComponent<TutorialManager>().updateTut(tutNumber);
        }
    }
}
