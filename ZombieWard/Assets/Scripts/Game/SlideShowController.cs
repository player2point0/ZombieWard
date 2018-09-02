using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SlideShowController : MonoBehaviour
{
    public Sprite[] images;
    public float SlideDelay;

    private Image UIImage;
    private SceneController sceneController;
    private int index;

	void Start ()
    {
        sceneController = FindObjectOfType<SceneController>();
        UIImage = GetComponentInChildren<Image>();
        index = 0;
        InvokeRepeating("NextSlide", SlideDelay, SlideDelay);
	}

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            CancelInvoke("NextSlide");
            NextSlide();
            InvokeRepeating("NextSlide", SlideDelay, SlideDelay);
        }
    }

    void NextSlide()
    {
        index++;

        if (index > images.Length - 1) sceneController.LoadNextLevel();

        else
        {
            Debug.Log(index);
            UIImage.sprite = images[index];
        }
    }
}
