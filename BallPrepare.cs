using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BallPrepare : MonoBehaviour
{
    [SerializeField]
    private GameObject top_stick_bone;
    private bool prepare_one_time;
    [SerializeField]
    private Animator stick_anim;
    private Rigidbody rb_body;
    public float force_meter;
    [SerializeField]
    private GameObject ball;
    private void Awake()
    {
        rb_body = gameObject.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        DOTween.Init();
        prepare_one_time = true;
        rb_body.isKinematic = true;
        Physics.gravity = Vector3.zero;
        force_meter = 0;
    }
    private void Update()
    {
        if (GameManager.gameManager_Instance.GetCurrentState() == GameManager.GameStates.BALL_PREPARE)
        {
            Cursor.lockState = CursorLockMode.Locked;
            PrepareStretchAnim();
            Stretching();
            Throw();
        }
    }
    void PrepareStretchAnim()
    {
        if (prepare_one_time)
        {
            prepare_one_time = false;
            ball.transform.parent = top_stick_bone.transform;
        }
    }
    void Stretching()
    {
        stick_anim.Play("Bend");
        float anim_speed = Mathf.Clamp(Input.GetAxisRaw("Mouse X") * -1, -1,1);
        stick_anim.SetFloat("AnimSpeed",anim_speed);
    }
    void Throw()
    {
        if (Input.GetMouseButtonUp(0))
        {
            rb_body.isKinematic = false;
            ball.transform.parent = gameObject.transform;
            ball.transform.DOLocalMove(Vector3.zero,0.4f);
            rb_body.AddForce(new Vector3(0, force_meter, force_meter),ForceMode.Impulse);
            Physics.gravity = new Vector3(0, -10, 0);
            stick_anim.Play("Release");
            Debug.Log(force_meter);
            GameManager.gameManager_Instance.SetCurrentState(GameManager.GameStates.BALL_FLY);
        }
    }
}
