using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    //Spawn Delimitation
    [SerializeField]
    public GameObject m_SpawnLimit1;
    [SerializeField]
    public GameObject m_SpawnLimit2;
 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float randomX = Random.Range(m_SpawnLimit1.transform.position.x, m_SpawnLimit2.transform.position.x);
            float randomY = Random.Range(m_SpawnLimit1.transform.position.y, m_SpawnLimit2.transform.position.y);
            float randomZ = Random.Range(m_SpawnLimit1.transform.position.z, m_SpawnLimit2.transform.position.z);

            Vector3 randomRange = new Vector3(randomX, randomY, randomZ);
            transform.position = randomRange;
        }
    }
    void OnDrawGizmosSelected()
    {
        float m_XPos = m_SpawnLimit1.transform.position.x - m_SpawnLimit2.transform.position.x;
        float m_YPos = m_SpawnLimit1.transform.position.y - m_SpawnLimit2.transform.position.y;
        float m_ZPos = m_SpawnLimit1.transform.position.z - m_SpawnLimit2.transform.position.z;

        Vector3 centro = (m_SpawnLimit1.transform.position + m_SpawnLimit2.transform.position) * 0.5f;
        Vector3 tamaño = new Vector3(Mathf.Abs(m_XPos), Mathf.Abs(m_YPos), Mathf.Abs(m_ZPos));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centro, tamaño);
    }
}
