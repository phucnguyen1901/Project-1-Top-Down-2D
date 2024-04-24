using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _health = 3;

    private KnockBack knockBack;

    private Flash flash;

    [SerializeField]
    private GameObject deathParticle;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
        }
    }

    public void TakeDamage(int damage)
    {
        knockBack.GetKnockBack(Player.Instance.transform, 8f);
        flash.Flashing();
        StartCoroutine(UpdateHealth(damage));
    }

    private IEnumerator UpdateHealth(int damage)
    {
        yield return new WaitForSeconds(flash.TimeFlash);
        _health -= damage;

        if (_health <= 0)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy (gameObject);
        }
    }

    private enum State
    {
        Roaming
    }

    private State state;

    private EnemyPathFinding enemyPathFinding;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }

    void Start()
    {
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming;
        StartCoroutine(Roaming());
    }

    private IEnumerator Roaming()
    {
        while (state == State.Roaming)
        {
            Vector2 roamingPosition = GetRoamingPosition();
            enemyPathFinding.MoveTo (roamingPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))
            .normalized;
    }
}
