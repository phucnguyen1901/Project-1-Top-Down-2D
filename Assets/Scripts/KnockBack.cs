using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Rigidbody2D rb;

    private float knockBackTime = .2f;

    public bool GettingKnockedBack { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockBack(Transform damagePosition, float knockBackThrust)
    {
        GettingKnockedBack = true;
        Vector2 vector2 =
            knockBackThrust *
            rb.mass *
            (transform.position - damagePosition.position).normalized;
        rb.AddForce(vector2, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        GettingKnockedBack = false;
    }
}
