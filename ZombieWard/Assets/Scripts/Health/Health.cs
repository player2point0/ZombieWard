using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float HealthAmount;
    public Text UIText;
    public Slider UISlider;
    public string[] names;

	void Start ()
    {
        int index = Random.Range(0, names.Length - 1);

        UIText.text = names[index].ToString();

        UISlider.value = HealthAmount;
	}

    private void LateUpdate()
    {
        UISlider.value = HealthAmount;
    }

    public virtual void AddHealth(float amt)
    {
        HealthAmount += amt;
    }
    public virtual void RemoveHealth(float amt, bool ZombieDamage)
    {
        HealthAmount -= amt;
    }
}
