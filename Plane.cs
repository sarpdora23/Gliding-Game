using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private Transform player_ball;
    private float distance_z,distance_x;
    private void Awake()
    {
        player_ball = GameObject.Find("PlayerBall").transform;
        distance_z = transform.position.z - player_ball.position.z;
        distance_x = transform.position.x - player_ball.position.x;
    }
    private void Update()
    {
        if (distance_z > transform.position.z - player_ball.position.z)
        {
            transform.Translate(Vector3.forward);
        }
        if (distance_x > transform.position.x - player_ball.position.x)
        {
            transform.Translate(Vector3.right);
        }
        else if (distance_x < transform.position.x - player_ball.position.x)
        {
            transform.Translate(Vector3.left);
        }
    }
}
