using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScaleControl : MonoBehaviour
{
    public float newScale = 0.05f;
    [Tooltip("西瓜变大后的持续时间")]
    public float duringTime = 6.0f;
    [Tooltip("西瓜变大和变小时动画的持续时间")]
    public float cartoonDuringTime = 0.5f;
     [Tooltip("西瓜放大后的伤害加成系数")]
    public float m_MelonDamageScaler = 1.2f;
     [Tooltip("毛丹放大后的加成系数")]
    public float m_WeaponDamageScaler = 1.2f;
    public ScaleControl scaleControl;
    float m_CurrentDuringTime;
    float m_CurrentScaleDuringTime;
    bool m_ScaleUpStatus;
    bool m_ScaleDownStatus;
    float m_ScaleChangeSpeed;
    float m_InitialScale;
    AttackBall attackBall;

    // Start is called before the first frame update
    void Start()
    {
        //m_InitialScale = transform.localScale.x;
        m_ScaleChangeSpeed = newScale;
        attackBall = GetComponent<AttackBall>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_CurrentDuringTime > 0)
        {
            m_CurrentDuringTime = m_CurrentDuringTime - Time.deltaTime;
            if(m_CurrentDuringTime <= 0) m_ScaleDownStatus = true;
            else return;
        }
        if(m_ScaleUpStatus) ScaleUp();
        else if(m_ScaleDownStatus) ScaleDown();
    }

    public void ChangeScale()
    {
        if(scaleControl != null) scaleControl.ChangeScale();
        Debug.Log("变大");
        //播放笑的表情
        m_ScaleUpStatus = true;
        m_CurrentScaleDuringTime = cartoonDuringTime;
    }

    void ScaleUp()
    {
        m_CurrentScaleDuringTime -= Time.deltaTime;
        transform.localScale += new Vector3(m_ScaleChangeSpeed * Time.deltaTime, m_ScaleChangeSpeed * Time.deltaTime, m_ScaleChangeSpeed * Time.deltaTime);
        if(m_CurrentScaleDuringTime < 0) 
        {
            m_ScaleUpStatus = false;
            m_CurrentDuringTime = duringTime;
            m_CurrentScaleDuringTime = cartoonDuringTime;
            GameObject.Find("Canvas").SendMessage("PlayAudio", "feiti", SendMessageOptions.DontRequireReceiver);//肥料声音
            Debug.Log("变大了！");
            if(attackBall!=null)
            attackBall.SetScalerDamage(m_MelonDamageScaler, m_WeaponDamageScaler);
        }
    }

    void ScaleDown()
    {
        m_CurrentScaleDuringTime -= Time.deltaTime;
        transform.localScale -= new Vector3(m_ScaleChangeSpeed * Time.deltaTime, m_ScaleChangeSpeed * Time.deltaTime, m_ScaleChangeSpeed * Time.deltaTime);
        if(m_CurrentScaleDuringTime < 0) 
        {
            m_ScaleDownStatus = false;
            Debug.Log("变小了！");
            //播放正常表情
            if (attackBall!=null)
            attackBall.SetScalerDamage(1, 1);
        }
    }

}
