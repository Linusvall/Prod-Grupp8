using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHoleManager : MonoBehaviour
{

    [SerializeField] private GameObject[] fishingHoles;
    private int fishingHoleIndex;


    // Start is called before the first frame update
    private void OnEnable()
    {
        OpenNewFishingHole();
    }

    private void OnDisable()
    {
        CloseAllFishingHoles();
    }

    public void OpenNewFishingHole()
    {
        fishingHoleIndex = Random.Range(1, fishingHoles.Length);
        GameObject fishingHoleToOpen = fishingHoles[fishingHoleIndex];
        fishingHoleToOpen.SetActive(true);
        fishingHoles[fishingHoleIndex] = fishingHoles[0];
        fishingHoles[0] = fishingHoleToOpen;
        Debug.Log("New fishing hole opened");
    }

    private void CloseAllFishingHoles()
    {
        foreach(GameObject fishingHole in fishingHoles)
        {
            fishingHole.SetActive(false);
            Debug.Log("Closed all fishing holes");
        }
    }


}
