using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public Animator animator;
    public string FaceChange;
    public string ReactChange;
    Rigidbody2D playerRig;
    [Header("基础数值")]
    public float AddForceMagn = 1;
    public float VelocityMax = 1;
    private float speed;
    Transform playertrans;
    Vector3 moveDirction;
    public float rotateVelocity = 30;
    public int playerID = 0;
    public SpriteRenderer shield;
    public GameObject deadAnimator;
    public GameObject melonRenderer;
    public Image cdMask;
    // Start is called before the first frame update


    Vector2 m_Rotate;
    Vector2 m_Move;
    private int m_KeyboardInput;

    [Header("闪避数值")]
    [Tooltip("真实锁血时间N")]
    public float shieldDuration = 1.0f;
    [Tooltip("表现缩写时间W")]
    public float goldenDuration = 0.9f;
    public float cdDuration = 5.0f;
    [Tooltip("每次闪避能量槽增加的百分比")]
    public float energyFillPerShield = 20.0f;
    [Tooltip("闪避时，刚体质量")]
    public float shieldMass = 100.0f;
    float m_NormalMelonMass;
    float m_CurrentShieldCD;
    float m_CurrentGoldenTime;
    float m_CurrentShieldTime;
    bool m_ShieldStatus;
    bool m_GoldenStatus;
    GetHurt m_GetHurt;
    bool m_ShieldSuccess;
    bool m_Dead;

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

    public bool GetShieldStatus()
    {
        return m_ShieldStatus;
    }

    public void SetShieldSuccessStatus(bool status)
    {
        m_ShieldSuccess = status;
    }

    void Start()
    {
        m_KeyboardInput = 0;
        playerRig = this.GetComponent<Rigidbody2D>();
        m_CurrentShieldCD = 0;
        m_CurrentGoldenTime = 0;
        m_CurrentShieldTime = 0;
        m_GoldenStatus = false;
        m_ShieldStatus = false;
        m_GetHurt = GetComponent<GetHurt>();
        m_NormalMelonMass = playerRig.mass;
        m_Dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentShieldCD = countdownToZero(m_CurrentShieldCD);
        cdMask.fillAmount = m_CurrentShieldCD / cdDuration;
        m_CurrentGoldenTime = countdownToZero(m_CurrentGoldenTime);
        if (m_GoldenStatus && m_CurrentGoldenTime == 0)
        {
            Debug.Log("黄金壳消失！");
            GameObject.Find("Canvas").SendMessage(FaceChange, 0, SendMessageOptions.DontRequireReceiver);//红西瓜切换成第0个表情，把这一句直接放到其他脚本里就行
            //播放正常表情
            shield.color = new Color(1, 1, 1, 0);
            m_GoldenStatus = false;
        }
        m_CurrentShieldTime = countdownToZero(m_CurrentShieldTime);
        if (m_ShieldStatus && m_CurrentShieldTime == 0)
        {
            Debug.Log("闪避状态结束！");
            if (m_ShieldSuccess) m_CurrentShieldCD = 0;
            playerRig.mass = m_NormalMelonMass;
            m_ShieldStatus = false;
        }
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

    public void StartShield()
    {
        if (m_CurrentShieldCD == 0)
        {
            m_GoldenStatus = true;
            m_CurrentGoldenTime = goldenDuration;
            m_ShieldStatus = true;
            m_ShieldSuccess = false;
            m_CurrentShieldTime = shieldDuration;
            Debug.Log("格挡！变成金黄色！");
            shield.color = new Color(1, 1, 1, 0.6f);
            playerRig.mass = shieldMass;
            m_CurrentShieldCD = cdDuration;
        }

    }

    private float countdownToZero(float time)
    {
        if (time > 0) time = time - Time.deltaTime;
        if (time < 0) time = 0;
        return time;
    }

    public void StartWuShuang(float decayTime)
    {
        m_CurrentShieldTime = 0;
        m_CurrentGoldenTime = 0;
        m_GoldenStatus = false;
        m_ShieldStatus = false;
        Debug.Log("结束所有闪避相关状态");
        Debug.Log("无双开启!!!∑(ﾟДﾟノ)ノ");
        GameObject.Find("Canvas").SendMessage(FaceChange, 1, SendMessageOptions.DontRequireReceiver);
        //播放笑的表情
        Debug.Log("播放笑的表情");
        animator.SetBool("isBig", true);
        GetComponent<AttackBall>().workForMelon = true;
        m_CurrentShieldCD = 999.99f;
        m_GetHurt.SetImmortalTime(decayTime, decayTime);
    }

    public void EndWushuang()
    {
        Debug.Log("无双结束~~ಠ╭╮ಠ~~");
        GameObject.Find("Canvas").SendMessage(FaceChange, 0, SendMessageOptions.DontRequireReceiver);
        //播放正常表情
        animator.SetBool("isBig", false);
        GetComponent<AttackBall>().workForMelon = false;
        m_CurrentShieldCD = 0f;
        //如有需要，可在结束无双的时候，重置无敌时间（GetHurt类中已会对无敌时间重新调整）
        //m_GetHurt.SetImmortalTime(0, 0);
    }

    public void PlayDead()
    {
        gameObject.SetActive(false);
        GameObject.Instantiate(deadAnimator, transform.position, Quaternion.identity);
        Destroy(melonRenderer);
        AudioManager.instance.Play("die");
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
