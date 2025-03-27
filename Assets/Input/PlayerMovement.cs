using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    RickyRolls playerInput;
    InputAction moveInput, fireInput, exitInput;

    public float moveSpeed = 100f;

    private void Awake()
    {
        playerInput = new RickyRolls();
        
    }

    private void OnEnable()
    {
        moveInput = playerInput.Player.Move;
        moveInput.Enable();

        fireInput = playerInput.Player.Fire;
        fireInput.Enable();
        fireInput.performed += Fire;

        exitInput = playerInput.Player.Exit;
        exitInput.Enable();
        exitInput.performed += Quit;
    }

    private void OnDisable()
    {
        moveInput.Disable();
        fireInput.Disable();
        exitInput.Disable();    
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector2 vectorInput = moveInput.ReadValue<Vector2>();

        //Debug.Log("input is " + vectorInput.x + "x and " + vectorInput.y);

        transform.Translate(new Vector3(vectorInput.x, 0, vectorInput.y) * Time.deltaTime * moveSpeed);
    }


    void Fire(InputAction.CallbackContext context)
    {


        Debug.Log("pew pew pew");
    }

    void Quit(InputAction.CallbackContext context)
    {
        Debug.Log("Quited");
        Application.Quit();
    }
}
