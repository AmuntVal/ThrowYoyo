using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinTrigger : MonoBehaviour
{

    public float flyHurt = 2.0f;
    GetHurt m_GetHurt;
    bool m_AudioPlayMark;

    private void Start()
    {
        m_AudioPlayMark = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        m_GetHurt = other.GetComponent<GetHurt>();
        if (m_GetHurt)
        {
           //GameObject.Find("Canvas").SendMessage("PlayAudio", "fly", SendMessageOptions.DontRequireReceiver);
           if (!AudioManager.instance.isPlaying("fly"))
                AudioManager.instance.Play("fly");

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("出来了");
        m_GetHurt = other.GetComponent<GetHurt>();
        
        if (m_GetHurt)
        {
            Debug.Log("此处停止。");
            //GameObject.Find("Canvas").SendMessage("StopAudio", "fly", SendMessageOptions.DontRequireReceiver);
            AudioManager.instance.Stop("fly");

        }
    }

    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D other)
    {
        m_GetHurt = other.GetComponent<GetHurt>();
        if(m_GetHurt)
        {
            m_GetHurt.ReceiveTerrainHurt((int)flyHurt);
            //GameObject.Find("Canvas").SendMessage("PlayAudio", "fly", SendMessageOptions.DontRequireReceiver);
            
        }
    }
}
