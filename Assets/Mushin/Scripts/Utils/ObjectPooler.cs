using System.Collections.Generic;
using Mushin.Scripts.Player;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    private Player _player;
    public static ObjectPooler Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                SetObject(pool, objectPool);
            }

            poolDictionary.Add(pool.poolTag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string objTag, Vector2 position,Quaternion rotation=default)
    {
        if (!poolDictionary.ContainsKey(objTag))
        {
            Debug.LogWarning("Pool with tag " + objTag + " doesn't exist");
            return null;
        }

        if (poolDictionary[objTag].Count == 0)
        {
            Pool pool = pools.Find(x => x.poolTag == objTag);
            if (pool == null)
            {
                Debug.LogWarning("Pool with tag " + objTag + " doesn't exist");
                return null;
            }
            return SetObject(pool, poolDictionary[objTag]);
        }

        GameObject objectToSpawn = poolDictionary[objTag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

    public void ReturnToPool(string objTag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(objTag))
        {
            Debug.LogWarning("Pool with tag " + objTag + " doesn't exist");
            return;
        }

        obj.SetActive(false);
        poolDictionary[objTag].Enqueue(obj);
    }

    private GameObject SetObject(Pool pool, Queue<GameObject> objectPool)
    {
        GameObject obj = Instantiate(pool.prefab, pool.container);
        IPoolable poolObj= obj.GetComponent<IPoolable>();
        switch (poolObj)
        {
            case null:
                Debug.LogWarning("Object with tag " + pool.poolTag + " doesn't implement IPoolable");
                return null;
            //Inyectar player a los orbes de xp
            case Collectable obj1:
            {
                Collectable collectable = obj1;
                collectable.Configure(_player);
                break;
            }
        }

        obj.SetActive(false);
        poolObj.SetTag(pool.poolTag);
        objectPool.Enqueue(obj);
        return obj;
    }
}