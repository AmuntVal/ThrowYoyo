using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMask : MaskBase
{
    private float r;//镂空半径
    private float scaleR;//变化之后的半径大小
    public float multiple;//镂空乘积

    private void Start()
    {
        multiple = 1;
    }

    public override void Mask(Canvas canvas, RectTransform target)
    {
        base.Mask(canvas, target);
        //计算半径
        float width = (targetCorners[3].x - targetCorners[0].x) / 2;
        float height = (targetCorners[1].y - targetCorners[0].y) / 2;
        r = Mathf.Sqrt(width * width + height * height)*multiple;
        this.material.SetFloat("_Slider", r);
    }

    public override void Mask(Canvas canvas, RectTransform target, float scale, float time)
    {
        this.Mask(canvas, target);
        scaleR = r * scale;
        this.material.SetFloat("_Slider", scaleR);

        this.time = time;
        isScaling = true;
        timer = 0;
    }

    protected override void Update()
    {
        base.Update();
        if (isScaling)
        {
            this.material.SetFloat("_Slider", Mathf.Lerp(scaleR, r, timer));
        }
    }
}
