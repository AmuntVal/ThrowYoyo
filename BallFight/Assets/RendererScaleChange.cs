using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RendererScaleChange : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform melonAnimation;

    public void BiggerRenderer()
    {
        Debug.Log("BiggerRenderer");
        melonAnimation.transform.DOScale(new Vector3(3, 3, 3), 2f);
    }
}
