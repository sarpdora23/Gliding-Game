using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    [SerializeField]
    private GameObject jump_Platform1,jump_Platform2;
    private static LineScript lineScript = null;
    private Vector3 min_X, max_X;
    private Vector3 spawn_position;
    [SerializeField]
    private int min_X_Gameobject_index, max_X_Gameobject_index;
    [SerializeField]
    private GameObject[] platforms_in_line = new GameObject[6];
    [SerializeField]
    private int middle_Gameobject_index;
    private BallFly ballFly_script = LevelGenerator.levelGenerator_Instance.ballFly_script;
    public static LineScript lineScript_Instance
    {
        get
        {
            if (lineScript == null)
            {
                lineScript = new LineScript();
            }
            return lineScript;
        }
    }
    private void OnEnable()
    {
        lineScript = this;
    }
    private void Awake()
    {
        min_X = transform.position;
        min_X.x -= 180;
        spawn_position = min_X;
        for (int i = 0; i < 6; i++)
        {
            int random_num = Random.Range(0, 2);
            GameObject obj;
            if (random_num == 1)
            {
                obj = Instantiate(jump_Platform1, spawn_position, Quaternion.identity);
            }
            else
            {
                obj = Instantiate(jump_Platform2, spawn_position, Quaternion.identity);
            }
            platforms_in_line[i] = obj;
            spawn_position.x += 60;
            obj.transform.parent = gameObject.transform;
        }
        max_X = platforms_in_line[5].transform.position;
        min_X_Gameobject_index = 0;
        max_X_Gameobject_index = 5;
        middle_Gameobject_index = 2;
    }
    void Update()
    {
        LevelGenerator.levelGenerator_Instance.IsVisible(transform);
    }
    void GenerateNewPlatform()
    {
       
    }
    
}
