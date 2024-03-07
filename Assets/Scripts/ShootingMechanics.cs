using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class ShootingMechanics : MonoBehaviour
{
    //Audio
    [SerializeField] AudioSource m_ShootingEffect;
    [SerializeField] AudioSource m_RechargeEffect;
    //Muzzle
    [SerializeField] GameObject Muzzle;
    //Shooting & Aiming   
    [SerializeField] GameObject m_Bullet;
    [SerializeField] GameObject m_ScopeDirection;
    public float m_BulletSpeed = 10f;
    public int m_Ndisparos = 15;
    int m_NdisparosCompletos;
    public float m_CoolDownEntreBalas  = 1f;
    public float m_CoolDownEnRecgargas = 1f;
    public float m_CoolDownEntreRecarga = 2f;
    public bool m_CanShoot = true;
    //Text Mesh Pro
    [SerializeField]
    TextMeshProUGUI m_BulletsCounter;
    public GameObject m_ShootingCanvas;
    public bool m_CanRecharge = true;
    
    
    void Start()
    {
        ObjectPool.PreLoad(m_Bullet, m_Ndisparos);
        m_NdisparosCompletos = m_Ndisparos;
    }

    
    void Update()
    {
        if (m_CanRecharge)
        {
            m_BulletsCounter.text = m_Ndisparos.ToString() + " / " + m_NdisparosCompletos.ToString();
        }
         
        if (Input.GetButtonDown("Fire1") && m_CanShoot && m_Ndisparos > 0 && m_CanRecharge)
        {
            m_Ndisparos--;
            m_CanShoot = false;
            m_ShootingEffect.Play();
            Muzzle.SetActive(true);
            Shoot(m_Bullet, m_ScopeDirection);
            StartCoroutine(Shooting_Timer(m_CoolDownEntreBalas));
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            Muzzle.SetActive(false);
        }
        if (m_Ndisparos <= 0 && m_CanRecharge)
        {
            m_CanShoot = false;
            m_CanRecharge = false;
            m_BulletsCounter.text = "Reloading / " + m_NdisparosCompletos.ToString();
            m_RechargeEffect.Play();
            StartCoroutine(NoBulletsRecharge_Timer(m_CoolDownEntreRecarga));
        }
        if (Input.GetKeyDown(KeyCode.R) && m_CanRecharge && m_Ndisparos < m_NdisparosCompletos)
        {
            m_CanRecharge = false;
            m_BulletsCounter.text = "Reloading / " + m_NdisparosCompletos.ToString();
            m_CanShoot = false;
            m_RechargeEffect.Play();
            StartCoroutine(Recharge_Timer(m_CoolDownEnRecgargas));
        }
    }
    ///Shoot and Aim
    void Shoot(GameObject bulletType, GameObject originObject)
    {

        GameObject m_Bullet2 = ObjectPool.GetObject(bulletType);
        m_Bullet2.transform.position = transform.position;

        ///Aiming Direction
        Vector3 direction = originObject.transform.forward;

        ///Apply Bullet Speed
        m_Bullet2.GetComponent<Rigidbody>().velocity = direction * m_BulletSpeed;

       
        StartCoroutine(RecicleObject(bulletType, m_Bullet2, 2.0f));
    }

    IEnumerator RecicleObject(GameObject bulletType, GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.RecicleObject(bulletType, bullet);
    }
    IEnumerator Shooting_Timer(float m_CoolDownEntreBalas)
    {
        yield return new WaitForSeconds(m_CoolDownEntreBalas);
        m_CanShoot = true;
    }

    IEnumerator NoBulletsRecharge_Timer(float m_CoolDownEntreRecarga)
    {
        yield return new WaitForSeconds(m_CoolDownEntreRecarga);
        m_CanShoot = true;
        m_CanRecharge = true;
        m_Ndisparos = m_NdisparosCompletos;
       
    }

    IEnumerator Recharge_Timer(float m_CoolDownEnRecgargas)
    {
        yield return new WaitForSeconds(m_CoolDownEnRecgargas);
        m_Ndisparos = m_NdisparosCompletos;
        m_CanShoot = true;
        m_CanRecharge = true;
       
    }
}