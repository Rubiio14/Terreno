using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    
    private static ObjectPool instance;

    
    static Dictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>>();
   
    static Dictionary<int, GameObject> parents = new Dictionary<int, GameObject>();

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

    ///Preload objects on the ObjectPool
    public static void PreLoad(GameObject objectToPool, int amount)
    { 
        
        int id = objectToPool.GetInstanceID();

        
        GameObject parent = new GameObject();
        
        parent.name = objectToPool.name + " Pool";
        
        parents.Add(id, parent);

        
        pool.Add(id, new Queue<GameObject>());

        for (int i = 0; i < amount; i++)
        {
            CreateObject(objectToPool);
        }
    }

    ///Create an object on the ObjectPool
    static void CreateObject(GameObject objectToPool)
    {
        
        int id = objectToPool.GetInstanceID();
        
        GameObject go = Instantiate(objectToPool) as GameObject;
        
        go.transform.SetParent(GetParent(id).transform);
        
        go.SetActive(false);
        
        pool[id].Enqueue(go);
        
    }

    
    static GameObject GetParent(int parentID)
    {
        
        GameObject parent;
        parents.TryGetValue(parentID, out parent);
        return parent;
    }

    ///Returns the object
    public static GameObject GetObject(GameObject objectToPool)
    {
        
        int id = objectToPool.GetInstanceID();

        
        if (pool[id].Count == 0)
        {
            CreateObject(objectToPool);
        }
        
        GameObject go = pool[id].Dequeue();
        go.SetActive(true);

        return go;

    }

    ///Recycle the object
    public static void RecicleObject(GameObject objectToPool, GameObject objectToRecicle)
    { 
        
        int id = objectToPool.GetInstanceID();
        
        pool[id].Enqueue(objectToRecicle);
        objectToRecicle.SetActive(false);
    }
    ///Clears the objects from the pool
    public static void ClearPool()
    {
        foreach (var m_FirstDictionary in pool)
        {
            Queue<GameObject> queue = m_FirstDictionary.Value;
            foreach (GameObject m_Obj in queue)
            {
                Destroy(m_Obj);
            }
            queue.Clear();
        }

        pool.Clear();

        foreach (var m_SecondDictionary in parents)
        {
            GameObject parent = m_SecondDictionary.Value;
            Destroy(parent);
        }

        parents.Clear();
    }


}
