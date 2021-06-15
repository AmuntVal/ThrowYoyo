using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public float maxEnergyBar = 100.0f;
    public float decayTime = 10.0f;
    //能量槽技能结束后，逻辑会比表现晚结算的时间
    public float delay = 0.01f;
    float m_CurrentDecayTime;
    float m_CurrentEnergyBar;
    bool m_DecayStatus;
    PlayerAction playerAction;
    public Image energyBarImage;
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentEnergyBar = 0;
        m_DecayStatus = false;
        playerAction = GetComponent<PlayerAction>();
        energyBarImage.fillAmount = m_CurrentEnergyBar / 100;
    }

    void Update()
    {
        DecayEnergy();
    }

    public void AddEnergy(float n)
    {
        Debug.Log("增加能量条:  " + n);
        m_CurrentEnergyBar += n;
        if(m_CurrentEnergyBar >= maxEnergyBar)
        {
            m_CurrentEnergyBar = maxEnergyBar;
            m_DecayStatus = true;
            m_CurrentDecayTime = decayTime;
            playerAction.StartWuShuang(decayTime + delay);
        }
        energyBarImage.fillAmount = m_CurrentEnergyBar / 100;
    }

    void DecayEnergy()
    {
        if(m_DecayStatus)
        {
            m_CurrentEnergyBar = m_CurrentDecayTime / decayTime * 100;
            energyBarImage.fillAmount = m_CurrentEnergyBar / 100;
            m_CurrentDecayTime -=  Time.deltaTime;
            if(m_CurrentDecayTime < 0) 
            {
                m_CurrentDecayTime = 0;
                m_DecayStatus = false;
                playerAction.EndWushuang();
            }
        }
    }
}
