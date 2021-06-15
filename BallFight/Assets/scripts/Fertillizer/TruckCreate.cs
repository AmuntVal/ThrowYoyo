using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckCreate : MonoBehaviour
{
    /// <summary>
    /// 不做设定时，默认每隔20秒出一趟车
    /// </summary>
    public float TruckTimeInterval = 20f;

    /// <summary>
    /// 下波班车时间
    /// </summary>
    public float nextTime;
    
    
    
    
    /// <summary>
    /// 用于生成货车的预制件
    /// </summary>
    public GameObject TruckPrefab;

    /// <summary>
    /// 货车长度，用于决定肥料丢在车屁股的位置
    /// </summary>
    public float TruckLength;

    public GameObject FertilizerPrefab;
    /// <summary>
    /// 游戏中生成的临时波次货车
    /// </summary>
    public GameObject TruckInstance;
    /// <summary>
    /// 开车的时间
    /// </summary>
    private float startTime;

    public float velocity = 0.1f;
    /// <summary>
    /// 当前是否有货车
    /// </summary>
    public bool isTruckActive;

    private bool isPreparing;
    public bool isFertilizerGenerated;
    
    /// <summary>
    /// 当前随机生成的肥料掉落地点
    /// </summary>
    public Vector2 FertilizerPoint;
    
    /// <summary>
    /// 货车生成的边缘范围
    /// </summary>
    public float rectangleWidth = 12;
    public float rectangleHeight = 6;

    public Vector2 StartPosition;


    // Start is called before the first frame update
    void Start()
    {
        TruckLength = 1f;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateCreater();
        
        
        
        /*
         * 共有三种状态
         * 1. 没有货车
         * 2. 生成了货车，但肥料还未丢出
         * 3. 货车已经丢出肥料，但还到达终点
         *  
         */



        if(isTruckActive)
        {
            if(TruckInstance.transform.position.y != - StartPosition.y)
            {
                //还未到达目的地
                TruckInstance.transform.position = Vector2.Lerp(StartPosition, new Vector2(StartPosition.x,-StartPosition.y), (Time.time - startTime) * velocity);
                //TruckInstance.transform.Rotate(0.0f, 0.0f, StartPosition.y < 0f ? 180.0f + Vector2.Angle(new Vector2(1.0f, 0), -StartPosition) : 180.0f - Vector2.Angle(new Vector2(1.0f, 0), -StartPosition));
                if (Mathf.Abs( TruckInstance.transform.position.y - FertilizerPoint.y ) <= 1e-2 && !isFertilizerGenerated)
                {
                    ThrowOutFertilizer();
                }
                 
            }
            else
            //if(TruckInstance.transform.position.y == -StartPosition.y)
            {
                //已经到达
                Debug.Log("已经到了");
                DestructTruck();
            }
        }
    }

    public void CreateATruck()
    {
        if(!isTruckActive)
        {
            TruckInstance =  GameObject.Instantiate(TruckPrefab);
            //TruckInstance.transform.position = RandomPositionGenerate(Random.Range(0f, 1f));
            TruckInit();
            isTruckActive = true;
            isFertilizerGenerated = false;
        }
    }


    public Vector2 RandomPositionGenerate(float randomNumber)
    {
        if(randomNumber > 1.0f || randomNumber < 0f)
        {
            Debug.LogError("位置生成错误");
        }

        Vector2 gPosition = new Vector2();

        if (randomNumber > 0.5f)
        {
            gPosition.y = rectangleHeight / 2;
            gPosition.x = -rectangleWidth / 2 + 2 * (randomNumber - 0.5f) * rectangleWidth;
        }
        else
        {
            gPosition.y = -rectangleHeight / 2;
            gPosition.x = -rectangleWidth / 2 + 2 * randomNumber * rectangleWidth;
        }
        StartPosition = gPosition;
        Debug.Log("出生于 " + gPosition);
        return gPosition;

    }
    /// <summary>
    /// 将肥料车的所有信息初始化。
    /// </summary>
    public void TruckInit()
    {
        isPreparing = false;
        TruckInstance.transform.position = RandomPositionGenerate(Random.Range(0f, 1f));
        //TruckInstance.transform.Rotate(0.0f, 0.0f, StartPosition.y < 0f ? 180.0f + Vector2.Angle(new Vector2(1.0f, 0), -StartPosition): 180.0f - Vector2.Angle(new Vector2(1.0f, 0), -StartPosition));
        float rmpRandom = Random.Range(0.0f, 1.0f);
        //FertilizerPoint = Vector2.Lerp(StartPosition, new Vector2(StartPosition.x, - StartPosition.y ), rmpRandom);
        FertilizerPoint = new Vector2(StartPosition.x, Mathf.Lerp(StartPosition.y,- StartPosition.y, rmpRandom));
        //Debug.Log();
        startTime = Time.time;
    }

    public void ThrowOutFertilizer()
    {
        GameObject fertilizerInstance = GameObject.Instantiate(FertilizerPrefab);
        fertilizerInstance.transform.position = new Vector2 (FertilizerPoint.x, FertilizerPoint.y + (StartPosition.y < 0 ? (- TruckLength/2) :TruckLength/ 2));
        isFertilizerGenerated = true;
        Debug.Log("肥料丢出");
    }

    /// <summary>
    /// 将当前的卡车
    /// </summary>
    public void DestructTruck()
    {
        TruckInstance.gameObject.SetActive(false);
        GameObject.Destroy(TruckInstance);
        isTruckActive = false;
        isFertilizerGenerated = false;
        isPreparing = true;
    }

    private void UpdateCreater()
    {
        if(!isTruckActive && isPreparing)
        {
            nextTime = Time.time + Random.Range(0, TruckTimeInterval);
            isPreparing = false;
        }

        if(Mathf.Abs(Time.time - nextTime) < 1e-2 )
        {
            CreateATruck();
            AudioManager.instance.Play("trunk");
        }
    }

}
