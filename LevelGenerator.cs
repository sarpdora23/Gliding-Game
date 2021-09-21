using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public BallFly ballFly_script;
    public Transform player_ball;
    [SerializeField]
    private GameObject jump_Platform1_Prefab;
    [SerializeField]
    private GameObject jump_Platform2_Prefab;
    private static LevelGenerator levelGenerator = null;
    [SerializeField]
    private GameObject line;
    [SerializeField]
    private Transform first_spawn_transform;
    private Vector3 spawn_position;
    private GameObject[][] lines_array = new GameObject[5][];
    private int z_counter, min_x_index,max_x_index;
    [SerializeField]
    private Transform level_parent;
    private Vector3 min_Censor, max_Censor;
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
        for (int i = 0; i < 5; i++)
        {
            lines_array[i] = new GameObject[3];
        }       
        spawn_position = first_spawn_transform.position;
        z_counter = 0;
        min_x_index = 0;
        max_x_index = 2;
    }
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 temp_position = spawn_position;
            temp_position.x -= 60;
            for (int j = 0; j < 3; j++)
            {
                GameObject obj = Instantiate(line, temp_position, Quaternion.identity);
                obj.transform.parent = level_parent;
                lines_array[i][j] = obj;
                temp_position.x += 360;
            }
            spawn_position.z += 111;
        }
       
    }
    private void Update()
    {
        if (player_ball.position.x > max_Censor.x)
        {
            GenerateNewLineX(true);
        }
        else if (player_ball.position.x < min_Censor.x)
        {
            GenerateNewLineX(false);
        }
    }
    public void IsVisible(Transform obj)
    {
        if (obj.position.z < player_ball.position.z)
        {
            GenerateNewLineZ();
        }
    }
    private void GenerateNewLineZ()
    {
        foreach (GameObject line in lines_array[z_counter])
        {
            Vector3 temp_position = line.transform.position;
            temp_position.z = spawn_position.z;
            line.transform.position = temp_position;
        }
        spawn_position.z += 111;
        if (z_counter == 4)
        {
            z_counter = 0;
        }
        else
        {
            z_counter++;
        }

    }
    private void GenerateNewLineX(bool isBigger)
    {
        if (isBigger)
        {
            for (int i = 0; i < lines_array.Length; i++)
            {
                Vector3 temp_position = lines_array[i][max_x_index].transform.position;
                temp_position.x += 360;
                lines_array[i][min_x_index].transform.position = temp_position;
            }
            max_x_index = min_x_index;
            if (min_x_index == 2)
            {
                min_x_index = 0;
            }
            else
            {
                min_x_index++;
            }
        }
        else
        {
            for (int i = 0; i < lines_array.Length; i++)
            {
                Vector3 temp_position = lines_array[i][min_x_index].transform.position;
                temp_position.x -= 300;
                lines_array[i][max_x_index].transform.position = temp_position;
            }
            min_x_index = max_x_index;
            if (max_x_index == 0)
            {
                max_x_index = 2;
            }
            else
            {
                max_x_index--;
            }
        }       
    }
}
