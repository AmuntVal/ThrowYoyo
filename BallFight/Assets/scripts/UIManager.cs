using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Linq;
using UnityEngine.SceneManagement;
using CarterGames;
using CarterGames.Assets.AudioManager;


public class UIManager : MonoBehaviour
{
    static public bool isGame = true;
    AudioManager audioManager;//音频控制器
    GameObject backGround;
    SpriteRenderer m_AnimatorSprite;
    public Sprite[] picLeft;//左边看台弹出的所有图
    public Sprite[] picRight;//右边看台弹出的所有图
    GameObject FrameL;
    GameObject FrameR;
    SpriteRenderer picL;
    SpriteRenderer picR;
    public float ReactTime = 1;
    GameObject PlayerRed;//红西瓜的表情物体
    GameObject PlayerYellow;//黄西瓜的表情物体
    public Sprite[] faceRed;//红西瓜的所有表情
    public Sprite[] faceYellow;//黄西瓜的所有表情
    SpriteRenderer picRed;
    SpriteRenderer picYellow;

    Color a;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioController").GetComponent<AudioManager>();//获得音频控制器
        FrameL = GameObject.Find("dialogueframe1");
        FrameR = GameObject.Find("dialogueframe2");
        picL = GameObject.Find("dialogueframe1/bubbleLeft").GetComponent<SpriteRenderer>();
        picR = GameObject.Find("dialogueframe2/bubbleRight").GetComponent<SpriteRenderer>();
        //观众看台的框与表情
        //picRed = PlayerRed.GetComponent<Sprite>();
        PlayerRed = GameObject.Find("RedMelonRenderer/FaceRed");
        PlayerYellow = GameObject.Find("YellowMelonRenderer/FaceYellow");
        picRed = PlayerRed.GetComponent<SpriteRenderer>();
        picYellow = PlayerYellow.GetComponent<SpriteRenderer>();
        //西瓜脸上的表情
        

    }
    public void ReactionLeft(int i)
    {
        if (picLeft[i])
        {
            picL.sprite = picLeft[i];
            FrameL.transform.localScale = Vector3.one;
        }
        Invoke("DisLeft", ReactTime);
    }
    public void ReactionRight(int i)
    {
        if (picRight[i])
        {
            picR.sprite = picRight[i];
            FrameR.transform.localScale = Vector3.one;
        }
        Invoke("DisRight", ReactTime);
    }
    void DisLeft()
    {
        FrameL.transform.localScale = Vector3.zero;
    }

    void DisRight()
    {
        FrameR.transform.localScale = Vector3.zero;
    }

    public void FaceChangeRed(int i)
    {
        if (faceRed[i])
        {
            picRed.sprite = faceRed[i];
        }
    }
    public void FaceChangeYellow(int i)
    {
        if (faceYellow[i])
        {
            picYellow.sprite = faceYellow[i];
        }
    }

    void Test()
    {
        ReactionRight(0);
    }

    public void PlayAudio(string str)
    {
        audioManager.Play(str);
    }
    // Update is called once per frame

}
