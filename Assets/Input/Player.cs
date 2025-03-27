using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    RickyRolls playerInput;
    InputAction moveInput, lookInput, fireInput, exitInput;
    [SerializeField] Transform childBody;

    private ShootProjectile shootProjectile;
    private float turnSpeed = 10f;

    public float moveSpeed = 100f;

    private void Awake()
    {
        playerInput = new RickyRolls();
        shootProjectile = GetComponent<ShootProjectile>();

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }

    private void OnEnable()
    {
        moveInput = playerInput.Player.Move;
        moveInput.Enable();

        lookInput = playerInput.Player.Look;
        lookInput.Enable();

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
        lookInput.Disable();
        fireInput.Disable();
        exitInput.Disable();    
    }

    private void Update()
    {
        Vector2 vectorInput = moveInput.ReadValue<Vector2>();

        //Debug.Log("input is " + vectorInput.x + "x and " + vectorInput.y);

        transform.Translate(new Vector3(vectorInput.x, 0, vectorInput.y) * Time.deltaTime * moveSpeed);
        PlayerLookAtMouse();
    }


    void Fire(InputAction.CallbackContext context)
    {
        shootProjectile.Spawn();
    }

    void Quit(InputAction.CallbackContext context)
    {
        Debug.Log("Quited");
        Application.Quit();
    }
    void PlayerLookAtMouse()
    {
        Plane playerPlane = new Plane(Vector3.up, childBody.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitDistance;

        if (playerPlane.Raycast(ray, out hitDistance))
        {

            Vector3 targetPoint = ray.GetPoint(hitDistance);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - childBody.position);

            childBody.rotation = Quaternion.Slerp(childBody.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    void PlayerRotationJoystick()
    {
        Vector2 vector2 = lookInput.ReadValue<Vector2>();

        if (vector2 == Vector2.zero)
            return;

        float dot = Vector3.Dot(Vector2.up, vector2.normalized);

        float angle = Mathf.Rad2Deg * Mathf.Acos(dot);

        childBody.rotation = Quaternion.Euler(0, angle, 0);
    }
}
