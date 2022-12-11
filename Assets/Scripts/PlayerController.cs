using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [Header("Controls:")]
    [SerializeField] private bool look;

    [Header("Player Values:")]
    [SerializeField] private float speedValue = 5f;
    [SerializeField] private float sprintMaxValue = 3f;

    // Object's
    private Rigidbody rb;
    private GameObject cam;

    private float sprintValue = 1;

    private Vector2 moveValue;
    private Vector2 lookValue;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = GameObject.Find("Camera");
    }
    public void Update()
    {
        Move();
        if(look) Look();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookValue = context.ReadValue<Vector2>();
    }
    public void OnShift(InputAction.CallbackContext context)
    {
        if (context.performed) sprintValue = sprintMaxValue;
        else if (context.canceled) sprintValue = 1;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumped");
    }
    private void Move()
    {
        rb.velocity = transform.forward * moveValue.y * speedValue * sprintValue;
    }
    private void Look()
    {
        transform.Rotate(new Vector3(0, lookValue.x, 0));
        cam.transform.Rotate(new Vector3(-lookValue.y, 0, 0));
    }
    private void LimitCamRotation()
    {
        //limit camera rotation
        Vector3 cameraEulerAngles = cam.transform.rotation.eulerAngles;

        cameraEulerAngles.x = (cameraEulerAngles.y > 180) ? cameraEulerAngles.x - 360 : cameraEulerAngles.x;
        cameraEulerAngles.x = Mathf.Clamp(cameraEulerAngles.x, -60, 60);

        cam.transform.rotation = Quaternion.Euler(cameraEulerAngles);
    }
}
