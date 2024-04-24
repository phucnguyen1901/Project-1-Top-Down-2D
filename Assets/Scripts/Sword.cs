using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Animator animator;

    private bool isAttack = false;

    [SerializeField]
    private GameObject slashPrefab;

    [SerializeField]
    private GameObject slashPosition;

    private GameObject slashAnim;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (!isAttack)
        {
            isAttack = true;
            animator.SetTrigger("Attack");
            CreateSlashDown();
            StartCoroutine(ReturnToIdleAfterDelay());
        }
    }

    private void CreateSlashDown()
    {
        Quaternion rotation = FlipYSlashAnim();
        slashAnim =
            Instantiate(slashPrefab,
            slashPosition.transform.position,
            rotation);
        slashAnim.transform.parent = this.transform.parent;
    }

    private Quaternion FlipYSlashAnim()
    {
        if (Player.Instance.IsFlip)
        {
            return Quaternion.Euler(0, -180, 0);
        }

        return Quaternion.Euler(0, 0, 0);
    }

    private void CreateSlashUp()
    {
        Quaternion rotation = FlipYSlashAnim();
        slashAnim =
            Instantiate(slashPrefab,
            slashPosition.transform.position,
            rotation);
        slashAnim.transform.rotation =
            Quaternion
                .Euler(180, rotation.eulerAngles.y, rotation.eulerAngles.z);
        slashAnim.transform.parent = this.transform.parent;
    }

    private IEnumerator ReturnToIdleAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);

        isAttack = false;
        animator.SetTrigger("Idle");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && isAttack)
        {
            if (other.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(1);
            }
        }
    }
}
