using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
using static GameEnums;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip scrapeWallSound;
    [SerializeField] private AudioClip[] gravelFootsteps;
    [SerializeField] private AudioClip[] floorFootsteps;
    [SerializeField] private AudioClip[] waterFootsteps;
    [SerializeField] private AudioClip[] caveFootsteps;
    private AudioClip[] currentFootsteps;

    [SerializeField] private float footstepDelay;
    private int clipIndex;
    private AudioSource audioSource;
    private AudioSource footstepSource;

    [SerializeField] public CharacterController controller;
    [SerializeField] float speed = 10f;
    public float wait;

    public static Func<PlayerController> GetInstance = () => null;

    [SerializeField] float rotationSpeed = 10f;

    private float leftJoystickInputX;
    private float leftJoystickInputY;

    private float rightJoystickInputX;

    private FishingGame fishGame;

    [SerializeField] bool isFishing = false;
    bool canMove = true; 

    public Image eyes;
    public bool canPlaySound = true;
    private bool isMoving = false;
    private bool isTouchingWall = false;

    private Directions currentDirection;

    public Directions GetCurrentDirection() { return currentDirection; }

    public void SetCurrentFishGame(FishingGame game) { fishGame = game; }

    enum Rotaastions
    {
        Left = -90,
        Right = 90,
        Up = 0,
        Down = 180
    }

    public float GetInputX() { return leftJoystickInputX; }
    public float GetInputY() { return leftJoystickInputY; }

    private void Awake()
    {
        GetInstance = () => this; 
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        footstepSource = gameObject.transform.GetChild(2).GetComponent<AudioSource>();
        currentFootsteps = gravelFootsteps;
    }
    // Update is called once per frame
    void Update()
    {

        if (!canMove)
        {
            return;
        }

        leftJoystickInputX = Input.GetAxis("LeftJoystickHorizontal");
        leftJoystickInputY = -Input.GetAxis("LeftJoystickVertical");
        rightJoystickInputX = Input.GetAxis("RightJoystickHorizontal");

        if ((leftJoystickInputY > 0.1 || leftJoystickInputX > 0.1))
        {
            isMoving = true;
        }
        else if ((leftJoystickInputY < -0.1 || leftJoystickInputX < -0.1))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

            if (Input.GetKeyDown(KeyCode.Space))
        {
            if (eyes.enabled)
            {
                eyes.enabled = false;
            }
            else
            {
                eyes.enabled = true;
            }
        }


        if (!isFishing)
        {
            float rotationAmount = rightJoystickInputX * rotationSpeed * Time.deltaTime;
            controller.transform.Rotate(0, rotationAmount, 0);
            Vector3 forward = controller.transform.forward;
            Vector3 right = controller.transform.right;

            Vector3 direction = (forward * leftJoystickInputY) + (right * leftJoystickInputX);

            if (direction.magnitude > 1)
            {
                direction.Normalize();
            }

            controller.Move(direction * Time.deltaTime * speed);
        }

        if (wait > 0)
        {
            wait -= Time.deltaTime;
        }

        if (Input.GetButtonDown("StartFishing"))
        {
            if (fishGame != null && !isFishing && wait <= 0)
            {
                fishGame.StartGame();
                isFishing = true;
            }
        }

        if(isMoving && !isTouchingWall && !footstepSource.isPlaying && !isFishing)
        {
            clipIndex = UnityEngine.Random.Range(1, currentFootsteps.Length);
            AudioClip clip = currentFootsteps[clipIndex];
            footstepSource.PlayOneShot(clip);
            currentFootsteps[clipIndex] = currentFootsteps[0];
            currentFootsteps[0] = clip;
        }

        UpdateCharacterDirection();

        //Gamepad.current.dpad.left.isPressed || 
        if (Input.GetKeyDown(KeyCode.J) || CheckDpadLeft())
        {

            if (controller.transform.rotation.y == (float)Rotaastions.Left)
            {
                return;
            }
            controller.transform.eulerAngles = new(0, (float)Rotaastions.Left, 0);

            PlaySound("Facing West");
        }
        else if (Input.GetKeyDown(KeyCode.I) || CheckDpadUp())
        {

            if (controller.transform.rotation.y == (float)Rotaastions.Up)
            {
                return;
            }
            controller.transform.eulerAngles = new(0, (float)Rotaastions.Up, 0);
            PlaySound("Facing North");
        }
        else if (Input.GetKeyDown(KeyCode.L) || CheckDpadRight())
        {

            if (controller.transform.rotation.y == (float)Rotaastions.Right)
            {
                return;
            }
            controller.transform.eulerAngles = new(0, (float)Rotaastions.Right, 0);
            PlaySound("Facing East");
        }
        else if (Input.GetKeyDown(KeyCode.K) || CheckDpadDown())
        {

            if (controller.transform.rotation.y == (float)Rotaastions.Down)
            {
                return;
            }
            controller.transform.eulerAngles = new(0, (float)Rotaastions.Down, 0);
            PlaySound("Facing South");
        }

    }
    private bool CheckDpadLeft()
    {

        if (Gamepad.current == null)
        {
            return false;
        }
        return Gamepad.current.dpad.left.isPressed;
    }
    private bool CheckDpadRight()
    {

        if (Gamepad.current == null)
        {
            return false;
        }
        return Gamepad.current.dpad.right.isPressed;
    }
    private bool CheckDpadUp()
    {

        if (Gamepad.current == null)
        {
            return false;
        }
        return Gamepad.current.dpad.up.isPressed;
    }
    private bool CheckDpadDown()
    {

        if (Gamepad.current == null)
        {
            return false;
        }
        return Gamepad.current.dpad.down.isPressed;
    }

    private void UpdateCharacterDirection()
    {

        currentDirection = (leftJoystickInputX > 0.5f && leftJoystickInputY < 0.5f) ? Directions.Right :
            (leftJoystickInputX < -0.5f && leftJoystickInputY < 0.5f) ? Directions.Left :
            (leftJoystickInputY > 0.5f && leftJoystickInputX < 0.5f) ? Directions.Up :
            (leftJoystickInputY < -0.5f && leftJoystickInputX < 0.5f) ? Directions.Down : Directions.Natural;

    }

    private void PlaySound(string soundToPlay)
    {
        if (AudioManager.instance == null || soundToPlay == null)
        {
            return;
        }

        AudioManager.instance.Play(soundToPlay, gameObject);
    }

    private void OnCollisionEnter(Collision hit)
    {
        if(hit.gameObject.CompareTag("Wall") == true)
        {
            Debug.Log("Hit a wall");
            PlaySound("Thud");
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;

            if (isMoving)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = scrapeWallSound;
                    audioSource.Play();
                    Debug.Log("Staying in wall");
                }
            }
            else
            {
                audioSource.Stop();
                Debug.Log("Stopping wall scrape");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            audioSource.Stop();
            isTouchingWall = false;

        }
    }


    public void SetFishing(bool set)
    {
        isFishing = set;
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }
    public void DisableMovement(bool state)
    {
        canMove = state;

    }
    public void ChangeFootstep(string footstepType)
    {
        switch (footstepType)
        {
            case "Cave":
                footstepSource.volume = 0.2f;
                currentFootsteps = caveFootsteps;
                break;
            case "Water":
                footstepSource.volume = 0.08f;
                currentFootsteps = waterFootsteps;
                break;
            case "Floor":
                footstepSource.volume = 0.26f;
                currentFootsteps = floorFootsteps;
                break;
            default:
                break;
        }
    }
}



