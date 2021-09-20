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
    private GameObject[] lines_array = new GameObject[5];
    private int counter;
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
        spawn_position = first_spawn_transform.position;
        counter = 0;
    }
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
           GameObject line_instance= Instantiate(line, spawn_position, Quaternion.identity);
           lines_array[i] = line_instance;
           spawn_position.z += 111; 
        }
       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateNewLine();
        }
    }
    public void IsVisible(Transform obj)
    {
        if (obj.position.z < player_ball.position.z)
        {
            GenerateNewLine();
        }        
    }
    private void GenerateNewLine()
    {
        Debug.Log("Hayda");
        lines_array[counter].transform.position = spawn_position;
        spawn_position.z += 111;
        if (counter == 4)
        {
            counter = 0;
        }
        else
        {
            counter++;
        }
    }
}
