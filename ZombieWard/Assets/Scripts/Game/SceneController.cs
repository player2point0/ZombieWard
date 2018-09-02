using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Button StartButton;
    public Button RestartButton;

    private int index;
    private int sceneNum;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Start()
    {
        if (FindObjectsOfType<SceneController>().Length > 1) Destroy(this.gameObject);

        sceneNum = SceneManager.sceneCountInBuildSettings;

        RestartButton.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(index);
    }

    public void LoadNextLevel()
    {
        Debug.Log("click");

        index++;

        if (index > sceneNum - 1) LoadMenu();

        if(index > 0) StartButton.gameObject.SetActive(false);

        if (index > 1) RestartButton.gameObject.SetActive(true);

        SceneManager.LoadScene(index);
    }

    public void LoadMenu()
    {
        index = 0;
        SceneManager.LoadScene(index);
        StartButton.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(false);
    }
}
