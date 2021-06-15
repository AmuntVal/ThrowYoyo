using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelonRenderer : MonoBehaviour
{
    
    public Animator animator;
    public GameObject currentPlayer;
    public GameObject attackBall;
    public float speedScale=1;

    /// <summary>
    /// 备选随机汁水类型，需要手动添加
    /// </summary>
    public Sprite[] juicyLiquidSprites;

    // 关于受伤顿帧、汁液的变量
    public float hurtFps = 0.3f;
    public float hurtTimeConstant = 5.0f;
    public float JuicyTimeConstant = 1.0f;
    private bool isHurt;
    //private float remainingTime;
    private float initialHurtTime;

    private GameObject juicyLiquidInstance;


    // Start is called before the first frame update
    void Start()
    {
        isHurt = false;
        animator = GetComponent<Animator>();
        //attackBall = currentPlayer.GetComponentInChildren<AttackBall>();
        //animator.SetBool("isClockWise", isClockWise());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLocation();

        UpdateDirectionState();

        UpdatePlaySpeed();

        UpdateHurtState();
                       
        //Debug.Log("当前角速度" + WeaponAngularVelocity() + "播放速度" + animator.speed);
    }
    /// <summary>
    /// 获取武器相对于当前玩家的角速度，由此确定动画的播放速度.
    /// </summary>
    /// <returns></returns>
    private float WeaponAngularVelocity()
    {
        float rotationRadius = (attackBall.gameObject.transform.position - currentPlayer.transform.position).sqrMagnitude ;
        return Vector2.SqrMagnitude( attackBall.GetComponent<Rigidbody2D>().velocity - currentPlayer.GetComponent<Rigidbody2D>().velocity) / rotationRadius;
    }



    /// <summary>
    /// 判断当前武器是顺时针还是逆时针.
    /// </summary>
    /// <returns></returns>
    private bool isClockWise()
    {
        Vector2 radiusVector = attackBall.gameObject.transform.position - currentPlayer.transform.position;
        Vector2 linearvelocityVector = attackBall.GetComponent<Rigidbody2D>().velocity - currentPlayer.GetComponent<Rigidbody2D>().velocity;
        return (Vector3.Cross(radiusVector, linearvelocityVector).z < 0 );

    }

    /// <summary>
    /// 收到打击时的顿帧方法
    /// </summary>
    public void HurtDelayPlay()
    {
        initialHurtTime = Time.time;

        if(juicyLiquidInstance == null)
        {
            JuicyLiquidPlay();
        }

        isHurt = true;
    }


    private void UpdateLocation()
    {
        this.transform.position = currentPlayer.transform.position;
    }
    private void UpdateDirectionState()
    {
        animator.SetBool("isClockwise", isClockWise());
    }
    public void UpdatePlaySpeed()
    {
        animator.speed = isHurt ? hurtFps : WeaponAngularVelocity() * speedScale / Mathf.PI;
    }

    public void UpdateHurtState()
    {
        if(isHurt && Time.time - initialHurtTime >= hurtTimeConstant)
        {
            isHurt = false;
        }

        if(isHurt && Time.time - initialHurtTime >= JuicyTimeConstant)
        {
            Destroy(juicyLiquidInstance);
        }
    }



    public void JuicyLiquidPlay()
    {
        juicyLiquidInstance = new GameObject();
        juicyLiquidInstance.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 0.25f);
        //juicyLiquidInstance.transform.parent = this.transform;
        juicyLiquidInstance.AddComponent<SpriteRenderer>();
        juicyLiquidInstance.GetComponent<SpriteRenderer>().sprite = juicyLiquidSprites[Random.Range(0, juicyLiquidSprites.Length)];
    }
}
