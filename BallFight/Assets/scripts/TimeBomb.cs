using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBomb : MonoBehaviour
{
    // Start is called before the first frame update
    public float waitTime;
    private Animation Bomb;

    void Start()
    {
        Bomb = (Animation)GameObject.FindObjectOfType(typeof(Animation)) as Animation;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("colli");
            Bomb.Play("About");
        }
    }
}