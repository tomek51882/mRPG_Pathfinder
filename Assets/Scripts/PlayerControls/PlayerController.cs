using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    PlayerAnimationController animationController;
    public GameObject cameraTransform;
    public float speed = 12f;
    public float gravity = -9.8f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;

    public LayerMask groundMask;
    public Transform groundCheck;
    public Transform cam;

    public Vector2 _move;
    public Vector2 _look;
    public bool freeLookEnabled;
    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;

    CharacterController controller;
    Vector3 velocity;
    float fallVelocity;
    bool isGrounded;
    bool isLanded = true;

    float turnSmoothVelocity;
    float turnSmoothTime = 0.1f;


    public void OnMove(InputAction.CallbackContext value)
    {
        _move = value.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext value)
    {
        _look = value.ReadValue<Vector2>();
    }
    public void OnFreeLook(InputAction.CallbackContext value)
    {
        freeLookEnabled = (value.ReadValue<float>() == 1f);
    }
    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (isGrounded)
            {
                fallVelocity = Mathf.Sqrt(jumpHeight * -2 * gravity);
                animationController.JumpStart();
            }
        }
    }
    public void OnInteract(InputAction.CallbackContext value)
    {
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Debug.Log("We hit " + hit.collider.name + " " + hit.point);
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                InteractWithObject(interactable);
            }
        }
    }
    void InteractWithObject(Interactable interactableObject)
    {
        interactableObject.OnFocus(transform);
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animationController = GetComponent<PlayerAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        animationController.isGrounded = isGrounded;
        Vector3 direction = new Vector3(_move.x, 0f, _move.y);
        Quaternion previousCameraRotation = cameraTransform.transform.rotation;
        if (isGrounded && fallVelocity < 0)
        {
            fallVelocity = -2f;
        }
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.transform.eulerAngles.y;
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir * speed * Time.deltaTime);
        }

        #endregion
        #region CameraRotation
        //Lock camera position so players rotation does not have impact on camera
        cameraTransform.transform.rotation = previousCameraRotation;

        if (freeLookEnabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            cameraTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);
            cameraTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * -rotationPower, Vector3.right);
            var angles = cameraTransform.transform.localEulerAngles;
            angles.z = 0;
            var angle = cameraTransform.transform.localEulerAngles.x;
            //Clamp the Up/Down rotation
            if (angle > 180 && angle < 300)
            {
                angles.x = 300;
            }
            else if (angle < 180 && angle > 60)
            {
                angles.x = 60;
            }
            cameraTransform.transform.localEulerAngles = angles;
        }
        else
        {
            
            Cursor.lockState = CursorLockMode.None;
        }
        #endregion
        #region Falling
        fallVelocity += gravity * Time.deltaTime;
        animationController.velocity = new Vector3(_move.x, fallVelocity, _move.y);
        controller.Move(new Vector3(0f,fallVelocity,0f) * Time.deltaTime);
        #endregion
    }
}
