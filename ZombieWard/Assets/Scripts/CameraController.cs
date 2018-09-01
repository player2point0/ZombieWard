using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float xSize;
    public float ySize;
    public int speed;

    private PlayerMoveController player;
    private bool coroutining;

    void Start()
    {
        player = FindObjectOfType<PlayerMoveController>();
        coroutining = false;
    }

    void Update()
    {
        if (!coroutining)
        {
            if (player.transform.position.x >= transform.position.x + xSize)
            {
                Vector3 pos = transform.position + new Vector3(xSize, 0, 0);
                StartCoroutine("LerpTo", pos);
            }
            else if (player.transform.position.x <= transform.position.x - xSize)
            {
                Vector3 pos = transform.position + new Vector3(-xSize, 0, 0);
                StartCoroutine("LerpTo", pos);
            }

            if (player.transform.position.y >= transform.position.y + ySize)
            {
                Vector3 pos = transform.position + new Vector3(0, ySize, 0);
                StartCoroutine("LerpTo", pos);
            }
            else if (player.transform.position.y <= transform.position.y - ySize)
            {
                Vector3 pos = transform.position + new Vector3(0, -ySize, 0);
                StartCoroutine("LerpTo", pos);
            }
        }
    }

    IEnumerator LerpTo(Vector3 pos)
    {
        Vector3 delta = pos - transform.position;
        delta = delta / speed;

        coroutining = true;

        for (int i = 0; i < speed; i++)
        {
            transform.position += delta;
            yield return null;
        }
        coroutining = false;
    }
}
