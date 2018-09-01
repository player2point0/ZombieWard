using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float HealthAmount;
    public bool IsZombie;
    public Text UIText;
    public Slider UISlider;
    public string[] names;

    private ZombieController Zombie;

	void Start ()
    {
        int index = Random.Range(0, names.Length - 1);

        if (IsZombie)
        {
            Zombie = GetComponent<ZombieController>();
            UIText.text = "Zombie " + names[index].ToString();
        }
        else UIText.text = names[index].ToString();

        UISlider.value = HealthAmount;
	}

    private void LateUpdate()
    {
        UISlider.value = HealthAmount;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!IsZombie)
        {
            bool zombieDamage = collision.gameObject.tag == "Zombie";

            RemoveHealth(0.1f, zombieDamage);
        }
    }

    public void AddHealth(float amt)
    {
        HealthAmount += amt;

        if(IsZombie)
        {
            Zombie.IncreaseSpeed();

            if(HealthAmount >= 100)
            {
                GameController gc = FindObjectOfType<GameController>();
                gc.AddPerson(Zombie);
                IsZombie = false;

                UIText.text = UIText.text.Split(' ')[1];
            }
        }
    }
    public void RemoveHealth(float amt, bool ZombieDamage)
    {
        HealthAmount -= amt;

        if(HealthAmount <= 0)
        {
            GameController gc = FindObjectOfType<GameController>();
            bool player = GetComponent<PlayerMoveController>() != null;

            if(player)
            {
                gc.GameOVer();
            }

            else
            {
                if (IsZombie) gc.RemoveZombie(Zombie);

                else if (ZombieDamage)
                {
                    gc.AddZombie(Zombie);
                    IsZombie = true;
                    HealthAmount = 50;
                    UIText.text = "Zombie" + UIText.text;
                }

                else gc.RemovePerson(Zombie);
            }
        }
    }

}
