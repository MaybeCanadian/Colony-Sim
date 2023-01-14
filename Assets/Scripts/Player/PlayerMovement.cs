using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 10.0f;

    public float rotateSpeed = 10.0f;

    private Vector2 input = Vector2.zero;

    private Vector3 oldMousePos = Vector3.zero;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(0.0f, 0.0f);

        if(Input.GetKey(KeyCode.W))
        {
            input.y += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            input.y -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            input.x += 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            input.y -= 1;
        }

        rb.MovePosition(transform.position + transform.forward * input.y * moveSpeed);

        Vector3 mouseDirection = (Input.mousePosition - new Vector3(Screen.width/2, Screen.height/2, 0)).normalized;

        if(mouseDirection.x < 0.5f && mouseDirection.x > -0.5f)
        {
            mouseDirection.x = 0.0f;
        }

        if(mouseDirection.y < 0.5f && mouseDirection.y > -0.5f)
        {
            mouseDirection.y = 0.0f;
        }

        transform.localRotation = new Quaternion(transform.localRotation.x - rotateSpeed * mouseDirection.y * Time.deltaTime, transform.localRotation.y + rotateSpeed * mouseDirection.x * Time.deltaTime, transform.localRotation.z, transform.localRotation.w);


        oldMousePos = Input.mousePosition;
    }
}
