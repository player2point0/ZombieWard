using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rigidbody2D;


    void Start ()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
    {
        LookAtMouse();
        Move();
    }

    void Move()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        Vector2 force = new Vector2(x, y);
        force *= speed;

        rigidbody2D.AddForce(force);
    }

    void LookAtMouse()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
