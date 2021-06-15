using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 经目测每三帧到达原角度时，转过约30度.
/// </summary>
public class MelonAnimationController : MonoBehaviour
{
    public GameObject thisPlayer;
    public Sprite[] sprites;
    

    public AttackBall attackBall;
    private SpriteRenderer spriteRenderer;

    public float hurtFps = 1;
    public float hurtTimeConstant = 10.0f;
    public bool isHurt;
    private float remainingTime;
    private int currentHurtIndex;
    private float startHurtTime;
    private float initialHurtTime;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //thisPlayer = GetComponent<player>();
        attackBall = GetComponentInChildren<AttackBall>();
        isHurt = false;
        //Debug.Log(-1 % 8 + "qumo");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isHurt)
        {
            spriteRenderer.sprite = sprites[GetCurrentIndex()];
        }
        else
        {
            if(Time.time - initialHurtTime >= hurtTimeConstant)
            {
                isHurt = false;
            }
            GetHurtIndex();
            if (currentHurtIndex > 7 || currentHurtIndex < 0)
                Debug.LogError("顿帧越界 " + currentHurtIndex);
            
            spriteRenderer.sprite = sprites[currentHurtIndex];
        }

    }

    private int GetCurrentIndex()
    {
        Vector2 relativeVector = attackBall.transform.position - this.transform.position;
        int index = (int)Vector2.Angle(new Vector2(0f, -1f), relativeVector);

        return ((relativeVector.x >= 0? 360 - index: index)/ 45 ) % 8;
        //return index / 120;
    }

    private void GetHurtIndex()
    {
        remainingTime = Time.time - startHurtTime;
        Debug.Log("shengyushij " + remainingTime + "biaozhunshij " + 1 / hurtFps);
        if(remainingTime > 1 / hurtFps)
        {
            Debug.Log("延迟动画");
            remainingTime -= 1 / hurtFps;
            startHurtTime = Time.time;
            currentHurtIndex = (currentHurtIndex + (GetCurrentIndex() - currentHurtIndex > 4 ? 1 : -1 ) + 8)% 8; 
            //currentHurtIndex = (currentHurtIndex + (GetCurrentIndex() - currentHurtIndex > 4)
        }
    }


    public void StartHurtPlay()
    {
        isHurt = true;
        currentHurtIndex = GetCurrentIndex();
        //startHurtIndex = GetCurrentIndex();
        remainingTime = 0;
        startHurtTime = Time.time;
        initialHurtTime = Time.time;
    }

    /*
    public IEnumerator HurtCountDown()
    {
        yield return new WaitForSeconds(hurtTime);
        isHurt = false;
    }

    public IEnumerator UpdateHurtIndex()
    {
        yield return new WaitForSeconds(1 / hurtFps);
        //currentHurtIndex = currentHurtIndex + GetCurrentIndex()- currentHurtIndex > 0 ? 
    }
    */
}
