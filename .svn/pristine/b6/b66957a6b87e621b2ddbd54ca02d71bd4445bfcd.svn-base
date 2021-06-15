using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer truckRenderer;
    Transform trucktrans;
    public Sprite up;
    public Sprite down;


    void Start()
    {
        trucktrans = this.transform;
        if (this.transform.position.y < 0)
        {
            truckRenderer.sprite = up;
        }

        if (this.transform.position.y > 0)
        {
            truckRenderer.sprite = down;
        }
    }

}
