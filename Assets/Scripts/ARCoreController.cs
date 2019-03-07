using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;
#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class ARCoreController : MonoBehaviour
{
    public static ARCoreController instance = null;
    public GameObject mapPrefab;
    public GameObject FirstPersonCamera;
    public static bool isInstanciated = false;
    public GameObject DetectedPlanePrefab;
    /// The rotation in degrees need to apply to model when the Andy model is placed.
    private const float k_ModelRotation = 180.0f;
    private List<DetectedPlane> m_NewPlanes = new List<DetectedPlane>();
    GameObject planeObject;
    List<GameObject> detectedPlanesList = new List<GameObject>();
    public static TrackableHit hit;
    public static GameObject mapObject;
    public static Anchor anchor;
    public static Vector3 hitPos;
    public static Quaternion hitRot;
    bool isScanInsClosed = false;
    [Header("ScanningUI")]
    public GameObject instructions;
    public GameObject scanningUI;
    public GameObject placesIndia;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instructions.transform.GetChild(0).gameObject.SetActive(true);
        scanningUI.transform.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (detectedPlanesList.Count > 0 && !isScanInsClosed)
        {
            try
            {
                instructions.transform.GetChild(0).gameObject.SetActive(false);
                scanningUI.transform.GetChild(0).gameObject.SetActive(false);
                instructions.transform.GetChild(1).gameObject.SetActive(true);
                isScanInsClosed = true;
            }
            catch (System.Exception excep)
            {

                print(excep.Message);
            }
        }
        else
        {
        }
        // Check that motion tracking is tracking.
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }
        // Iterate over planes found in this frame and instantiate corresponding GameObjects to visualize them.
        Session.GetTrackables<DetectedPlane>(m_NewPlanes, TrackableQueryFilter.New);
        for (int i = 0; i < m_NewPlanes.Count; i++)
        {
            if (ARCoreController.isInstanciated == false)
            {
                // Instantiate a plane visualization prefab and set it to track the new plane. The transform is set to
                // the origin with an identity rotation since the mesh for our prefab is updated in Unity World
                // coordinates.
                planeObject = Instantiate(DetectedPlanePrefab, Vector3.zero, Quaternion.identity, transform);
                detectedPlanesList.Add(planeObject);
                planeObject.GetComponent<DetectedPlaneVisualizer>().Initialize(m_NewPlanes[i]);
            }
        }

        // If the player has not touched the screen, we are done with this update.
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        // Raycast against the location the player touched to search for planes.
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if ((hit.Trackable is DetectedPlane) &&
                Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                    hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                if (isInstanciated == false)
                {
                    SetMap();
                }
            }
        }
    }

    public void SetMap()
    {
        isInstanciated = true;
        instructions.transform.GetChild(1).gameObject.SetActive(false);

        print("MapPrefab Name : " + mapPrefab.name);

        // Instantiate Andy model at the hit pose.
        mapObject = Instantiate(mapPrefab, hit.Pose.position, hit.Pose.rotation);

        // Compensate for the hitPose rotation facing away from the raycast (i.e. camera).
        mapObject.transform.Rotate(0, k_ModelRotation, 0, Space.Self);

        // Reducing the scale of the instanciated object
        //mapObject.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        hitRot = hit.Pose.rotation;
        hitPos = hit.Pose.position;
        Debug.LogError("Hit.Pose.position : " + hit.Pose.position);
        Debug.LogError("Hit.Pose.rotation : " + hit.Pose.rotation);
        Debug.LogError("mapObject : " + mapObject.transform.position);

        //// To rotate the map in x axis
        //mapObject.transform.Rotate(-180f, 0f, 180f);
        // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
        // world evolves.
        anchor = hit.Trackable.CreateAnchor(hit.Pose);
        instructions.transform.GetChild(2).gameObject.SetActive(true);
        scanningUI.transform.GetChild(1).gameObject.SetActive(true);

        // Make Andy model a child of the anchor.
        mapObject.transform.parent = anchor.transform;
        placesIndia.SetActive(true);
        if (detectedPlanesList.Count > 0)
        {
            foreach (GameObject planes in detectedPlanesList)
            {
                planes.SetActive(false);
            }
        }
    }
}
