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
                print("State Object Null");
                stateObject = Instantiate(nalandha, ARCoreController.hit.Pose.position, ARCoreController.hit.Pose.rotation);
                stateObject.transform.Rotate(0, 180.0f, 0, Space.Self);
                stateObject.transform.position = new Vector3(2.1f, -29f, 66f);
                stateObject.transform.parent = ARCoreController.anchor.transform;
                if (ARCoreController.instance.scanningUI.transform.GetChild(1).gameObject.activeSelf &&
                    ARCoreController.instance.instructions.transform.GetChild(2).gameObject.activeSelf)
                {
                    ARCoreController.instance.scanningUI.transform.GetChild(1).gameObject.SetActive(false);
                    ARCoreController.instance.instructions.transform.GetChild(2).gameObject.SetActive(false);
                }
                //StopAllCoroutines();
                //StartCoroutine("RotationMethod");
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
        if (!isUp)
        {
            print(stateBihar.transform.position.y);
            stateBihar.transform.DOLocalMoveY(0.096f, 0.5f).OnComplete(() => tweener(true));
            mapPosition = ARCoreController.anchor.transform.position;
            ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalMoveZ(-0.700f, 0.5f);
            ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalMoveY(0.700f, 0.5f);
            ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalRotate(
                new Vector3(25.0f, 180f,
                ARCoreController.anchor.transform.GetChild(0).gameObject.transform.localRotation.z), 0.05f);
            nalandhaIcon.GetComponent<SpriteRenderer>().sprite = nalandhaAfter;
        }
        else
        {
            stateBihar.transform.DOLocalMoveY(0.05000001f, 0.5f).OnComplete(() => tweener(false));
            ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalMoveZ(0.700f, 0.5f);
            ARCoreController.anchor.transform.GetChild(0).gameObject.transform.DOLocalMoveY(-0.700f, 0.5f);
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

    public void TemplePointOfInterest()
    {
        if (stateObject != null)
        {
            stateObject.transform.DORotate(new Vector3(0f, 60.709f, 0f), 0.75f);
        }
    }

}
