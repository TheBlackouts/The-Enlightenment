using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float movementSpeed;
    public float mouseSensitivity;
    public float lerpSpeed;
    public float movementLerpSpeed;

    public float yLookLockUp = -70f;
    public float yLookLockDown = 80f;

    private float xRot = 0f;
    private float yRot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        xRot += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        yRot += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRot = Mathf.Clamp(yRot, yLookLockUp, yLookLockDown);

        Vector3 movement = transform.right * horizontal + transform.forward * vertical;
        movement = movement.normalized * movementSpeed;
        movement = new Vector3(movement.x, 0f, movement.z);

        Camera.main.transform.localRotation = Quaternion.Euler(Mathf.LerpAngle(Camera.main.transform.rotation.eulerAngles.x, -yRot, Mathf.Clamp01(lerpSpeed * Time.deltaTime)), 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, Mathf.LerpAngle(transform.rotation.eulerAngles.y, xRot, lerpSpeed * Time.deltaTime), 0f);

        rb.velocity = Vector3.Lerp(rb.velocity, movement, Time.deltaTime * movementLerpSpeed);
    }
}
