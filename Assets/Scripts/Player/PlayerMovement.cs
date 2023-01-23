using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10.0f;

    private InputAction action;
    private CharacterController charController;
    private Vector3 playerInput;
    private Camera mainCamera;

    private void Start()
    {
        charController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        playerInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
        Vector3 TempMovementVector = playerInput;
        TempMovementVector = Quaternion.Euler(0, mainCamera.gameObject.transform.eulerAngles.y, 0) * TempMovementVector;
        //I got this from the rotation video as well, this offsets the moveDirection to be up relative to the camera

        charController.SimpleMove(TempMovementVector);
    }

    #region Input Events

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        //input = input.normalized;
        playerInput = (input.y * movementSpeed * Vector3.forward) + (input.x * movementSpeed * Vector3.right);

        Debug.Log(playerInput);
    }

    #endregion
}
