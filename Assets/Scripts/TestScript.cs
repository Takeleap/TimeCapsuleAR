using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using GoogleARCore;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public UnityEvent MouseDown;
    public GameObject stateBihar, nalandhaIcon;
    bool isUp = false;
    public GameObject nalandha;
    public static GameObject stateObject;
    ARCoreController arCoreInstance;
    float camPosition = 0.0f;
    float distance = 0.0f;
    Vector3 mapPosition;
    public Sprite nalandhaBefore, nalandhaAfter;
    public static TestScript instance;
    [Header("ObjectMovement")]
    public Transform cameraTransform;
    public float distanceFromCamera = 1f;
    public GameObject sliderTime;
    public static string CurrentID;
    public GameObject SariputtaStupaInfo;
    public GameObject[] infoandAudio;
    enum POIName { POI1 = 1, POI2 = 2 };
    public Vector2 sliderValues;
    public GameObject POINameHeader;
    public Text poiNameText;
    public GameObject poiNameObj, siteNameObj;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.LogError("State Name : " + ARCoreController.anchor.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject);
        arCoreInstance = ARCoreController.instance;
        //stateBihar = ARCoreController.anchor.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            print("Null");
            poiNameText = GameManager.instance.poiNameText;
            poiNameObj = GameManager.instance.poiNameObj;
            siteNameObj = GameManager.instance.siteNameObj;
        }
        else
        {
            instance.stateBihar = this.stateBihar;
            poiNameText = GameManager.instance.poiNameText;
            poiNameObj = GameManager.instance.poiNameObj;
            siteNameObj = GameManager.instance.siteNameObj;
            print("Not Null");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void TestMethod()
    {
        Debug.LogError("Hello!!!!");
    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        MouseDown.Invoke();
        // print("Bihar");
    }

    public void Nalandha()
    {
        if (isUp)
        {
            print("Nalandha");
            //ARCoreController.anchor.transform.GetChild(0).gameObject.SetActive(false);
            /* Destroying anchor child */
            print(ARCoreController.anchor.transform.GetChild(0).gameObject.name);
            //Reset();
            Destroy(ARCoreController.anchor.transform.GetChild(0).gameObject);
            nalandha.gameObject.SetActive(true);
            if (stateObject == null)
            {
                print("State Object Null" + nalandha.name);
                stateObject = Instantiate(nalandha, ARCoreController.hit.Pose.position, ARCoreController.hit.Pose.rotation);
                stateObject.transform.Rotate(0, 180.0f, 0, Space.Self);
                float yAxisHeight = stateObject.transform.position.y + 1f;
                print("yAxisHeight : " + yAxisHeight);
                stateObject.transform.position = new Vector3(stateObject.transform.position.x, yAxisHeight, stateObject.transform.position.z);
                stateObject.transform.parent = ARCoreController.anchor.transform;
                if (ARCoreController.instance.scanningUI.transform.GetChild(1).gameObject.activeSelf &&
                    ARCoreController.instance.instructions.transform.GetChild(2).gameObject.activeSelf)
                {
                    ARCoreController.instance.scanningUI.transform.GetChild(1).gameObject.SetActive(false);
                    ARCoreController.instance.instructions.transform.GetChild(2).gameObject.SetActive(false);
                }
            }
            else
            {
                print("State Object Not Null");
                ARCoreController.anchor.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
    public void BiharState()
    {
        if (mapPosition != null)
        {
            print("MapPosition is Null");
            //mapPosition = ARCoreController.anchor.transform.GetChild(0).gameObject.transform.position;
        }
        if (!isUp)
        {
            print(stateBihar.transform.position.y);
            stateBihar.transform.DOLocalMoveY(0.096f, 0.5f).OnComplete(() => tweener(true));
            //ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalMoveZ(-0.700f, 0.5f);
            ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalMoveY(0.700f, 0.5f);
            ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalRotate(
                new Vector3(25.0f, 180f,
                ARCoreController.anchor.transform.GetChild(0).gameObject.transform.localRotation.z), 0.05f);
            nalandhaIcon.GetComponent<SpriteRenderer>().sprite = nalandhaAfter;
        }
        else
        {
            stateBihar.transform.DOLocalMoveY(0.05000001f, 0.5f).OnComplete(() => tweener(false));
            //ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalMoveZ(mapPosition.z, 0.5f);
            ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalMoveY(0f, 0.5f);
            ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalRotate(
                    new Vector3(0f, 180f,
                    ARCoreController.anchor.transform.GetChild(0).gameObject.transform.localRotation.z), 0.05f);
            nalandhaIcon.GetComponent<SpriteRenderer>().sprite = nalandhaBefore;
        }
    }

    void tweener(bool val)
    {
        isUp = val;
    }

    public void Reset()
    {
        ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalMoveZ(-0.700f, 0.5f);
        ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalMoveY(0.700f, 0.5f);
        ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalRotate(
            new Vector3(25.0f, 180f,
            ARCoreController.anchor.transform.GetChild(0).gameObject.transform.localRotation.z), 0.05f);
        nalandhaIcon.GetComponent<SpriteRenderer>().sprite = nalandhaAfter;
    }

    public void TemplePointOfInterest(string poiName)
    {
        if (stateObject != null)
        {
            siteNameObj.SetActive(false);
            poiNameObj.SetActive(true);
            print(" siteNameObj : " + siteNameObj.activeSelf + "    , poiNameObj  : " + poiNameObj.activeSelf);
            GameManager.instance.timeSlider.gameObject.SetActive(true);
            GameManager.instance.timeSlider.value = sliderValues.y;
            GameManager.instance.timeSlider.minValue = sliderValues.x;
            GameManager.instance.timeSlider.maxValue = sliderValues.y;
            CurrentID = poiName;
            Vector3 lookAtPos = Camera.main.transform.position;
            lookAtPos.y = stateObject.transform.position.y;
            stateObject.transform.DOLookAt(lookAtPos, 0.75f);
            InfoandAudio(poiName);
            NalandhaObjectsHidingScript.instance.ObjectSetActive(poiName);
            //InfoandAudio(poiName);
        }
    }

    public void InfoandAudio(string poiName)
    {
        switch (poiName)
        {
            case "Temple_3_New":
                poiNameText.text = "Temple 3";
                break;
            case "T12_New":
                poiNameText.text = "Temple 2";
                break;
            case "M1_New":
                poiNameText.text = "Monastery 1";
                break;
            case "M1A_New":
                poiNameText.text = "Monastery 1A";
                break;
            case "M1B_New":
                poiNameText.text = "Monastery 1B";
                break;
            case "M4_New":
                poiNameText.text = "Monastery 4";
                break;
            case "M6_New":
                poiNameText.text = "Monastery 6";
                break;
            case "M7_New":
                poiNameText.text = "Monastery 7";
                break;
            case "M8_New":
                poiNameText.text = "Monastery 8";
                break;
            case "M9_New":
                poiNameText.text = "Monastery 9";
                break;
            case "M10_New":
                poiNameText.text = "Monastery 10";
                break;
            case "M11_New":
                poiNameText.text = "Monastery 11";
                break;
            case "T13_New":
                poiNameText.text = "Temple 13";
                break;
            case "T14_New":
                poiNameText.text = "Temple 14";
                break;

        }
    }
}
