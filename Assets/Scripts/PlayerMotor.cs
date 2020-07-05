using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 moveVector;
    public float speed = 5.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;

    private float animationDuration = 3.0f;
    private float startTime;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            return;
        }

        if(Time.time - startTime < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }
        moveVector = Vector3.zero;

        if(controller.isGrounded)
        {
            verticalVelocity = 0.0f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        //x - left and right
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        if(Input.GetMouseButton(0))
        {
            // are we holding touch on the right side?
            if(Input.mousePosition.x > Screen.width /2)
            {
                moveVector.x = speed;
            }
            else
            {
                moveVector.x = -speed;
            }
        }
        //y - up and down
        moveVector.y = verticalVelocity;
        //y - forward and backward
        moveVector.z = speed;
        controller.Move((moveVector) * Time.deltaTime);
    }

    public void SetSpeed(float modifier)
    {
        speed = 5.0f + modifier;
    }

    // it is being called every time our player hits something
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.point.z > transform.position.z + controller.radius)
        {
            BluetoothTest.SendValue("d");
            //ArduinoConnect.SendValue("d");
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        GetComponent<Score>().OnDeath();
    }

    public bool playerCollideTrue()
    {
        return isDead;
    }

}
