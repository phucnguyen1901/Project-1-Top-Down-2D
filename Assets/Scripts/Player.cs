using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public Joystick joystick;

    public float speed;

    private bool isSpeedUp;

    private Rigidbody2D rb;

    private Animator animator;

    private bool _isFlip;

    [SerializeField]
    private TrailRenderer trailRenderer;

    public bool IsFlip
    {
        get
        {
            return _isFlip;
        }
        set
        {
            _isFlip = value;
        }
    }

    private void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
     }

    private void FixedUpdate()
    {
        bool isMoving = joystick.Direction != Vector2.zero;
        if (isMoving)
        {
            animator.SetTrigger(isMoving ? "Run" : "Idle");
            FlipPlayer();
            rb.velocity =
                new Vector2(joystick.Direction.x * speed,
                    joystick.Direction.y * speed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void FlipPlayer()
    {
        if (joystick.Direction.x < 0)
        {
            IsFlip = true;
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else if (joystick.Direction.x > 0)
        {
            IsFlip = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void SpeedUp()
    {
        if (!isSpeedUp)
        {
            speed *= 3;
            isSpeedUp = true;
            trailRenderer.emitting = true;
            StartCoroutine(EndSpeedUp());
        }
    }

    private IEnumerator EndSpeedUp()
    {
        yield return new WaitForSeconds(1f);
        speed /= 3;
        isSpeedUp = false;
        trailRenderer.emitting = false;
    }
}
