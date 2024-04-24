using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    private new ParticleSystem particleSystem;

    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (particleSystem && !particleSystem.IsAlive())
        {
            Destroy (gameObject);
        }
    }
}
