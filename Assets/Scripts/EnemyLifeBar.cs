using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyLifeBar : MonoBehaviour
{
    //Health
    public float m_MaxHealth;
    public float m_CurrentHealth;
    //LifeBar
    public Image m_LifeBarImage;
    public GameObject m_camera;
    //DamageSystem
    public HealthManager m_HealthManager;
 

    void Start()
    {
        m_MaxHealth = m_HealthManager.m_HitsToDie;
        m_CurrentHealth = m_MaxHealth;
    }

    void Update()
    {
        LifeChecker();
        /*LifeBar follows camera*/
        if (gameObject.tag == "Enemy")
        {
            transform.forward = m_camera.transform.forward;
        }        
    }
    /*Decrement Life if is necesary*/
    public void LifeChecker()
    {
        m_LifeBarImage.fillAmount = m_CurrentHealth / m_MaxHealth;
    }
}
