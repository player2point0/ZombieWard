using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Zombie;
    public Transform[] SpawnPositions;
    public float SpawnDelay;
    public int MaxZombieNum;
    public Text UIMessageText;
    public Text UIZombieCountText;

    private List<ZombieController> Zombies;
    private List<GameObject> People;
    private SceneController sceneController;
    private int SpawnCount;

	void Start ()
    {
        Zombies = new List<ZombieController>();
        People = new List<GameObject>();
        sceneController = FindObjectOfType<SceneController>();
        SpawnCount = 0;
        UIMessageText.text = "";
        People.Add(GameObject.FindGameObjectWithTag("Player"));
        InvokeRepeating("SpawnZombie", 0, SpawnDelay);
        StartCoroutine("SetZombiesTarget");
        SetZombieNumUI();
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
        SpawnCount++;

        if (SpawnCount > MaxZombieNum)
        {
            CancelInvoke("SpawnZombie");
            return;
        }

        int index = Random.Range(0, SpawnPositions.Length);

        Debug.Log(index);

        GameObject temp = Instantiate(Zombie, SpawnPositions[index].position, Quaternion.identity, this.transform);
        Zombies.Add(temp.GetComponentInChildren<ZombieController>());
        SetZombieNumUI();
    }

    public void AddPerson(ZombieController oldZombie)
    {
        StopCoroutine("SetZombieTarget");

        oldZombie.ChangeToPerson();
        Zombies.Remove(oldZombie);
        People.Add(oldZombie.gameObject);

        CheckLevelComplete();
        SetZombieNumUI();

        StartCoroutine("SetZombiesTarget");
    }
    public void AddZombie(ZombieController oldPerson)
    {
        StopCoroutine("SetZombieTarget");

        oldPerson.changeToZombie();
        People.Remove(oldPerson.gameObject);
        Zombies.Add(oldPerson);
        SetZombieNumUI();

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

        CheckLevelComplete();
        SetZombieNumUI();

        StartCoroutine("SetZombiesTarget");
    }

    void CheckLevelComplete()
    {
        if(SpawnCount >= MaxZombieNum)
        {
            if(Zombies.Count == 0)
            {
                if(sceneController != null) sceneController.Invoke("LoadNextLevel", 1);
                UIMessageText.text = "Level Complete";
            }
        }
    }

    void SetZombieNumUI()
    {
        UIZombieCountText.text = Zombies.Count.ToString() + " / " + MaxZombieNum.ToString();
    }

    public void GameOVer()
    {
        if (sceneController != null) sceneController.Invoke("RestartLevel", 1);
        UIMessageText.text = "Game over";
    }
}
