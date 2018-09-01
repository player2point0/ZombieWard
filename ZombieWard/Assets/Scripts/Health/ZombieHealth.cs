using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : Health
{
    public bool IsZombie;

    private ZombieController Zombie;

    void Start()
    {
        int index = Random.Range(0, names.Length - 1);

        Zombie = GetComponent<ZombieController>();
        UIText.text = "Zombie " + names[index].ToString();

        UISlider.value = HealthAmount;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!IsZombie)
        {
            bool zombieDamage = collision.gameObject.tag == "Zombie";

            RemoveHealth(0.1f, zombieDamage);
        }
    }

    public override void AddHealth(float amt)
    {
        HealthAmount += amt;

        if (IsZombie)
        {
            Zombie.IncreaseSpeed();

            if (HealthAmount >= 100)
            {
                GameController gc = FindObjectOfType<GameController>();
                gc.AddPerson(Zombie);
                IsZombie = false;

                UIText.text = UIText.text.Split(' ')[1];
            }
        }
    }
    public override void RemoveHealth(float amt, bool ZombieDamage)
    {
        HealthAmount -= amt;

        if (HealthAmount <= 0)
        {
            GameController gc = FindObjectOfType<GameController>();
            
            if (IsZombie) gc.RemoveZombie(Zombie);//kill zombie

            else if (ZombieDamage)//change zombie to person
            {
                gc.AddZombie(Zombie);
                IsZombie = true;
                HealthAmount = 50;
                UIText.text = "Zombie" + UIText.text;
            }

            else gc.RemovePerson(Zombie);//kill person
        }
    }
}
