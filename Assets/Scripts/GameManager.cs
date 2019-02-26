using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Button backButton;
    ARCoreController arCoreInstance;
    GameObject mapObject;
    public GameObject map;
    public GameObject[] listArray;
    public GameObject siteName;
    public GameObject poiObject;
    public Sprite[] poiSprites;
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
            print("Running");
        }
        else
        {
            print("Destroying");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
    }

    public void BackButton()
    {
        // ARCoreController.anchor.transform.GetChild(0).gameObject.SetActive(true);
        // TestScript.stateObject.SetActive(false);
        /*Creating Map instance again */
        print("State Object Null");
        print(ARCoreController.anchor.transform.GetChild(0).gameObject.name);
        Destroy(ARCoreController.anchor.transform.GetChild(0).gameObject);
        mapObject = Instantiate(map, ARCoreController.hit.Pose.position, ARCoreController.hit.Pose.rotation);
        mapObject.transform.Rotate(new Vector3(0f, 180f, 0f));
        mapObject.transform.parent = ARCoreController.anchor.transform;

        print("scanningUI : " + ARCoreController.instance.scanningUI.transform.GetChild(1).gameObject.activeSelf);
        print("instructions : " + ARCoreController.instance.instructions.transform.GetChild(2).gameObject.activeSelf);
        if (ARCoreController.instance.scanningUI.transform.GetChild(1).gameObject.activeSelf == false &&
   ARCoreController.instance.instructions.transform.GetChild(2).gameObject.activeSelf == false)
        {
            ARCoreController.instance.scanningUI.transform.GetChild(1).gameObject.SetActive(true);
            ARCoreController.instance.instructions.transform.GetChild(2).gameObject.SetActive(true);
        }
    }


    public void ListOfSitesIndia()
    {
        if (TestScript.instance != null)
        {
            Debug.LogError("Not Null");
            TestScript.instance.BiharState();
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
}
