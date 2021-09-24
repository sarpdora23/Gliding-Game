using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallFly : MonoBehaviour
{
    [SerializeField]
    private Animator ball_anim;
    [SerializeField]
    private Animator cam_anim;
    private bool one_time_setCameraPosition;
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private float rotation_speed;
    private bool canRotate;
    private bool canControll;
    [SerializeField]
    private float horizontal_Speed;
    private Rigidbody rb_body;
    public float lerp_t;
    private Vector3 tem_pos;
    [SerializeField]
    private float x_distance;
    private void Awake()
    {
        rb_body = gameObject.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        one_time_setCameraPosition = true;
        DOTween.Init();
        canRotate = true;
        canControll = false;
        lerp_t = 0.5f;
        tem_pos = transform.position;
    }
    private void Update()
    {
        if (GameManager.gameManager_Instance.GetCurrentState() == GameManager.GameStates.BALL_FLY)
        {
            ControllBall();
            SetCameraPosition();
            RotateBall();
            OpenWings();
            ControllBall();
        }
        CheckXpositon();
    }
    void CheckXpositon()
    {
       
        if ((transform.position.x - tem_pos.x) >= x_distance)
        {
            LevelGenerator.levelGenerator_Instance.x_position = LevelGenerator.X_Position.BIGGER;
            tem_pos = transform.position;
            Debug.Log("tEST BİGGER");
        }
        else if ((transform.position.x - tem_pos.x) <= -x_distance)
        {
            LevelGenerator.levelGenerator_Instance.x_position = LevelGenerator.X_Position.LOWER;
            tem_pos = transform.position;
            Debug.Log("TEST LOWER");
        }
    }
    void SetCameraPosition()
    {
        if (one_time_setCameraPosition)
        {
            cam_anim.Play("SetCameraPosition");
        }
    }
    void RotateBall()
    {
        if (Input.GetMouseButtonUp(0))
        {
            lerp_t = 0.5f;
            canControll = false;
            ball_anim.SetTrigger("Close");
            canRotate = true;
            Physics.gravity = Vector3.down * 10;
        }
        if (canRotate)
        {
            ball.transform.Rotate(new Vector3(rotation_speed * Time.deltaTime * 10, 0, 0));
        }
        
    }
    void OpenWings()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canRotate = false;
            ball_anim.ResetTrigger("Close");//56.309
            ball.transform.DORotateQuaternion(Quaternion.Euler(45.7165f, -4.741f, -6.596f),0.7f).OnComplete(()=> ball_anim.Play("OpenWing"));
            Physics.gravity = Vector3.down * 3;
            StartCoroutine(OpenControllDelay());
        }
    }
    IEnumerator OpenControllDelay()
    {
        yield return new WaitForSeconds(1f);
        canControll = true;
    }
    void ControllBall()
    {
        if (canControll)
        {
            //LEFT --> X: 49.167, Y: 31.964, 39,513
            //RIGHT --> X:42.266, Y: -41.446, -52.705
            float h = Input.GetAxisRaw("Mouse X");
            Vector3 temp_rot = ball.transform.eulerAngles;
            lerp_t += h * Time.deltaTime;
            lerp_t = Mathf.Clamp(lerp_t, 0, 1);
            temp_rot.x = Mathf.Lerp(49.167f, 42.266f, lerp_t);
            temp_rot.y = Mathf.Lerp(31.964f, -41.446f, lerp_t);
            temp_rot.z = Mathf.Lerp(39.513f, -52.705f, lerp_t);
            ball.transform.eulerAngles = temp_rot;
            rb_body.velocity = new Vector3(horizontal_Speed * h, rb_body.velocity.y, rb_body.velocity.z);
            if (lerp_t > 0.5f)
            {
                rb_body.velocity = new Vector3(horizontal_Speed, rb_body.velocity.y, rb_body.velocity.z);
            }
            else if (lerp_t < 0.5f)
            {
                rb_body.velocity = new Vector3(-horizontal_Speed, rb_body.velocity.y, rb_body.velocity.z);
            }
        }
    }
}
