using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Zombie;
    public float SpawnRadius;
    public float SpawnDelay;
    public int MaxZombieNum;

    private List<ZombieController> Zombies;
    private List<GameObject> People;
    private int SpawnCount;

	void Start ()
    {
        Zombies = new List<ZombieController>();
        People = new List<GameObject>();
        SpawnCount = 0;
        People.Add(GameObject.FindGameObjectWithTag("Player"));
        InvokeRepeating("SpawnZombie", 0, SpawnDelay);
        StartCoroutine("SetZombiesTarget");
	}
	
    IEnumerator SetZombiesTarget()
    {
        int length = Zombies.Count;

        while(true)
        {
            length = Zombies.Count;

            for (int i = length - 1; i >= 0; i--)
            {
                //find closest target
                float minDis = Mathf.Infinity;
                Vector3 pos = Vector3.zero;

                for(int j = 0;j<People.Count;j++)
                {
                    float dis = Vector3.Distance(People[j].transform.position, Zombies[i].transform.position);

                    if(dis < minDis)
                    {
                        minDis = dis;
                        pos = People[j].transform.position;
                    }
                }

                Zombies[i].SetTarget(pos);

                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnZombie()
    {
        if(SpawnCount > MaxZombieNum)
        {
            CancelInvoke("SpawnZombie");
            return;
        }

        SpawnCount++;

        int angle = Random.Range(0, 360);
        float x = SpawnRadius * Mathf.Sin(angle);
        float y = SpawnRadius * Mathf.Cos(angle);

        GameObject temp = Instantiate(Zombie, new Vector3(x, y, 0), Quaternion.identity, this.transform);
        Zombies.Add(temp.GetComponentInChildren<ZombieController>());
    }

    public void AddPerson(ZombieController oldZombie)
    {
        StopCoroutine("SetZombieTarget");

        oldZombie.ChangeToPerson();
        Zombies.Remove(oldZombie);
        People.Add(oldZombie.gameObject);

        StartCoroutine("SetZombiesTarget");
    }
    public void AddZombie(ZombieController oldPerson)
    {
        StopCoroutine("SetZombieTarget");

        oldPerson.changeToZombie();
        People.Remove(oldPerson.gameObject);
        Zombies.Add(oldPerson);

        StartCoroutine("SetZombiesTarget");
    }
    public void RemovePerson(ZombieController oldPerson)
    {
        StopCoroutine("SetZombieTarget");

        People.Remove(oldPerson.gameObject);
        Destroy(oldPerson.transform.parent.gameObject);

        StartCoroutine("SetZombiesTarget");
    }
    public void RemoveZombie(ZombieController oldZombie)
    {
        StopCoroutine("SetZombieTarget");

        Zombies.Remove(oldZombie);
        Destroy(oldZombie.transform.parent.gameObject);

        StartCoroutine("SetZombiesTarget");
    }

    public void GameOVer()
    {
        Debug.Log("Game OVer");
        Time.timeScale = 0.5f;
    }
}
