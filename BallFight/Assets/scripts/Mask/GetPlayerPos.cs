using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerPos : MonoBehaviour
{
    MaskController maskController;
    Canvas canvas;
    public GameObject player;
    public float time;
    public float scale;

    private void Start()
    {
        canvas = transform.GetComponentInParent<Canvas>();
        maskController = transform.GetComponent<MaskController>();

        maskController.Mask(canvas, player.GetComponent<RectTransform>(), MaskType.Circle, scale, time);
        //画布，物体，形状（圆形、矩形），起势边距，时间
    }

    private void Update()
    {
        //guideController.Guide(canvas, game.GetComponent<RectTransform>(), GuideType.Circle);
        //画布，物体，形状（圆形、矩形）

    }
}
