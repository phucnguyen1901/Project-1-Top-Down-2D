using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;

    private Rigidbody2D rb;

    private Vector2 moveDir;

    private KnockBack knockBack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
    }

    private void FixedUpdate()
    {
        if (knockBack.GettingKnockedBack) return;
        rb
            .MovePosition(rb.position +
            moveSpeed * Time.fixedDeltaTime * moveDir);
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }
}
