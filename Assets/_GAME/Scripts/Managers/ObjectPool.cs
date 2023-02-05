using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : PersistentSingleton<ObjectPool>
{
    public GameObject prefab;
    public int poolSize = 20;

    
    private Queue<GameObject> objects = new Queue<GameObject>();

    private void Start()
    {
        objects = new Queue<GameObject>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objects.Enqueue(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        if (objects.Count == 0)
        {
            GameObject obj = Instantiate(prefab);
            objects.Enqueue(obj);
        }
        GameObject pooledObject = objects.Dequeue();
        pooledObject.SetActive(true);
        return pooledObject;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        objects.Enqueue(obj);
    }
}
