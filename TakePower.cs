using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePower : MonoBehaviour
{
    [SerializeField]
    private BallPrepare ballPrepareScript;
    private Animator stick_anim;
    [SerializeField]
    private float force;
    [SerializeField]
    private float max_force;
    private void Awake()
    {
        stick_anim = gameObject.GetComponent<Animator>();
    }
    void AddForce()
    {
        float abs_force = stick_anim.GetFloat("AnimSpeed");
        if (abs_force > 0)
        {
            abs_force = 1;
        }
        else if (abs_force < 0)
        {
            abs_force = -1;
        }
        ballPrepareScript.force_meter += force * abs_force;
        ballPrepareScript.force_meter = Mathf.Clamp(ballPrepareScript.force_meter,0,max_force);
        Debug.Log(ballPrepareScript.force_meter);
    }
}
