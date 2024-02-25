using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private static HealthManager instance;
    public int m_MaxHealth;
    public int m_CurrentHealth;
    // Start is called before the first frame update
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Player") && m_CurrentHealth <= 0)
        {
            Debug.Log("Game Over");
            //Poner Fin de juego
        }
    }
    public void TakeDamage(int m_Damage)
    { 
        m_CurrentHealth -= m_Damage;

        //Para que la salud no sea negativa
        m_CurrentHealth = Mathf.Max(m_CurrentHealth, 0);

        if (gameObject.CompareTag("Enemy") && m_CurrentHealth <= 0)
        {
            Debug.Log("Destrir enemigo");
            Destroy(gameObject);
        }
    }
}
