using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameEnums;

public class PlayerController : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 10f;

    private float inputX;
    private float inputY;

    [SerializeField] bool isFishing = false;

    private Directions currentDirection;

    public Directions GetCurrentDirection () { return currentDirection; }  

    enum Rotaastions
    {
        Left =-90,
        Right = 180,
        Up = 0, 
        Down = 90
    }
    public 
    Dictionary<Rotaastions, string> AudioMap;


    public float GetInputX() { return inputX; }
    public float GetInputY() { return inputY; }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        if (!isFishing)
        {
            Vector3 direction = new Vector3(inputX, 0f, inputY);
            controller.Move(direction * Time.deltaTime * speed);
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
        }
        else if (Input.GetKeyDown(KeyCode.I) || CheckDpadUp())
        {
          
            if (controller.transform.rotation.y == (float)Rotaastions.Up)
            {
                return;
            }
            controller.transform.eulerAngles = new (0, (float)Rotaastions.Up, 0);
        }
        else if (Input.GetKeyDown(KeyCode.L) || CheckDpadRight())
        {
            
            if (controller.transform.rotation.y == (float)Rotaastions.Right)
            {
                return;
            }
            controller.transform.eulerAngles = new(0, (float)Rotaastions.Right, 0);
        }
        else if (Input.GetKeyDown(KeyCode.K) || CheckDpadDown())
        {
           
            if (controller.transform.rotation.y == (float)Rotaastions.Down)
            {
                return;
            }
            controller.transform.eulerAngles = new(0, (float)Rotaastions.Down, 0);
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

        currentDirection = (inputX > 0.5f && inputY < 0.5f) ? Directions.Right :
            (inputX < -0.5f && inputY < 0.5f) ? Directions.Left :
            (inputY > 0.5f && inputX < 0.5f) ? Directions.Up :
            (inputY < -0.5f && inputX < 0.5f) ? Directions.Down : Directions.Natural;
    }

}



