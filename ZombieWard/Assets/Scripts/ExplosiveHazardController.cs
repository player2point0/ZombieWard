using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveHazardController : Health
{
    public float Damage;

    private ParticleSystem explosion;
    private bool exploding = false;

    private void OnParticleCollision(GameObject other)
    {
        Health otherHealth = other.GetComponent<Health>();

        if(otherHealth != null)
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

        if(HealthAmount <= 0 && !exploding)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            explosion = GetComponent<ParticleSystem>();
            explosion.Play();
            exploding = true;

            Destroy(this.gameObject, explosion.main.startLifetime.constant);
        }
    }
}
