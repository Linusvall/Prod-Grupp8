using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHoleManager : MonoBehaviour
{
    [SerializeField] private GameObject fishingHole;
    [SerializeField] private GameObject fishingGame;
    [SerializeField] private GameObject[] fishingHoleLocations;
    private int locationIndex;


    // Start is called before the first frame update
    private void OnEnable()
    {
        OpenNewFishingHole();
    }

    public void OpenNewFishingHole()
    {
        locationIndex = Random.Range(1, fishingHoleLocations.Length);
        GameObject currentLocation = fishingHoleLocations[locationIndex];
        fishingGame.transform.position = currentLocation.transform.position;
        fishingHole.SetActive(true);
        fishingHoleLocations[locationIndex] = fishingHoleLocations[0];
        fishingHoleLocations[0] = currentLocation;
        Debug.Log("New fishing hole opened");
    }

}
