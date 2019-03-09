﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Transform[] sliderPlane;
    public Slider timeSlider;
    public Button backButton;
    ARCoreController arCoreInstance;
    GameObject mapObject;
    public GameObject map;
    public GameObject[] listArray;
    public GameObject siteName;
    public GameObject poiObject;
    public Sprite[] poiSprites;
    public GameObject poiNameObj, siteNameObj;
    public Text poiNameText;
    public NalandhaObjectsHidingScript nalandhaObjectsInstance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            nalandhaObjectsInstance = NalandhaObjectsHidingScript.instance;
            print("Running");
        }
        else
        {
            print("Destroying");
            Destroy(gameObject);
        }
    }

    public void OnSliderChangedValue()
    {
        if (sliderPlane != null)
        {
            switch (TestScript.CurrentID)
            {
                case "Temple_3_New":
                    sliderPlane[0].localPosition = new Vector3(sliderPlane[0].localPosition.x, timeSlider.value, sliderPlane[0].localPosition.z);
                    break;
                case "T12_New":
                    sliderPlane[1].localPosition = new Vector3(sliderPlane[1].localPosition.x, timeSlider.value, sliderPlane[1].localPosition.z);
                    break;
                case "M1_New":
                case "M1A_New":
                case "M1B_New":
                case "M4_New":
                case "M6_New":
                case "M7_New":
                case "M8_New":
                case "M9_New":
                case "M10_New":
                case "M11_New":
                    sliderPlane[2].localPosition = new Vector3(sliderPlane[2].localPosition.x, timeSlider.value, sliderPlane[2].localPosition.z);
                    break;
                case "T13_New":
                    sliderPlane[3].localPosition = new Vector3(sliderPlane[3].localPosition.x, timeSlider.value, sliderPlane[3].localPosition.z);
                    break;
                case "T14_New":
                    sliderPlane[4].localPosition = new Vector3(sliderPlane[4].localPosition.x, timeSlider.value, sliderPlane[4].localPosition.z);
                    break;
            }
        }

    }

    void Update()
    {
        if (ARCoreController.anchor != null)
        {
            if (ARCoreController.anchor.transform.childCount > 0)
            {
                if (ARCoreController.anchor.transform.GetChild(0).gameObject.name != "INDIA States(Clone)")
                {
                    backButton.gameObject.SetActive(true);
                    siteName.SetActive(true);
                    poiObject.GetComponent<Image>().sprite = poiSprites[1];
                }
                else
                {
                    backButton.gameObject.SetActive(false);
                    siteName.SetActive(false);
                    poiObject.GetComponent<Image>().sprite = poiSprites[0];
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ARCoreController.anchor.transform.GetChild(0).gameObject.name == "INDIA States(Clone)")
            {
                Application.Quit();
            }
        }

    }

    public void BackButton()
    {
        // ARCoreController.anchor.transform.GetChild(0).gameObject.SetActive(true);
        // TestScript.stateObject.SetActive(false);
        /*Creating Map instance again */
        print("State Object Null");
        print(ARCoreController.anchor.transform.GetChild(0).gameObject.name);
        Destroy(ARCoreController.anchor.transform.GetChild(0).gameObject);
        Debug.LogError("ARCoreController.hit.Pose.position : " + ARCoreController.hitPos);
        Debug.LogError("ARCoreController.hit.Pose.rotation : " + ARCoreController.hitRot);
        mapObject = Instantiate(map, ARCoreController.hitPos, ARCoreController.hitRot);
        Debug.LogError("mapObject : " + mapObject.transform.position);
        mapObject.transform.Rotate(0, 180f, 0, Space.Self);
        mapObject.transform.parent = ARCoreController.anchor.transform;
        print("scanningUI : " + ARCoreController.instance.scanningUI.transform.GetChild(1).gameObject.activeSelf);
        print("instructions : " + ARCoreController.instance.instructions.transform.GetChild(2).gameObject.activeSelf);
        if (ARCoreController.instance.scanningUI.transform.GetChild(1).gameObject.activeSelf == false &&
   ARCoreController.instance.instructions.transform.GetChild(2).gameObject.activeSelf == false)
        {
            ARCoreController.instance.scanningUI.transform.GetChild(1).gameObject.SetActive(true);
            ARCoreController.instance.instructions.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (TestScript.instance.sliderTime.activeSelf)
        {
            TestScript.instance.sliderTime.SetActive(false);
        }
    }


    public void ListOfSitesIndia()
    {
        if (TestScript.instance != null)
        {
            Debug.LogError("Not Null");
            //TestScript.instance.BiharState();
            ARCoreController.anchor.transform.GetComponentInChildren<TestScript>().BiharState();
        }
        else
        {
            Debug.LogError("Null");
            ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalMoveZ(-0.700f, 0.5f);
            ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalMoveY(0.700f, 0.5f);
            ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalRotate(
                new Vector3(25.0f, 180f,
                ARCoreController.anchor.transform.GetChild(0).gameObject.transform.localRotation.z), 0.05f);
            ARCoreController.anchor.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform.DOLocalMoveY(0.096f, 0.5f);
        }
    }

    public void ListViewButton()
    {
        if (ARCoreController.anchor.transform.GetChild(0).gameObject.name != "INDIA States(Clone)")
        {
            listArray[1].SetActive(true);
            listArray[0].SetActive(false);
        }
        else
        {
            listArray[0].SetActive(true);
            listArray[1].SetActive(false);
        }
    }

    public void ScreenShot()
    {
        string format = "Mddyyyyhhmmsstt";
        string _imagename = String.Format("{0}.jpg", DateTime.Now.ToString(format));
        Debug.LogError(_imagename);
        ScreenCapture.CaptureScreenshot(_imagename + ".jpeg");
    }

    //public void SaveImageToPublicFolder(byte[] byteBuffer, string fileName)
    //{
    //    new MediaLibrary().SavePicture(fileName, byteBuffer);
    //}

    public void ObjectReset()
    {
        NalandhaObjectsHidingScript.instance.ResetObjects();
    }


    public void CloseInfoButton()
    {
        if(poiNameObj.activeSelf)
        {

        }
        else
        {
            siteNameObj.SetActive(true);
        }
    }
}
