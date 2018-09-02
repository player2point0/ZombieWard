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
        SetTarget(transform.position);
    }
    public void changeToZombie()
    {
        SpeedMultiplier = 1;
    }
}
