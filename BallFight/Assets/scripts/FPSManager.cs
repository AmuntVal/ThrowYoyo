using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSManager : MonoBehaviour
{
    /// <summary>
    /// 是否在GUI上显示帧数
    /// </summary>
    public bool FPSUIOn = false;

    /// <summary>
    /// 正常情况下的帧率
    /// </summary>
    public int normalFPS;
    /// <summary>
    /// 闪避成功时的帧率
    /// </summary>
    public int lowFPS;

    /// <summary>
    /// 低帧率的持续时间
    /// </summary>
    public float lowFPSPeriodLength;

    /// <summary>
    /// 当前是否处于低帧数情况
    /// </summary>
    private bool isLow;

    private float lowFPSStartTime = 0f;

    /// <summary>
    /// 上一次更新帧率的时间
    /// </summary>
    private float lastUpdatedShowTime = 0f;


    /// <summary>
    /// 更新显示帧率的时间间隔
    /// </summary>
    private readonly float updateTime = 0.05f;

    /// <summary>
    /// 帧数
    /// </summary>
    private int frames = 0;

    private float frameDeltaTime = 0;

    private float FPS = 0;
    private Rect fps, deltaTime;
    private GUIStyle guiStyle = new GUIStyle();


    private void Awake()
    {
        Application.targetFrameRate = 100;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
        lastUpdatedShowTime = Time.realtimeSinceStartup;
        fps = new Rect(0, 0, 100, 100);
        deltaTime = new Rect(0, 100, 100, 100);
        guiStyle.fontSize = 100;
        guiStyle.normal.textColor = Color.blue;


    }

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = isLow ? lowFPS : normalFPS;
        frames++;
        if (Time.realtimeSinceStartup - lastUpdatedShowTime >= updateTime)
        {
            FPS = frames / (Time.realtimeSinceStartup - lastUpdatedShowTime);
            frameDeltaTime = 1 / FPS;
            frames = 0;
            lastUpdatedShowTime = Time.realtimeSinceStartup;
        }

        UpdateFPSState();
    }

    private void OnGUI()
    {
        if(FPSUIOn)
        {
            GUI.Label(fps, "FPS:" + (int) FPS, guiStyle);
            //GUI.Label(deltaTime, "DeltaTime: " + frameDeltaTime, guiStyle);
        }


    }

    private void UpdateFPSState()
    {
        if(isLow && Time.realtimeSinceStartup - lowFPSStartTime > lowFPSPeriodLength)
        {
            isLow = false;

        }    
    }

    /// <summary>
    /// 公开方法，开始降帧
    /// </summary>
    public void StartLowFPS()
    {
        if(!isLow)
        {
            isLow = true;
            lowFPSStartTime = Time.realtimeSinceStartup;
        }
    }

    /// <summary>
    /// 公开方法，toggle开启或是关闭FPS显示
    /// </summary>
    public void switchOnFPSUI()
    {
        FPSUIOn = !FPSUIOn;
    }
}
