using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public float delayTime;
    public float stingHurt;
    public float stingDuration;
    float m_CurrentDelayTime;
    float m_CurrentStingTime;
    bool m_Triggered;
    bool m_StingOut;
    GetHurt m_GetHurt;
    public GameObject animate;

    void OnTriggerStay2D(Collider2D other)
    { 
        if(other.name=="player1"||other.name=="player2")
        {
            animate.SetActive(true);
            m_GetHurt = other.GetComponent<GetHurt>();
            if (m_GetHurt == null) return;
            if (m_StingOut)
            {
                m_GetHurt.ReceiveTerrainHurt((int)stingHurt);
                //GameObject.Find("Canvas").SendMessage("PlayAudio", "knife", SendMessageOptions.DontRequireReceiver);//受击声音
                if (!AudioManager.instance.isPlaying("beat"))
                    AudioManager.instance.Play("beat");
            }

            else m_Triggered = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentDelayTime = delayTime;
        m_CurrentStingTime = stingDuration;
        m_Triggered = false;
        m_StingOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Triggered) 
        {
            if(m_CurrentDelayTime > 0) m_CurrentDelayTime -= Time.deltaTime;
            else
            {
                m_Triggered = false;
                m_StingOut = true;
                m_CurrentDelayTime = delayTime;
                Debug.Log("针刺出击！！ヾ(✿ﾟ▽ﾟ)ノ");
                if (!AudioManager.instance.isPlaying("knife"))
                    AudioManager.instance.Play("knife");
            }
        }
        else if(m_StingOut) 
        {
            if(m_CurrentStingTime > 0) m_CurrentStingTime -= Time.deltaTime;
            else
            {
                animate.SetActive(false);
                m_StingOut = false;
                m_CurrentStingTime = stingDuration;
                Debug.Log("针刺回收|ू･ω･` )");
            }
        }
    }
}
