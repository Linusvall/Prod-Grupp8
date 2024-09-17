using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static GameEnums;
using static Unity.VisualScripting.Member;

public class FishingGame : MonoBehaviour
{

    private AudioSource fishAudioSource;
    [SerializeField] private AudioClip reelIn;
    [SerializeField] private AudioClip victory;

    [SerializeField] Fish currentFish;
    [SerializeField] private FishingPool pool;

    [SerializeField] PlayerController playerController;

    [SerializeField] int fishingPhase = 1;

    [SerializeField] List<string> caughtFish = new List<string>();

    private Directions fishCurrentDirection;
    private Directions playerCurrentDirection;

    public void SetFish (Fish fish) { currentFish = fish; }


    public float spinThreshold = 360f;  // Amount of degrees needed to complete a spin
    public int goal = 5;  // The value you want to increase
    private int currentSpins = 0;
    public int increaseAmount = 1;   // Amount to increase value each spin

    private float previousAngle = 0f;   // Angle of the joystick in the last frame
    private float accumulatedAngle = 0f;

    private bool fishingEnabled = false;


    // Start is called before the first frame update
    void Start()
    {
        currentFish = pool.GetRandomFish();
        fishAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
      
       playerCurrentDirection = playerController.GetCurrentDirection();

        Fishing();
        

    }

    private void Fishing()
    {
        if (fishingEnabled)
        {
            if (fishingPhase == 1)
            {
                Phase1();
            }

            if (fishingPhase == 2)
            {
                Phase2();
                fishAudioSource.panStereo = 0;
            }
        }
    }

    private void Phase1()
    {
        fishCurrentDirection = currentFish.CurrentDirection;
        if (currentFish.CurrentStamina > 0)
        {

            print(currentFish.CurrentDirection);

            if (fishCurrentDirection == Directions.Left)
            {
                fishAudioSource.panStereo = -1;
                if (!fishAudioSource.isPlaying)
                {
                    fishAudioSource.Play();
                }

                if (playerCurrentDirection == Directions.Right)
                {
                    currentFish.LowerStamina(1 * Time.deltaTime);
                }
            }

            if (fishCurrentDirection == Directions.Right)
            {
                fishAudioSource.panStereo = 1;
                if (!fishAudioSource.isPlaying)
                {

                    fishAudioSource.Play();
                }
                if (playerCurrentDirection == Directions.Left)
                {
                    currentFish.LowerStamina(1 * Time.deltaTime);
                }
            }

            if (fishCurrentDirection == Directions.Up)
            {
                fishAudioSource.panStereo = 0;
                if (!fishAudioSource.isPlaying)
                {
                    fishAudioSource.Play();
                }
                if (playerCurrentDirection == Directions.Down)
                {
                    currentFish.LowerStamina(1 * Time.deltaTime);
                }
            }
        }
        if(currentFish.CurrentStamina <= 0)
        {
            print("du vann jao");
            fishAudioSource.Stop();
            fishingPhase = 2;
            playerController.StopSound();
        }
    }

    private void Phase2()
    {
        // Get the joystick position
        float horizontal = Input.GetAxis("RightJoystickHorizontal");
        float vertical = Input.GetAxis("RightJoystickVertical");

        // Calculate the current angle of the joystick (in degrees)
        float currentAngle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;

        // Ensure the angle is in the range of [0, 360]
        if (currentAngle < 0)
        {
            currentAngle += 360f;
        }

        // Calculate the difference between the current angle and the previous angle
        float angleDifference = Mathf.DeltaAngle(previousAngle, currentAngle);

        // Accumulate the angle difference
        accumulatedAngle += angleDifference;

        // Check if a full spin has occurred
        if (Mathf.Abs(accumulatedAngle) >= spinThreshold)
        {
            // Full spin detected, increase the value
            currentSpins += increaseAmount;
            if (!fishAudioSource.isPlaying)
            {
                fishAudioSource.PlayOneShot(reelIn);
            }
            // Reset the accumulated angle
            accumulatedAngle = 0f;

            Debug.Log("Value increased: " + currentSpins);
        }

        // Update the previous angle for the next frame
        previousAngle = currentAngle;

        if(goal == currentSpins)
        {
            fishAudioSource.PlayOneShot(victory);
            fishingPhase = 3;
            fishingEnabled = false;
            playerController.SetFishing(false);
            print("yippieeee");

            AudioManager.instance.Play(currentFish.dialogID + "_Intro", playerController.gameObject);
            if (!checkFish())
            {
                AudioManager.instance.Play(currentFish.dialogID + "_Desc", playerController.gameObject);
            }

            resetGame();
        }
    }

    public void StartGame()
    {
        fishingEnabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().SetCurrentFishGame(this);
        }
    }

    //Check if fish type has been caught before, if not catalog it
    bool checkFish()
    {
        foreach (var ID in caughtFish)
        {
            if (ID == currentFish.dialogID)
            {
                return true;
            }
        }

        caughtFish.Add(currentFish.dialogID);
        return false;
    }

    void resetGame()
    {
        fishingPhase = 1;
        currentSpins = 0;
        Destroy(currentFish.gameObject);
        currentFish = pool.GetRandomFish();
    }
}
