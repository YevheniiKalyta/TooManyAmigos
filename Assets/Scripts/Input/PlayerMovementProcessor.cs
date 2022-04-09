using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementProcessor : InputProcessor, IInputProcessor
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed;
    public override void ProcessInput()
    {
        HandleMovement();
        HandleRotation();
    }
    private void FixedUpdate()
    {
        HandleGravity();
    }
    private void HandleRotation()
    {
        Vector3 lookPos = InputManager.Instance.GetMousePositionWorld() - transform.position;
        float rotation = (Mathf.Atan2(lookPos.x, lookPos.z) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
    private void HandleMovement()
    {
        characterController.Move(InputManager.Instance.GetPlayerMovement() * speed * Time.deltaTime);
    }
    private void HandleGravity()
    {
        characterController.Move(Vector3.down * 9.81f * Time.deltaTime);
    }
}
