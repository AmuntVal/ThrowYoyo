using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MaskType
{
    Circle,//圆形
}

[RequireComponent(typeof(CircleMask))]
public class MaskController : MonoBehaviour, ICanvasRaycastFilter
{
    private CircleMask circleMask;
    public Material circleMat;
    private Image mask;
    private RectTransform target;
    private void Awake()
    {
        mask = transform.GetComponent<Image>();
        if (mask == null) { throw new System.Exception("mask初始化失败"); }
        if (circleMat == null) { throw new System.Exception("材质未赋值"); }
        circleMask = transform.GetComponent<CircleMask>();
    }
    public void Mask(Canvas canvas, RectTransform target,MaskType maskType)
    {
        this.target = target;
        mask.material = circleMat;
        switch (maskType)
        {
            case MaskType.Circle:
                mask.material = circleMat;
                circleMask.Mask(canvas, target);
                break;
            default:
                break;
        }
    }

    public void Mask(Canvas canvas,RectTransform target, MaskType maskType, float scale,float time)
    {
        this.target = target;
        switch (maskType)
        {
            case MaskType.Circle:
                mask.material = circleMat;
                circleMask.Mask(canvas, target, scale, time);
                break;
            default:
                break;
        }
    }
        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (target == null) { return true; }
        return !RectTransformUtility.RectangleContainsScreenPoint(target, sp);
    }
}