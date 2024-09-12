using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using static GameEnums;

public class PlayerController : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 10f;
    [SerializeField] bool isFishing = false;

    private float inputX;
    private float inputY;

    private Directions currentDirection;


    public float GetInputX() { return inputX; }
    public float GetInputY() { return inputY; }

    public Directions GetCurrentDirection() { return currentDirection; }

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
    }

    private void UpdateCharacterDirection()
    {
        currentDirection = (inputX > 0.5f && inputY < 0.5f) ? Directions.Right :
                    (inputX < -0.5f && inputY < 0.5f) ? Directions.Left :
                    (inputY > 0.5f && inputX < 0.5f) ? Directions.Up :
                    (inputY < -0.5f && inputX < 0.5f) ? Directions.Down :
                    Directions.Netrual;
    }

}
