using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    [SerializeField]
    public GameObject m_BoostSpawnLimit1;
    [SerializeField]
    public GameObject m_BoostSpawnLimit2;
    [SerializeField]
    public float m_SpawnDelay;
    [SerializeField]
    public float m_RotateSpeed;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, m_RotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            float m_RandomX = Random.Range(m_BoostSpawnLimit1.transform.position.x, m_BoostSpawnLimit2.transform.position.x);
            float m_RandomY = Random.Range(m_BoostSpawnLimit1.transform.position.y, m_BoostSpawnLimit2.transform.position.y);
            float m_RandomZ = Random.Range(m_BoostSpawnLimit1.transform.position.z, m_BoostSpawnLimit2.transform.position.z);
            rb.GetComponent<Renderer>().enabled = false; 
            GetComponent<SphereCollider>().enabled = false; 
            StartCoroutine(SpawnTimer(m_SpawnDelay));

            Vector3 randomRange = new Vector3(m_RandomX, m_RandomY, m_RandomZ);
            transform.position = randomRange;
        }
    }
    void OnDrawGizmosSelected() // Para que marque la zona dentro de los dos puntos, solo se ve en la esccena
    {
        float boostSpawnX = m_BoostSpawnLimit1.transform.position.x - m_BoostSpawnLimit2.transform.position.x;
        float boostSpawnY = m_BoostSpawnLimit1.transform.position.y - m_BoostSpawnLimit2.transform.position.y;
        float boostSpawnZ = m_BoostSpawnLimit1.transform.position.z - m_BoostSpawnLimit2.transform.position.z;

        Vector3 m_Base = (m_BoostSpawnLimit1.transform.position + m_BoostSpawnLimit2.transform.position) * 0.5f;
        Vector3 m_Height = new Vector3(Mathf.Abs(boostSpawnX), Mathf.Abs(boostSpawnY), Mathf.Abs(boostSpawnZ));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(m_Base, m_Height);
    }

    IEnumerator SpawnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        rb.GetComponent<Renderer>().enabled = true;
        GetComponent<SphereCollider>().enabled = true; 
    }
}
