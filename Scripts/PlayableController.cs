using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class PlayableController : MonoBehaviour
{
    public PlayableDirector director;
    public UIController uiController;
    public string[] guideList = new string[10];

    void Start()
    {
        director = GetComponent<PlayableDirector>();
        uiController = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
    }

    public void Play()
    {
        uiController.OpenStoryBoard(true);
        director.Play();
    }

    public void Resume()
    {
        uiController.OpenStoryBoard(true);
        director.Resume();
    }

    public void Pause()
    {
        uiController.OpenStoryBoard(false);
        director.Pause();
    }

    public void ChangeGuideText(int index)
    {
        uiController.ChangeGuideText(guideList[index]);
    }

}
