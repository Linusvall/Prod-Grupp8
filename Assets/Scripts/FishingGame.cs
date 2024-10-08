using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnums;
using UnityEngine.InputSystem;

public class FishingGame : MonoBehaviour
{

    private AudioSource fishAudioSource;
    [SerializeField] private AudioClip reelIn;
    [SerializeField] private AudioClip victory;

    [SerializeField] Fish currentFish;
    [SerializeField] private FishingPool pool;

    [SerializeField] PlayerController playerController;

    [SerializeField] int fishingPhase = 0;

    [SerializeField] List<string> caughtFish = new List<string>();

    float fishingTimer = 1.5f, vibratetimer;
    float bitingTimer = 0.8f;
    bool isBiting = false;
    public GameObject fishSound, narrator;

    private Directions fishCurrentDirection;
    private Directions playerCurrentDirection;

    public void SetFish(Fish fish) { currentFish = fish; }


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
        AudioManager.instance.Play("Splash", pool.gameObject);
        currentFish = pool.GetRandomFish();
        goal = currentFish.Weight;
        fishAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playerCurrentDirection = playerController.GetCurrentDirection();

        Fishing();

        if (isBiting)
        {
            bitingTimer -= Time.deltaTime;

            if (Input.GetButtonDown("StartFishing"))
            {
                fishingPhase = 1;
            }

            if (bitingTimer <= 0)
            {
                isBiting = false;
                bitingTimer = 0.8f;
            }
        }
    }

    private void Fishing()
    {
        if (fishingEnabled)
        {
            switch (fishingPhase)
            {
                case 0:
                    {
                        phase0();
                        break;
                    }

                case 1:
                    {
                        Phase1();
                        break;
                    }

                case 2:
                    {
                        Phase2();
                        break;
                    }

                default:
                    {
                        Phase3();
                        break;
                    }
            }

            /*if (fishingPhase == 1)
            {
                Phase1();
            }

            if (fishingPhase == 2)
            {
                Phase2();
                //fishAudioSource.panStereo = 0;
            }*/
        }
    }

    void phase0()
    {
        fishingTimer -= Time.deltaTime;

        if (fishingTimer <= 0)
        {
            if (Random.Range(0, currentFish.Agression) < 2)
            {
                AudioManager.instance.Play("Bite", pool.gameObject);
                isBiting = true;
                Vibrate(0.2f, 0.2f);
                vibratetimer = 0.8f;
            }
            else
            {
                AudioManager.instance.Play("Nibble", pool.gameObject);
                if (Input.GetButtonDown("StartFishing"))
                {
                    fishingTimer += 7;
                }
                Vibrate(0.1f, 0.1f);
                vibratetimer = 0.4f;
            }

            fishingTimer = Random.Range(3, 6);
        }



        if (vibratetimer <= 0)
        {
            Vibrate(0f, 0f);
        }
        else
        {
            vibratetimer -= Time.deltaTime;
        }
    }

    private void Phase1()
    {
        fishCurrentDirection = currentFish.CurrentDirection;
        if (currentFish.CurrentStamina > 0)
        {
            if (fishSound.GetComponent<AudioSource>() == null || !fishSound.GetComponent<AudioSource>().isPlaying)
            {
                AudioManager.instance.Play("Swimming", fishSound);
            }

            print(currentFish.CurrentDirection);

            if (fishCurrentDirection == Directions.Left)
            {
                fishSound.transform.position = playerController.transform.position + (playerController.transform.right * -2) + (playerController.transform.forward * 2);

                //fishAudioSource.panStereo = -1;
                /*if (!fishAudioSource.isPlaying)
                {
                    fishAudioSource.Play();
                }*/

                if (playerCurrentDirection == Directions.Right)
                {
                    currentFish.LowerStamina(1 * Time.deltaTime);
                    Vibrate(1f, 0);
                }
                else
                {
                    Vibrate(0, 0);
                }
            }

            if (fishCurrentDirection == Directions.Right)
            {
                fishSound.transform.position = playerController.transform.position + (playerController.transform.forward * 2) + (playerController.transform.right * 2);

                //fishAudioSource.panStereo = 1;
                /*if (!fishAudioSource.isPlaying)
                {

                    fishAudioSource.Play();
                }*/
                if (playerCurrentDirection == Directions.Left)
                {
                    currentFish.LowerStamina(1 * Time.deltaTime);
                    Vibrate(0, 1f);
                }
                else
                {
                    Vibrate(0, 0f);
                }
            }

            if (fishCurrentDirection == Directions.Up)
            {
                fishSound.transform.position = playerController.transform.position + playerController.transform.forward * 4;

                //fishAudioSource.panStereo = 0;
                /*if (!fishAudioSource.isPlaying)
                {
                    fishAudioSource.Play();
                }*/
                if (playerCurrentDirection == Directions.Down)
                {
                    currentFish.LowerStamina(1 * Time.deltaTime);
                    Vibrate(0.5f, 0.5f);

                }
                else
                {
                    Vibrate(0, 0);
                }
            }
        }
        if (currentFish.CurrentStamina <= 0)
        {
            fishSound.GetComponent<AudioSource>().Stop();
            //fishAudioSource.Stop();
            fishingPhase = 2;
            Vibrate(0, 0);
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

        Vibrate(currentSpins, currentSpins);

        // Update the previous angle for the next frame
        previousAngle = currentAngle;

        if (goal == currentSpins)
        {
            fishAudioSource.PlayOneShot(victory);

            if (currentFish.isFish)
            {
                AudioManager.instance.Play(currentFish.dialogID + "_Intro", narrator);
                if (!checkFish())
                {
                    AudioManager.instance.Play(currentFish.dialogID + "_Desc", narrator);
                }
            }
            else
            {
                AudioManager.instance.Play(currentFish.dialogID, narrator);
            }

            Vibrate(10f, 10f);
            fishingPhase = 3;
        }
    }

    void Phase3()
    {
        Vibrate(0, 0);
        currentFish.transform.position = playerController.transform.position + playerController.transform.forward * 1.5f + playerController.transform.up * 0.75f;
        currentFish.transform.Rotate(new Vector3(0, 20, 0) * Time.deltaTime);

        if (Input.GetButtonDown("StartFishing"))
        {
            playerController.wait = 0.2f;
            fishingEnabled = false;
            playerController.SetFishing(false);
            resetGame();
        }
    }

    public void StartGame()
    {
        fishingEnabled = true;
        pool.GetComponent<AudioSource>().Stop();
        AudioManager.instance.Play("CastReel", pool.gameObject);
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
        AudioManager.instance.Play("Splash", pool.gameObject);
        fishingPhase = 0;
        currentSpins = 0;
        fishingTimer = 1.5f;
        Destroy(currentFish.gameObject);
        currentFish = pool.GetRandomFish();
        Vibrate(0, 0);
    }
    private void Vibrate(float left, float right)
    {
        if (Gamepad.current == null)
        {
            return;
        }
        Gamepad.current.SetMotorSpeeds(left, right);
    }

    public int GetPhase()
    {
        return (fishingPhase);
    }
}


