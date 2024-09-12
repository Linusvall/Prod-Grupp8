using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 10f;

    private float inputX;
    private float inputY;

    public float GetInputX() { return inputX; }
    public float GetInputY() { return inputY; }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(inputX, 0f, inputY);
        controller.Move(direction * Time.deltaTime * speed);
    }
}
