using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using GoogleARCore;

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
        }
        else
        {
            instance.stateBihar = this.stateBihar;
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
            GameManager.instance.timeSlider.gameObject.SetActive(true);
            GameManager.instance.timeSlider.value = sliderValues.y;
            GameManager.instance.timeSlider.minValue = sliderValues.x;
            GameManager.instance.timeSlider.maxValue = sliderValues.y;
            CurrentID = poiName;
            Vector3 lookAtPos = Camera.main.transform.position;
            lookAtPos.y = stateObject.transform.position.y;
            stateObject.transform.DOLookAt(lookAtPos, 0.75f);
            print("Calling TemplePointOfInterest");
            NalandhaObjectsHidingScript.instance.ObjectSetActive(poiName);
            //InfoandAudio(poiName);
        }
    }

    public void InfoandAudio(string poiName)
    {

    }

    //public void SariputtaStupaInfoMethod()
    //{
    //    SariputtaStupaInfo.SetActive(true);
    //}
}
