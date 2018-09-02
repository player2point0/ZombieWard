using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHazardController : Health
{
    public float Damage;

    private ParticleSystem fire;
    private bool buring = false;

    private void OnParticleCollision(GameObject other)
    {
        Health otherHealth = other.GetComponent<Health>();

        if (otherHealth != null)
        {
            otherHealth.RemoveHealth(Damage, false);
        }
    }

    public override void AddHealth(float amt)
    {
        RemoveHealth(amt, false);
    }
    public override void RemoveHealth(float amt, bool ZombieDamage)
    {
        base.RemoveHealth(amt, ZombieDamage);

        if (HealthAmount <= 0 && !buring)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            fire = GetComponent<ParticleSystem>();
            fire.Play();
            buring = true;

            Destroy(this.gameObject, fire.main.startLifetime.constant);
        }
    }
}
