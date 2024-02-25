using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Behavior : MonoBehaviour
{
    
    //Spawn Point
    [SerializeField]
    GameObject m_Spawnpoint; 
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


    private Rigidbody rb;
    
    
    void Start()
    {
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
        if(collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bala_enemiga")
        {
            Debug.Log("Choca");
            gameEnding.ActivateGameOverScreen();
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
            transform.position = m_Spawnpoint.transform.position;
            transform.rotation = m_Spawnpoint.transform.rotation;
        }
    }
}