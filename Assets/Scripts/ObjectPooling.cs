using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling current;
    public GameObject[] objectsToPool;
    public bool willGrow = true;
    public List<GameObject> pooledObjects;

    int index;
    void Awake()
    {
        current = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int j = 0; j < objectsToPool.Length; j++)
        {
            GameObject obj = (GameObject)Instantiate(objectsToPool[j]);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

    }

    public GameObject GetpooledObject()
    {
        index = Random.Range(0, objectsToPool.Length);
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];
        }

        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(objectsToPool[index]);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }

}
