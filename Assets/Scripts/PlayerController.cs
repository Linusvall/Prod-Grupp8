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

    [SerializeField] private AudioClip[] footsteps;
    [SerializeField] private float footstepDelay;
    private int clipIndex;
    private AudioSource audioSource;

     [SerializeField]  public CharacterController controller;
    [SerializeField] float speed = 10f;
    public float wait;

    [SerializeField] float rotationSpeed = 10f;

    private float leftJoystickInputX;
    private float leftJoystickInputY;

    private float rightJoystickInputX;

    private FishingGame fishGame;

    [SerializeField] bool isFishing = false;

    public Image eyes;
   public bool canPlaySound = true;

    private Directions currentDirection;

    public Directions GetCurrentDirection () { return currentDirection; }  

    public void SetCurrentFishGame(FishingGame game) { fishGame = game; }

    enum Rotaastions
    {
        Left =-90,
        Right = 90,
        Up = 0, 
        Down = 180
    }

   



    

    public float GetInputX() { return leftJoystickInputX; }
    public float GetInputY() { return leftJoystickInputY; }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
     
        leftJoystickInputX = Input.GetAxis("LeftJoystickHorizontal");
        leftJoystickInputY = -Input.GetAxis("LeftJoystickVertical");
        rightJoystickInputX = Input.GetAxis("RightJoystickHorizontal");
        if((leftJoystickInputY > 0.1f || leftJoystickInputX > 0.1f) && !audioSource.isPlaying && !isFishing)
        {
            clipIndex = Random.Range(1, footsteps.Length);
            AudioClip clip = footsteps[clipIndex];
            audioSource.PlayOneShot(clip);
            footsteps[clipIndex] = footsteps[0];
            footsteps[0] = clip;
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

        if (Input.GetButtonDown("StartFishing")){
            if(fishGame != null && !isFishing && wait <= 0)
            {
                fishGame.StartGame();
                isFishing = true;
            }
        }
    




        UpdateCharacterDirection();

        //Gamepad.current.dpad.left.isPressed || 
        if (Input.GetKeyDown(KeyCode.J) || CheckDpadLeft())
        {
            
            if(controller.transform.rotation.y == (float)Rotaastions.Left)
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
            controller.transform.eulerAngles = new (0, (float)Rotaastions.Up, 0);
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

        if(Gamepad.current == null)
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
        if(AudioManager.instance == null || soundToPlay == null)
        {
            return; 
        }

        AudioManager.instance.Play(soundToPlay, gameObject);
    }

  /*  private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Hit a wall");
        if (hit.gameObject.CompareTag("Wall"))
        {
            if (!canPlaySound) return;
            PlaySound("Thud");
            canPlaySound = false;
        }
  
    }*/
    private void OnCollisionEnter(Collision hit)
    {
        Debug.Log("Hit a wall");
        PlaySound("Thud");
    }


    public void SetFishing(bool set)
    {
        isFishing = set;
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }
}



