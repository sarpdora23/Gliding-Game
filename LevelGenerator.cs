using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    
    [SerializeField]
    private GameObject jump_Platform1_Prefab;
    [SerializeField]
    private GameObject jump_Platform2_Prefab;
    [SerializeField]
    private int pool_Size;
    private Queue<GameObject> pooledObjects;
    private static LevelGenerator levelGenerator = null;
    public static LevelGenerator levelGenerator_Instance
    {
        get
        {
            if (levelGenerator == null)
            {
                levelGenerator = new GameObject("LevelGenerator").AddComponent<LevelGenerator>();
            }
            return levelGenerator;
        }
    }
    private void OnEnable()
    {
        levelGenerator = this;
    }
    private void Awake()
    {
        pooledObjects = new Queue<GameObject>();
        for (int i = 0; i < pool_Size; i++)
        {
            GameObject obj;
            if (i % 2 == 0)
            {
                obj = Instantiate(jump_Platform1_Prefab);
            }
            else
            {
                obj = Instantiate(jump_Platform2_Prefab);
            }
            obj.SetActive(false);
            pooledObjects.Enqueue(obj);
        }
    }
    private void Start()
    {
       
    }
    private void Update()
    {
      
    }
    public bool IsVisible(Transform obj)
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(obj.position);
        Debug.Log(viewPos);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    private GameObject GetPooledObject()
    {
        GameObject obj = pooledObjects.Dequeue();
        obj.SetActive(true);
        pooledObjects.Enqueue(obj);
        return obj;
    }
}
