using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBall : MonoBehaviour
{
    
    public bool workForMelon = false;
    [Tooltip("西瓜碰撞伤害基础系数")]
    public float melonDamageParameter = 1.0f;
    public bool workForWeapon = true;
    [Tooltip("毛丹碰撞伤害基础系数")]
    public float weaponDamageParameter = 1.0f;
    private Rigidbody2D rigidbody2d;
    private float m_MelonDamageScaler;
    private float m_WeaponDamageScaler;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        m_MelonDamageScaler = 1.0f;
        m_WeaponDamageScaler = 1.0f;
    }

    public int GetDamage()
    {
        int speed = 0;
        if(workForMelon)
        speed = (int) (rigidbody2d.velocity.magnitude * melonDamageParameter * m_MelonDamageScaler);
        else if(workForWeapon)
        speed = (int) (rigidbody2d.velocity.magnitude * weaponDamageParameter * m_WeaponDamageScaler);
        //Debug.Log("return damage: " + speed);
        return speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!string.Equals(other.tag, tag))
        {
            if(other.GetComponent<GetHurt>())
            {
                int damage = GetDamage();
                if(damage == 0) return;
                other.GetComponent<GetHurt>().RecieveCollisionHurt(damage, rigidbody2d.velocity,gameObject);
                Debug.Log("获得世界坐标系:" + rigidbody2d.velocity);
            }
        }
    }

    public void SetScalerDamage(float melonScaler, float weaponScaler)
    {
        m_MelonDamageScaler = melonScaler;
        m_WeaponDamageScaler = weaponScaler;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
