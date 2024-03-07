using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Behavior : MonoBehaviour
{
    //Death
    [SerializeField]
    AudioSource m_GameOver;
    [SerializeField]
    AudioSource m_AmbientSound;
    //Life
    [SerializeField]
    public HealthManager m_Health;
    
    //Explosion
    public GameObject m_Explosion;
    public int m_Nexplotions;
    [SerializeField]
    AudioSource m_audioClipExplosion;
    //[SerializeField]
    //public ParticleSystem m_Explosion;
 
    //Acceleration
    public float m_NormalSpeed = 50f;
    public float m_Speed = 50f;
    public float m_Vmax = 5f;
    public float m_AccSpeed = 10f;
    public float m_deSpeed = 15f;
    public bool m_CanAccelerate = false;

    //Mouse
    public Vector2 m_Rotation;
    public float m_Sens = .5f;
    public Vector3 m_Direction;

    //GameEnding
    public GameEnding gameEnding;
    public GameObject player;

    //PowerUP
    public float m_CurrentSpeed;
    public float m_BoostSpeed;
    public float m_BoostTimer;
    public AudioSource m_PowerUpSound;
    

    private Rigidbody rb;
    
    
    void Start()
    {
        ObjectPool.PreLoad(m_Explosion, m_Nexplotions);
        rb = GetComponent<Rigidbody>(); 
        Cursor.lockState = CursorLockMode.Locked;
       
    }
    void Update()
    {
        MouseLook();
        Movement();
        AccelerationGestioner();
       
        
    }
    void MouseLook()
    {
        
        m_Rotation.x += Input.GetAxis("Mouse X") * m_Sens;
        m_Rotation.y += Input.GetAxis("Mouse Y") * m_Sens;
        transform.localRotation = Quaternion.Euler(-m_Rotation.y, m_Rotation.x, 0f);
        
    }
   
    
    void Movement()
    {
        transform.position += transform.forward * m_Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            m_CanAccelerate = true;
        }else
        {
            m_CanAccelerate = false;
        }
        
    }
    void AccelerationGestioner()
    {
        if (m_CanAccelerate)
        {
            if(m_Speed < m_Vmax)
            {
                Acceleration();
            }
        }
        else
        {
            if(m_Speed < m_Vmax)
            {
                Deceleration();
            }
        }
    }
    void Acceleration()
    {
        m_Speed = Mathf.Lerp(m_Speed, m_Vmax , m_AccSpeed * Time.deltaTime);
    }
    void Deceleration()
    {
        m_Speed = Mathf.Lerp(m_Speed, m_NormalSpeed , m_deSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "Enemy")
        {
            m_AmbientSound.Stop();
            m_GameOver.Play();
            m_audioClipExplosion.Play();
            m_Explosion.transform.position = transform.position;
            GameObject m_Explotion2 = ObjectPool.GetObject(m_Explosion);
            m_Explotion2.transform.position = transform.position;
            m_Explotion2.GetComponent<ParticleSystem>().Play();
            StartCoroutine(Explotion_Timer(m_Explosion, m_Explotion2, 2.0f));
            gameEnding.ActivateGameOverScreen();
            Destroy(gameObject);
            Cursor.lockState = CursorLockMode.None; 
            
        }
        if (collision.gameObject.tag == "Bala_enemiga")
        {
            m_Health.RecieveHit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boost")
        {
            m_PowerUpSound.Play();
            m_CurrentSpeed = m_Speed;
            m_Speed = m_Vmax + m_BoostSpeed;
            StartCoroutine(PowerUpTimer(m_BoostTimer));
        }
    }
    IEnumerator PowerUpTimer(float time)
    {
        yield return new WaitForSeconds(time);
        m_Speed = m_CurrentSpeed;
    }
    IEnumerator Explotion_Timer(GameObject m_Explosion, GameObject m_Explotion2, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.RecicleObject(m_Explosion, m_Explotion2);
        ObjectPool.ClearPool();
    }

}