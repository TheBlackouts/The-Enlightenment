using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float movementSpeed;
    public float mouseSensitivity;
    public float lookSmoothing;
    public float movementSmoothing;

    public float yLookUpLock = -70f;
    public float yLookDownLock = 80f;

    public float normalCameraPosY = 0.5f;
    public float crouchCameraPosY = -0.25f;
    public float crouchSmoothing;

    private float xLookRot = 0f;
    private float yLookRot = 0f;

    private Transform camT;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        camT = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        xLookRot += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        yLookRot += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yLookRot = Mathf.Clamp(yLookRot, yLookUpLock, yLookDownLock);

        Vector3 movement = (transform.right * horizontal) + (transform.forward * vertical);
        movement = movement.normalized * movementSpeed;
        movement = new Vector3(movement.x, 0f, movement.z);

        camT.localRotation = Quaternion.Euler(Mathf.LerpAngle(camT.rotation.eulerAngles.x, -yLookRot, lookSmoothing * Time.deltaTime), 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, Mathf.LerpAngle(transform.rotation.eulerAngles.y, xLookRot, lookSmoothing * Time.deltaTime), 0f);

        rb.velocity = Vector3.Lerp(rb.velocity, movement, Time.deltaTime * movementSmoothing);
    
        if (Input.GetKey(KeyCode.LeftControl))
        {
            camT.localPosition = new Vector3(0f, Mathf.Lerp(camT.localPosition.y, crouchCameraPosY, Time.deltaTime * crouchSmoothing), 0f);
        }
        else
        {
            camT.localPosition = new Vector3(0f, Mathf.Lerp(camT.localPosition.y, normalCameraPosY, Time.deltaTime * crouchSmoothing), 0f);
        }
    }
}
