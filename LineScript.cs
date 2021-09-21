using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    [SerializeField]
    private GameObject jump_Platform1,jump_Platform2;
    private Vector3 min_X;
    private Vector3 spawn_position;
    [SerializeField]
    private GameObject[] platforms_in_line = new GameObject[6];
    [SerializeField]
    private int middle_Gameobject_index;
    private Vector3 min_x_position, max_x_position;
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
        min_x_position = platforms_in_line[0].transform.position;
        max_x_position = platforms_in_line[5].transform.position;
    }
    void Update()
    {
        LevelGenerator.levelGenerator_Instance.IsVisible(transform,min_x_position,max_x_position);
    }
}

