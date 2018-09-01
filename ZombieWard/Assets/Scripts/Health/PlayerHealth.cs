using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Zombie") RemoveHealth(0.1f, true);    
    }

    public override void RemoveHealth(float amt, bool ZombieDamage)
    {
        HealthAmount -= amt;

        if (HealthAmount <= 0)
        {
            GameController gc = FindObjectOfType<GameController>();
            gc.GameOVer();
        }
    }
}
