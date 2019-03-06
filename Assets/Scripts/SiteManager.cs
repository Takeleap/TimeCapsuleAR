using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteManager : MonoBehaviour
{
    public static SiteManager instance = null;
    public Transform[] sliderPlane;
    public InfoManager currentInfoManager;
    private GameManager gameManager;

    private AudioSource audioSource;

    void Start()
    {
        gameManager = GameManager.instance;
        gameManager.sliderPlane = sliderPlane;

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio (AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
