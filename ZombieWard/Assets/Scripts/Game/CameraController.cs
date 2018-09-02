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
                StartCoroutine("LerpTo");
            }
            else if (player.transform.position.x <= transform.position.x - xSize)
            {
                StartCoroutine("LerpTo");
            }

            if (player.transform.position.y >= transform.position.y + ySize)
            {
                StartCoroutine("LerpTo");
            }
            else if (player.transform.position.y <= transform.position.y - ySize)
            {
                StartCoroutine("LerpTo");
            }
        }
    }

    IEnumerator LerpTo()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        Vector3 delta = playerPos- transform.position;
        delta = delta / speed;

        coroutining = true;

        for (int i = 0; i < speed; i++)
        {
            playerPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
            delta = playerPos - transform.position;
            delta = delta / speed;

            transform.position += delta;
            yield return null;
        }
        coroutining = false;
    }
}
