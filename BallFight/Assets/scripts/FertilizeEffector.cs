using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertilizeEffector : MonoBehaviour
{
    ScaleControl scaleControl;
    void OnTriggerEnter2D(Collider2D other)
    {
        scaleControl = other.GetComponent<ScaleControl>();
        if(scaleControl) 
        {
            scaleControl.ChangeScale();
            Destroy(this.gameObject);
        }
    }
}
