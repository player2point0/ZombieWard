using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float Health;
    public float Speed;
    public float SpeedMultiplier;
    public float SpeedIncrement;

    private Rigidbody2D rigidbody2D;
    private Vector3 target;

	void Start ()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        target = Vector3.zero;
	}
	
	void FixedUpdate ()
    {
        MoveToTarget();   
    }

    void MoveToTarget()
    {
        rigidbody2D.position = Vector3.MoveTowards(rigidbody2D.position, target, (Speed * SpeedMultiplier));

        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void SetTarget(Vector3 pos)
    {
        target = pos;
    }

    public void IncreaseSpeed()
    {
        SpeedMultiplier += SpeedIncrement;
    }

    public void ChangeToPerson()
    {
        float coinFlip = Random.value;

        if(coinFlip > 0.5f)
        {
            target = transform.position;
            return;
        }

        //set random target
        int angle = Random.Range(0, 360);
        float x = 1000 * Mathf.Sin(angle);
        float y = 1000 * Mathf.Cos(angle);

        SetTarget(new Vector3(x, y, 0));
    }
    public void changeToZombie()
    {
        SpeedMultiplier = 1;
    }
}
