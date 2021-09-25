using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAddForce : MonoBehaviour
{
    private Rigidbody rb_body;
    [SerializeField]
    private float force;
    [SerializeField]
    private BallFly ballFly_script;
    private void Awake()
    {
        rb_body = gameObject.GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("JumpPlatform1Force"))
        {
            ballFly_script.CloseWings();
            rb_body.AddForce(new Vector3(0, force, force),ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("JumpPlatform2Force"))
        {
            ballFly_script.CloseWings();
            rb_body.AddForce(new Vector3(0, force * 2, force * 2),ForceMode.Impulse);
        }
    }
}
