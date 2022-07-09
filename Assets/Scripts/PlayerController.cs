using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        
        Vector3 horizV = transform.right * horiz;
        Vector3 vertV = transform.forward * vert;

        Vector3 movement = horizV + vertV;
        movement = movement.normalized;

        transform.position += movement;
    }
}
