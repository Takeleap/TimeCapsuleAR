using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    public  VideoPlayer videoPlayer;
    public  Image       loadingBarLeft;
    public  GameObject  loadingScreen;

    private bool videoCompleted = true;

    void Start()
    {
        videoPlayer.prepareCompleted += SetVideoPlayer;
    }

    void SetVideoPlayer (VideoPlayer vp)
    {
        videoPlayer.Play();
        Invoke("SetComplete", 1f);
    }

    void SetComplete ()
    {
        videoCompleted = false;
    }

    void Update ()
    {
        if(!videoPlayer.isPlaying)
        {
            if (!videoCompleted)
            {
                videoCompleted = true;
                VideoCompleted();
            }
        }
    }

    void VideoCompleted ()
    {
        loadingScreen.SetActive(true);
        loadingBarLeft.transform.DOScaleX(8.0f, 2f).OnComplete(() => TweenerComplete());
        //DOTween.To(() => loadingBarLeft.fillAmount, x => loadingBarLeft.fillAmount = x, 1, 3).SetEase(Ease.Linear).OnComplete(
        //() =>
        //{
        //    SceneManager.LoadScene(1);
        //});
    }

    void TweenerComplete()
    {
        print("Calling Tweener Complete");
       loadingBarLeft.transform.localScale = new Vector3(0f,1f,1f);
        SceneManager.LoadScene(1);
    }
}
