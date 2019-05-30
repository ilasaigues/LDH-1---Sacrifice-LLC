using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistController : MonoBehaviour
{
    public float speed;
    [Range(0, 1)]
    public float rotationBias = .5f;

    public ItemGrabber itemGrabber;
    public Transform grabberAxis;

    public Animator animController;

    private Rigidbody2D _rb2d;
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
    }

    float previousGrabInput;
    void FixedUpdate()
    {
        if (TimeManager.paused) return;
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        SetLookDirection(moveDir);
        _rb2d.MovePosition(transform.position + moveDir * speed * TimeManager.deltaTime);

        if (moveDir != Vector3.zero)
        {
            if (Vector3.Dot(grabberAxis.up, moveDir) < -0.95f)
            {
                moveDir = Vector3.Cross(moveDir, Vector3.forward);
            }
            moveDir = Vector3.Slerp(grabberAxis.up, moveDir, rotationBias);
            grabberAxis.up = moveDir;
        }

        if (Input.GetAxis("Grab") > 0 && previousGrabInput <= 0) //Am pressing
        {
            itemGrabber.Toggle();
        }
        previousGrabInput = Input.GetAxis("Grab");
    }

    public void SetLookDirection(Vector3 direction)
    {
        if (direction.magnitude > Mathf.Epsilon)
        {
            animController.SetFloat("Horizontal", direction.x);
            animController.SetFloat("Vertical", direction.y);
        }
    }

}
