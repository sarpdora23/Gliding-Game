using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimScripts : MonoBehaviour
{
    [SerializeField]
    private BallFly ballfly_script;
    void CloseAnim()
    {
        ballfly_script.CloseAnimEvent();
    }
}
