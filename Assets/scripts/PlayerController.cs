using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // w a s d yürüme
    public float walkingSpeed = 7.5f;
    public float gravity = 20.0f;

    // kamera
    public Camera playerCamera;
    public float CameraLook = 45.0f;

    // karakter kontrolü
    CharacterController characterController;

    // movement ve look at
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    // hareket edebilir mi
    [HideInInspector]
    public bool canMove = true;

    // baþlangýc + cursor
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        // movement kodu vektörel hali
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedX = canMove ? (walkingSpeed * Input.GetAxis("Vertical")) : 0;
        float curSpeedY = canMove ? (walkingSpeed*Input.GetAxis("Horizontal")) : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //yer çekimi
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        //hareket kodu ve kamera kodu
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * 2.0f;
            rotationX = Mathf.Clamp(rotationX, CameraLook, CameraLook);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * 2.0f, 0);
        }
    }
}