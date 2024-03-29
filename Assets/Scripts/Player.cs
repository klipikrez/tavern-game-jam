using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
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

        if (CheckGrounded())
        {
            if (groundedDelayCoroutine != null)
            {
                StopCoroutine(groundedDelayCoroutine);
                groundedDelayCoroutine = null;
            }
            grounded = true;
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




        if (CheckGrounded() && (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetButton("Jump")))// ako pipas zemlju, imas veliko trenje
        {

            velocityTMP.x -= velocityTMP.x * stoppingDrag * Time.deltaTime;
        }



        //max brzina
        if (Mathf.Abs(velocityTMP.x) > maxSpeed)
        {
            body.velocity = new Vector2(Mathf.Lerp(Mathf.Sign(velocityTMP.x) * maxSpeed, velocityTMP.x, DeltaTimeLerp(0.3f)), velocityTMP.y);
        }
        else
        {
            body.velocity = new Vector2(velocityTMP.x, velocityTMP.y);
        }


    }

    void Jump()
    {
        bool jumped = false;
        if (grounded && (Input.GetKey(KeyCode.W) || Input.GetButton("Jump"))) // jump
        {
            body.velocity = Vector2.up * jumpStrenth;
            body.position += Vector2.up * 0.01f;
            grounded = false;
            jumped = true;
            AudioManager.Instance.PlayAudioClip("zapsplat_multimedia_game_sound_classic_jump_004_41723");
        }
        //Debug.Log((Input.GetKeyUp(KeyCode.W) || Input.GetButtonUp("Jump")));
        if ((Input.GetKeyUp(KeyCode.W) || Input.GetButtonUp("Jump")) && (body.velocity.y > 0 || jumped))
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 1.5f);
        }
    }
    bool CheckGrounded()
    {
        DrawCircle(floorCheckCenter.position, floorCheckRadious, 52, Color.blue);
        List<Collider2D> colliders = new List<Collider2D>();
        foreach (Collider2D col in Physics2D.OverlapCircleAll(floorCheckCenter.position, floorCheckRadious, ~floorCheckIgnoreLayers))
        {
            if (!col.isTrigger)
            {

                colliders.Add(col);
            }
        }
        return colliders.Count == 0 ? false : true;
    }

    IEnumerator c_GroundedDelay()
    {
        yield return new WaitForSeconds(0.2f);
        grounded = false;
        groundedDelayCoroutine = null;
    }


}

