using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AllPlayer : MonoBehaviour
{
    float time;
    Rigidbody2D playerRig;
    public float AddForceMagn = 1;
    public float VelocityMax = 1;
    private float speed;
    Transform playertrans;
    Transform ballTrans;
    Vector3 moveDirction;
    public float rotateVelocity = 30;
    public int playerID = 0;
    // Start is called before the first frame update

    Vector2 m_Rotate;
    Vector2 m_Move;

    public bool attack;//AI是否攻击人
    public Vector2[] testVec;//给AI的多方向
    public int dirNum = 0;//方向编号
    public float changeTime = 0.3f;//AI的手速（？）
    public GameObject other; //对战的敌人
    private float disPlayer;//AI和玩家的距离
    private float disBall;//AI和毛丹的距离
    private Vector2 midVec;//AI和玩家的向量差，用于判断往哪个方向攻击

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
        ballTrans = transform.Find("ball");
        StartCoroutine("ChangeDir");//AI过几秒改变一次方向
        testVec = new Vector2[4];
        testVec[0] = new Vector2(1, 0);
        testVec[1] = new Vector2(0, -1);
        testVec[2] = new Vector2(-1, 0);
        testVec[3] = new Vector2(0, 1);
    }

    IEnumerator ChangeDir()
    {
        while (true)
        {
            dirNum++;
            if (dirNum >= testVec.Length) dirNum = 0;
            //设置间隔时间为2秒
            if(attack&&CheckDistance()&& CheckAttack()) yield return new WaitForSeconds(changeTime*1.5f);
            //判断是否是有利方向,AI采取进攻策略，如果己身-他身>己身-己球则靠近，反之则远离，符合这个策略的方向就是有利方向。
            yield return new WaitForSeconds(changeTime);
        }
    }

    bool CheckDistance()
    {
        disPlayer = Vector2.Distance(this.transform.position, other.transform.position);
        disBall = Vector2.Distance(this.transform.position, ballTrans.position);
        if (disPlayer > disBall) return true; //如果和玩家的距离大于和毛丹的距离，就需要靠近玩家
        else return false;
    }

    bool CheckAttack()
    {
        //判断是否进攻方向
        midVec = this.transform.position - other.transform.position;
        if (dirNum == 0 && midVec.x < 0) return true;
        else if (dirNum == 1 && midVec.y > 0) return true;
        else if (dirNum == 2 && midVec.x > 0) return true;
        else if (dirNum == 3 && midVec.y < 0) return true;
        else return false;
    }

    void FixedUpdate(){
        SetMoveVector(testVec[dirNum]);
        Move();
        Rotate();
    }

    private void Move()
    {
            playerRig.AddForce(new Vector3(m_Move.x, m_Move.y, 0) * AddForceMagn);
            speed = Vector3.Magnitude(playerRig.velocity);
            moveDirction = playerRig.velocity.normalized;
            playerRig.AddForce(-moveDirction * AddForceMagn * (speed / VelocityMax));
    }

    private void Rotate()
    {
        if (m_Rotate.x != 0 && m_Rotate.y != 0)
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

        if (m_KeyboardInput == 1)
        {
            playerRig.MoveRotation(playerRig.rotation - rotateVelocity * Time.deltaTime);
            return;
        }

        if (m_KeyboardInput == 2)
        {
            playerRig.MoveRotation(playerRig.rotation + rotateVelocity * Time.deltaTime);
            return;
        }

    }

/* 上是（0，1），下是（0，-1），左（-1，0），右（1，0），斜方向的数值是0.7



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
