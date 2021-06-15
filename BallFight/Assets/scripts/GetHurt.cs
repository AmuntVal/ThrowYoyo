using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetHurt : MonoBehaviour
{
    public string FaceChange;
    public string ReactChange;
    public string ReactEnemy;
    public GameObject losePanel;
    public TMP_Text m_UIText;
    public Image m_Image;
    public SpriteRenderer white;
    public float collisionImmortalTime = 0.5f;
    public float terrainImmortalTime = 1.0f;
    float m_CurrentCollisionImmortalTime;
    float m_CurrentTerrainImmortalTime;
    public int initialBlood = 100;
    public float hitFlyRatioForBall = 0.3f;
    public float hitFlyRatioForMD = 0.5f;
    public float hitFlyDurationForBall = 1.5f;
    public float hitFlyDurationForMD = 1.5f;
    public SpriteRenderer shield;
    
    int m_CurrentBlood;
    PlayerAction playerAction;
    void Start()
    {
        m_CurrentBlood = initialBlood;
        m_CurrentCollisionImmortalTime = 0;
        m_CurrentTerrainImmortalTime = 0;
        playerAction = GetComponent<PlayerAction>();
        m_Image.fillAmount = m_CurrentBlood / initialBlood;
    }

    public void RecieveCollisionHurt(int damageInt, Vector3 attackOrigin,GameObject attackball)

    {
        if (m_CurrentCollisionImmortalTime > 0) return;
        m_CurrentCollisionImmortalTime = collisionImmortalTime;    //由于碰撞判定是在短时间内连续发生多次，因此在受到伤害后需要有短暂的无敌状态
        if (playerAction.GetShieldStatus())
        {
            Debug.Log("闪避成功！");
            if (!AudioManager.instance.isPlaying("ferti"))
                AudioManager.instance.Play("ferti");
            GameObject.Find("Canvas").SendMessage(FaceChange, 1, SendMessageOptions.DontRequireReceiver);//红西瓜切换成第0个表情，把这一句直接放到其他脚本里就行
            //播放笑的表情
            shield.color = new Color(1, 1, 1, 1);
            playerAction.SetShieldSuccessStatus(true);
            EnergyBar energyBar = GetComponent<EnergyBar>();
            attackball.transform.DOMove(-attackOrigin * hitFlyRatioForMD, hitFlyDurationForMD, false);
            //attackball.GetComponent<Rigidbody2D>().AddForce(-attackOrigin * 2, ForceMode2D.Impulse);
            Debug.Log("毛丹被弹开！");
            if (energyBar) energyBar.AddEnergy(playerAction.energyFillPerShield);
        }
        else
        {
            m_CurrentBlood = m_CurrentBlood - damageInt;
            if (!AudioManager.instance.isPlaying("beat"))
                AudioManager.instance.Play("beat");

            GameObject.Find("Canvas").SendMessage(FaceChange, 2, SendMessageOptions.DontRequireReceiver);//播放被打的表情0.5秒
            Invoke("FaceNormal", 0.5f);
            //有0.3的概率，己方看台播放愤怒表情，有0.3的概率，对方看台播放喜悦表情（不互斥）
            int a = Random.Range(0, 3);
            if(a==0) GameObject.Find("Canvas").SendMessage(ReactChange, 1, SendMessageOptions.DontRequireReceiver);//己方看台
            a = Random.Range(0, 3);
            if (a== 0) GameObject.Find("Canvas").SendMessage(ReactEnemy, 0, SendMessageOptions.DontRequireReceiver);//敌方看台
            //Vector3 moveDirection = transform.position - attackOrigin;
            transform.DOMove(attackOrigin * hitFlyRatioForBall, hitFlyDurationForBall, false);
            //GetComponent<Rigidbody2D>().AddForce(attackOrigin, ForceMode2D.Impulse);
            sendMassageToUI(m_CurrentBlood);
            WhiteBlink();
        }
    }

    public void WhiteBlink()
    {
        white.DOFade(0.5f, 0.1f).SetLoops(5, LoopType.Yoyo);
        StartCoroutine(TurnTransparent(0.6f));
    }

    IEnumerator TurnTransparent(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        white.DOFade(0f, 0.01f).SetEase(Ease.InBounce);
    }

    public void ReceiveTerrainHurt(int damageHurt)
    {
        if (m_CurrentTerrainImmortalTime > 0) return;
        m_CurrentTerrainImmortalTime = terrainImmortalTime;    //由于碰撞判定是在短时间内连续发生多次，因此在受到伤害后需要有短暂的无敌状态
        m_CurrentBlood = m_CurrentBlood - damageHurt;
        sendMassageToUI(m_CurrentBlood);
        WhiteBlink();
    }

    public void SetImmortalTime(float terrainImmortalT, float collisionImmortalT)
    {
        m_CurrentTerrainImmortalTime = terrainImmortalT;
        m_CurrentCollisionImmortalTime = collisionImmortalT;
    }

    void sendMassageToUI(int damageInt)
    {
        //if (UIManager.isGame == false) return;
        //m_UIText.text = m_CurrentBlood.ToString();
        if(m_CurrentBlood >0)
        m_Image.fillAmount= m_CurrentBlood * 1.0f / 100;
        else
        {
            if(m_CurrentBlood > -99999)
            {
                Debug.Log("you are dead.");
                m_Image.fillAmount = 0;
                //UIManager.isGame = false;
                playerAction.PlayDead();
                m_CurrentBlood = -100000;
                Invoke("Lose", 2.0f);//这里最好先暂停一下伤害记录
            }
        }
    }

    void Update()
    {
        if (m_CurrentCollisionImmortalTime > 0) m_CurrentCollisionImmortalTime = m_CurrentCollisionImmortalTime - Time.deltaTime;
        if (m_CurrentTerrainImmortalTime > 0) m_CurrentTerrainImmortalTime -= Time.deltaTime;
    }

    void FaceNormal()
    {
        GameObject.Find("Canvas").SendMessage(FaceChange, 0, SendMessageOptions.DontRequireReceiver);
    }

    void Lose()
    {
        //GameObject.Find("Canvas").SendMessage("PlayAudio", "end", SendMessageOptions.DontRequireReceiver);
        AudioManager.instance.Play("end");
        losePanel.SetActive(true);
        Invoke("LoadEndScene", 3.0f);
    }

    void LoadEndScene()
    {
        SceneManager.LoadScene("UIEnd");
    }

}
