using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;

	void Start ()
    {
        setPos();
	}
	
	void Update ()
    {
        setPos();
	}

    void setPos()
    {
        transform.position = target.transform.position;
    }
}
