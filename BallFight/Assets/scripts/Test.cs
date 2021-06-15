using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using UnityEngine.SceneManagement;
using CarterGames;
using CarterGames.Assets.AudioManager;

public class Test : MonoBehaviour
{
    AudioManager audioManager;//音频控制器
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioController").GetComponent<AudioManager>();//获得音频控制器
        //GameObject.Find("Canvas").SendMessage("PlayAudio", "haha", SendMessageOptions.DontRequireReceiver);
    }
    public void ReactRight() //弹出右边看台表情的方法
    {
        GameObject.Find("Canvas").SendMessage("ReactionRight", 0, SendMessageOptions.DontRequireReceiver);//弹出右边第0个表情
        //GameObject.Find("Canvas").SendMessage("ReactionLeft", 1, SendMessageOptions.DontRequireReceiver);//弹出左边第1个表情
    }
    public void FaceRed() //切换西瓜表情的方法示例
    {
        GameObject.Find("Canvas").SendMessage("FaceChangeRed", 1, SendMessageOptions.DontRequireReceiver);//红西瓜切换成第0个表情，把这一句直接放到其他脚本里就行
        //GameObject.Find("Canvas").SendMessage("FaceChangeYellow", 1, SendMessageOptions.DontRequireReceiver);//黄西瓜切换成第1个表情
    }
    public void PlayAudio()
    {
        audioManager.Play("haha");
    }
}
