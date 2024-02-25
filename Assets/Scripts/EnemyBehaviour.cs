using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject Muzzle;
    [SerializeField]
    GameObject Muzzle_2;
    public float m_RotationSpeed = 1f;
    public float m_PatrolSpeed = 5f;
    public float m_SpeedPersecution = 10f;
    public float m_VisionRange = 10f;
    public Transform[] m_Waypoints;

    private Transform m_Player;
    private int m_CurrentWaypointIndex = 0;

    [SerializeField]
    GameObject m_BulletPrefab;
    public float m_BulletSpeed = 10f;
    public bool m_canShoot = true;
    [SerializeField]
    GameObject m_BulletPrefab_2;
  

    [SerializeField]
    public ParticleSystem m_Explosion;

    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        ObjectPool.PreLoad(m_BulletPrefab, 5); // Preload de balas en el Object Pool
        ObjectPool.PreLoad(m_BulletPrefab_2, 5);
    }

    void Update()
    {
        if (m_Player == null)
        {
            return;
        }

        float m_PlayerDistance = Vector3.Distance(transform.position, m_Player.position);

        if (m_PlayerDistance <= m_VisionRange)
        {
            Persecution();

        }
        else
        {
            // Si el jugador no está en el rango de visión, mueve hacia el waypoint
            Patrol();
        }
    }

    void Persecution()
    {
        // Si el jugador está dentro del rango de visión, persigue al jugador
        Vector3 m_PlayerDirection = (m_Player.position - transform.position).normalized;
        transform.position += m_PlayerDirection * m_SpeedPersecution * Time.deltaTime;

        // Rotar hacia el jugador
        transform.LookAt(m_Player);

        // Disparar si se puede
        if (m_canShoot)
        {
            
            Shoot();
            m_canShoot = false;
            
        } 
        
    }

    void Patrol()
    {
        if (m_Waypoints.Length == 0) return;

        Transform m_WaypointTarget = m_Waypoints[m_CurrentWaypointIndex];
        Vector3 m_WaypointDirection = (m_WaypointTarget.position - transform.position).normalized;
        transform.position += m_WaypointDirection * m_PatrolSpeed * Time.deltaTime;

        // Rotar hacia el waypoint
        Quaternion targetRotation = Quaternion.LookRotation(m_WaypointDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, m_RotationSpeed * Time.deltaTime);
      
        // Si la nave alcanza el waypoint actual, pasa al siguiente waypoint
        if (Vector3.Distance(transform.position, m_WaypointTarget.position) < 1f)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % m_Waypoints.Length;

        }
    }
    void Shoot()
    {
        Muzzle.SetActive(true);
        Muzzle_2.SetActive(true);
        
        GameObject m_Bullet = ObjectPool.GetObject(m_BulletPrefab); // Obtener una bala del Object Pool
        m_Bullet.transform.position = Muzzle.transform.position;
        Vector3 direction = (m_Player.position - Muzzle.transform.position).normalized;
        m_Bullet.GetComponent<Rigidbody>().velocity = direction * m_BulletSpeed;
        StartCoroutine(ResetShoot(m_BulletPrefab, m_Bullet, 2.0f));
        //CAñon_2
        GameObject m_Bullet_2 = ObjectPool.GetObject(m_BulletPrefab_2); // Obtener una bala del Object Pool
        m_Bullet_2.transform.position = Muzzle_2.transform.position;
        Vector3 direction_2 = (m_Player.position - Muzzle_2.transform.position).normalized;
        m_Bullet_2.GetComponent<Rigidbody>().velocity = direction_2 * m_BulletSpeed;
        StartCoroutine(ResetShoot(m_BulletPrefab_2, m_Bullet_2, 2.0f));
    }

    IEnumerator ResetShoot(GameObject bulletType, GameObject bullet, float time)
    {
        
        yield return new WaitForSeconds(0.5f); // Tiempo de espera entre disparos
        Muzzle.SetActive(false);
        Muzzle_2.SetActive(false);
        ObjectPool.RecicleObject(bulletType, bullet);
        m_canShoot = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bala")
        {

            m_Explosion.transform.position = transform.position;
            m_Explosion.Play();
            Destroy(gameObject);
        }
    }


    void OnDrawGizmosSelected()
    {
        // Dibujar el rango de visión del enemigo en el editor de Unity
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_VisionRange);
    }
}