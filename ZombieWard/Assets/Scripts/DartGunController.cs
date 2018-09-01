using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartGunController : MonoBehaviour
{
    public float Damage;

    private ParticleSystem GunParticleSystem;

    void Start()
    {
        GunParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Enable();   

        else if (Input.GetMouseButtonUp(0)) Disable();
    }

    private void OnParticleCollision(GameObject other)
    {
        Health otherHealth = other.GetComponent<Health>();

        if (otherHealth != null) otherHealth.AddHealth(Damage);
    }

    public void Enable()
    {
        if (!GunParticleSystem.isEmitting) GunParticleSystem.Play();
    }
    public void Disable()
    {
        if (GunParticleSystem.isEmitting) GunParticleSystem.Stop();
    }
}
