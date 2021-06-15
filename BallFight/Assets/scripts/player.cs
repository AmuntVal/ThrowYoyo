using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    float time;
    Rigidbody2D playerRig;
    public float AddForceMagn=1;
    public float VelocityMax=1;
    private float speed;
    Transform playertrans;
    Vector3 moveDirction;
    public float rotateVelocity=30;
    public int playerID = 0;
    // Start is called before the first frame update
    
    Vector2 m_Rotate;
    Vector2 m_Move;
    private int m_KeyboardInput;

    public void SetMoveVector(Vector2 vector)
    {
        m_Move = vector;
    }

    public void SetRotateVector(Vector2 vector)
    {
        m_Rotate = vector;
    }

    public void SetRotateN(float f)
    {
        m_KeyboardInput = 1;
    }

    public void SetRotateM(float f)
    {
        m_KeyboardInput = 2;
    }

    void Start()
    {
        m_KeyboardInput = 0;
        playerRig = this.GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        playerRig.AddForce(new Vector3(m_Move.x, m_Move.y, 0) * AddForceMagn);
        speed = Vector3.Magnitude(playerRig.velocity);
        moveDirction = playerRig.velocity.normalized;
        playerRig.AddForce(-moveDirction * AddForceMagn * (speed/VelocityMax));
    }

    private void Rotate()
    {
        if(m_Rotate.x != 0 && m_Rotate.y !=0)
        {
            float angular = 0;
            if (m_Rotate.x > 0)
            {
                angular = Mathf.Rad2Deg * Mathf.Atan(m_Rotate.y / m_Rotate.x) - 90;
            }
            else
            {
                angular = 90 + Mathf.Rad2Deg * Mathf.Atan(m_Rotate.y / m_Rotate.x);
            }
            playerRig.MoveRotation(angular); 
            return;
        }
        
        if(m_KeyboardInput == 1)
        {
            playerRig.MoveRotation(playerRig.rotation-rotateVelocity*Time.deltaTime);
            return;
        }

        if(m_KeyboardInput == 2)
        {
            playerRig.MoveRotation(playerRig.rotation+rotateVelocity*Time.deltaTime);
            return;
        }

    }





/*  原型阶段运动控制，已弃用     
private void Move()
    {

        if (Input.GetAxis("Vertical" + playerID) > 0)
           {
            playerRig.AddForce(new Vector3(0, AddForceMagn, 0));
            }
        if (Input.GetAxis("Vertical" + playerID) < 0)
            {
            playerRig.AddForce(new Vector3(0, -AddForceMagn, 0));
            }
        if (Input.GetAxis("Horizontal" + playerID) > 0)
            {
            playerRig.AddForce(new Vector3(AddForceMagn, 0, 0));
            }
        if (Input.GetAxis("Horizontal" + playerID) < 0)
            {
            playerRig.AddForce(new Vector3(-AddForceMagn, 0, 0));
            }
        if(tag == "Player2")
        {
            if(Input.GetAxis("Horizontal-joystick")> 0)
        {
            playerRig.AddForce(new Vector3(0, AddForceMagn, 0));
        }
        if(Input.GetAxis("Horizontal-joystick")< 0)
        {
            playerRig.AddForce(new Vector3(0, -AddForceMagn, 0));
        }
        if(Input.GetAxis("Vertical-joystick")> 0)
        {
            playerRig.AddForce(new Vector3(AddForceMagn, 0, 0));
        }
        if(Input.GetAxis("Vertical-joystick")< 0)
        {
            playerRig.AddForce(new Vector3(-AddForceMagn, 0, 0));
        }
        }
        
        
        
        speed = Vector3.Magnitude(playerRig.velocity);
        moveDirction = playerRig.velocity.normalized;
        playerRig.AddForce(-moveDirction * AddForceMagn*(speed/VelocityMax));
     }

    private void RotatePlayer()
    {
        if (Input.GetAxis("ClockWise" + playerID) >0)
        {
            playerRig.MoveRotation(playerRig.rotation-rotateVelocity*Time.deltaTime);
        }

        if (Input.GetAxis("ClockWise" + playerID) < 0)
        {
            playerRig.MoveRotation(playerRig.rotation + rotateVelocity * Time.deltaTime);
        }

    } */
}
