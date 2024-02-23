using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //Creo una insatcia de la calse para evitar que se repita
    private static ObjectPool instance;

    //Creo un Diccionario Con la cola de objetos [clave = nºdel objeto/almacenamiento de objetos en la cola]
    static Dictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>>();
    //Creo un Diccionario para que en el Hierachy todos los objetos de la cola esten ordenados[clave = Id del objeto/nºdel objeto]
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

    //clase que precarga el nº de objetos que necesitamos (ObjetoPrimigeneo, cantidad de objetos a crear)
    public static void PreLoad(GameObject objectToPool, int amount)
    { 
        //Almacenamos en id el orden en el que se creó el objeto, lo utilizremos como identificador
        int id = objectToPool.GetInstanceID();

        //Creamos el objeto padre para el diccionario que ordena el Hierachy
        GameObject parent = new GameObject();
        //Sirve para cambiar el nombre del objeto que se clona(Si se llama bala se almacena como balaPool)
        parent.name = objectToPool.name + " Pool";
        //Los Añado al Diccionario de Hierachy
        parents.Add(id, parent);

        //Creo una nueva cola en el Diccionario donde voy a almacenar realmente el objeto
        pool.Add(id, new Queue<GameObject>());

        for (int i = 0; i < amount; i++)
        {
            CreateObject(objectToPool);
        }
    }

    //El paramentro que se le pasa es el ObjetoPrimigeneo, ya que clonaremos el resto de objetos de ahí 
    static void CreateObject(GameObject objectToPool)
    {
        //Instancio el Id (Se hace para obtener el identificador del objeto)
        int id = objectToPool.GetInstanceID();
        //creo el objetoCloando desde el ObjetoPrimigeneo
        GameObject go = Instantiate(objectToPool) as GameObject;
        //Le paso el id del padre para poder hacerlo hijo
        go.transform.SetParent(GetParent(id).transform);
        //Los desactivo para que no aparezcan todos de golpe en la pantalla
        go.SetActive(false);
        //lo añado al diccionario pool que es el que uso para operar con los objetos
        pool[id].Enqueue(go);
        
    }

    //Clase que devuelve el ID del padre (se le pasa como parametro la clave del diccionario)
    static GameObject GetParent(int parentID)
    {
        //Se crea un GameObject en el qeus e almacena el ID del Padre
        GameObject parent;
        parents.TryGetValue(parentID, out parent);
        return parent;
    }

    //Clase para utilizar el objeto(Se le pasa como parámetro el objeto primigeneo)
    public static GameObject GetObject(GameObject objectToPool)
    {
        //Se almacena el id del objeto para identificar en que piscina está, ya que podemos tener varias pscinas(Balas, misiles, powerups,...)
        int id = objectToPool.GetInstanceID();

        //Comprueba si el pool esta vacio ----Esta vacio crea un objeto----- No Esta vacio lo saca de la cola
        if (pool[id].Count == 0)
        {
            CreateObject(objectToPool);
        }
        //Con esta sentencai sacamos el primero de la cola(el que está preparado)
        GameObject go = pool[id].Dequeue();
        go.SetActive(true);

        return go;

    }

    //Clase que retorna el objeto activado a la cola (objeto primigeneo(para sacar el id), Objeto que se quiere poner en la cola)
    public static void RecicleObject(GameObject objectToPool, GameObject objectToRecicle)
    { 
        //Se saca el id para saber a que piscina meterlo
        int id = objectToPool.GetInstanceID();
        //Se mete el objeto que se quiere reutilizar en la cola y se desactiva
        pool[id].Enqueue(objectToRecicle);
        objectToRecicle.SetActive(false);
    }
}
