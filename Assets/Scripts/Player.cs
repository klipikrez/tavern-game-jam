using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Functions;

public class Player : MonoBehaviour
{

    public Rigidbody2D body;
    public float stoppingDrag = 0.5f;
    public Transform floorCheckCenter;
    public float floorCheckRadious = 0.5f;
    public LayerMask floorCheckIgnoreLayers;
    bool grounded = false;
    public float jumpStrenth = 10f;
    public float moveStrenth = 10f;
    public float maxSpeed = 10f;
    Coroutine groundedDelayCoroutine;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (grounded && !CheckGrounded())
        {
            if (groundedDelayCoroutine == null)
                groundedDelayCoroutine = StartCoroutine(c_GroundedDelay());
        }
        else
        {
            if (CheckGrounded())
            {
                if (groundedDelayCoroutine != null)
                {
                    StopCoroutine(groundedDelayCoroutine);
                    groundedDelayCoroutine = null;
                }
                grounded = true;
            }
        }
        Movement();
        Jump();
    }

    void Movement()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");


        Vector2 velocityTMP = body.velocity;
        velocityTMP.x += moveStrenth * move.x * Time.deltaTime;




        if (grounded && ((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) && !Input.GetButton("Jump")))// ako pipas zemlju, imas veliko trenje
        {

            velocityTMP.x -= velocityTMP.x * stoppingDrag * Time.deltaTime;
        }



        //max brzina
        if (Mathf.Abs(velocityTMP.x) > maxSpeed)
        {
            body.velocity = new Vector2(Mathf.Lerp(Mathf.Sign(velocityTMP.x) * maxSpeed, velocityTMP.x, DeltaTimeLerp(0.15f)), velocityTMP.y);
        }
        else
        {
            body.velocity = new Vector2(velocityTMP.x, velocityTMP.y);
        }


    }

    void Jump()
    {
        if (grounded && (Input.GetKey(KeyCode.W) || Input.GetButton("Jump"))) // jump
        {
            body.velocity = Vector2.up * jumpStrenth;
            body.position += Vector2.up * 0.01f;
            grounded = false;
        }
    }
    bool CheckGrounded()
    {
        DrawCircle(floorCheckCenter.position, floorCheckRadious, 52, Color.blue);
        return Physics2D.OverlapCircle(floorCheckCenter.position, floorCheckRadious, ~floorCheckIgnoreLayers);
    }

    IEnumerator c_GroundedDelay()
    {
        yield return new WaitForSeconds(0.2f);
        grounded = false;
        groundedDelayCoroutine = null;
    }


}

