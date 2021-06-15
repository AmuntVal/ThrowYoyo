using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MaskBase : MonoBehaviour
{
    protected Material material;//材质
    protected Vector3 center;//镂空中心
    protected RectTransform target;
    protected Vector3[] targetCorners = new Vector3[4];//引导目标的边界

    protected float timer;
    protected float time;
    protected bool isScaling;

    protected virtual void Update()
    {
        if (isScaling)
        {
            timer += Time.deltaTime / time;
            if (timer >= 1)
            {
                timer = 0;
                isScaling = false;
            }
        }
    }

    public virtual void Mask(Canvas canvas, RectTransform target)
    {
        material = GetComponent<Image>().material;
        this.target = target;
        //获取四个点的世界坐标
        target.GetWorldCorners(targetCorners);
        //世界坐标转屏幕坐标
        for (int i = 0; i < targetCorners.Length; i++)
        {
            targetCorners[i] = WorldToScreenPoints(canvas, targetCorners[i]);
        }
        //计算中心点
        center.x = targetCorners[0].x + (targetCorners[3].x - targetCorners[0].x) / 2;
        center.y = targetCorners[0].y + (targetCorners[1].y - targetCorners[0].y) / 2;
        //设置中心点
        material.SetVector("_Center", center);
    }

    public virtual void Mask(Canvas canvas, RectTransform target, float scale, float time)
    {

    }

    public Vector2 WorldToScreenPoints(Canvas canvas, Vector3 world)
    {
        //把世界转屏幕
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, world);
        Vector2 localPoint;
        //屏幕转局部坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPoint, canvas.worldCamera, out localPoint);
        return localPoint;
    }
}
