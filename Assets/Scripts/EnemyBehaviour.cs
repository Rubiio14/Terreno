using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //Audio
    [SerializeField]
    public AudioSource m_Hitmarker;
    [SerializeField]
    AudioSource m_audioClipShoot;
    //Health
    [SerializeField]
    public HealthManager m_Healthmanager;
    
    //Muzzles
    [SerializeField]
    GameObject m_Muzzle;
    [SerializeField]
    GameObject m_Muzzle_2;
    //Enemy Movement
    public float m_RotationSpeed = 1f;
    public float m_PatrolSpeed = 5f;
    public float m_SpeedPersecution = 10f;
    public float m_VisionRange = 10f;
    public Transform[] m_Waypoints;

    private Transform m_Player;
    private int m_CurrentWaypointIndex = 0;

    //Enemy Shot
    [SerializeField]
    GameObject m_BulletPrefab;
    public float m_BulletSpeed = 10f;
    public bool m_canShoot = true;
    [SerializeField]
    GameObject m_BulletPrefab_2;
  

    

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
            Patrol();
            return;
        }

        float m_PlayerDistance = Vector3.Distance(transform.position, m_Player.position);

        if (m_PlayerDistance <= m_VisionRange)
        {
            Persecution();

        }
        else
        {
            Patrol();
        }
    }

    ///Chase the player
    void Persecution()
    {
        ///If player is in range vision ---> Persecution()
        Vector3 m_PlayerDirection = (m_Player.position - transform.position).normalized;
        transform.position += m_PlayerDirection * m_SpeedPersecution * Time.deltaTime;

        
        transform.LookAt(m_Player);

        
        if (m_canShoot)
        {
            
            Shoot();
            m_canShoot = false;
            
        } 
        
    }
    ///Patrol following a series of waypoints
    void Patrol()
    {
        if (m_Waypoints.Length == 0) return;

        Transform m_WaypointTarget = m_Waypoints[m_CurrentWaypointIndex];
        Vector3 m_WaypointDirection = (m_WaypointTarget.position - transform.position).normalized;
        transform.position += m_WaypointDirection * m_PatrolSpeed * Time.deltaTime;

        
        Quaternion targetRotation = Quaternion.LookRotation(m_WaypointDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, m_RotationSpeed * Time.deltaTime);
      
        
        if (Vector3.Distance(transform.position, m_WaypointTarget.position) < 1f)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % m_Waypoints.Length;

        }
    }
    ///Shooting class
    void Shoot()
    {
        m_audioClipShoot.Play();
        m_Muzzle.SetActive(true);
        m_Muzzle_2.SetActive(true);
        
        GameObject m_Bullet = ObjectPool.GetObject(m_BulletPrefab); 
        m_Bullet.transform.position = m_Muzzle.transform.position;
        Vector3 direction = (m_Player.position - m_Muzzle.transform.position).normalized;
        m_Bullet.GetComponent<Rigidbody>().velocity = direction * m_BulletSpeed;
        StartCoroutine(ResetShoot(m_BulletPrefab, m_Bullet, 2.0f));

        ///Second canon
        GameObject m_Bullet_2 = ObjectPool.GetObject(m_BulletPrefab_2); 
        m_Bullet_2.transform.position = m_Muzzle_2.transform.position;
        Vector3 direction_2 = (m_Player.position - m_Muzzle_2.transform.position).normalized;
        m_Bullet_2.GetComponent<Rigidbody>().velocity = direction_2 * m_BulletSpeed;
        StartCoroutine(ResetShoot(m_BulletPrefab_2, m_Bullet_2, 2.0f));
    }

    IEnumerator ResetShoot(GameObject bulletType, GameObject bullet, float time)
    {
        
        yield return new WaitForSeconds(0.5f);
        m_Muzzle.SetActive(false);
        m_Muzzle_2.SetActive(false);
        ObjectPool.RecicleObject(bulletType, bullet);
        m_canShoot = true;
    }

    ///Damage info
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bala")
        {
            m_Hitmarker.Play();
            m_Healthmanager.RecieveHit();
        }
    }

    ///Shows Enenmy Vision Range Gizmos in UnityEditor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_VisionRange);
    }
}