using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class NalandhaObjectsHidingScript : MonoBehaviour
{
    public GameObject[] nalandhaObjects;
    public static NalandhaObjectsHidingScript instance;
    List<Vector3> modelSize = new List<Vector3>();
    public GameObject sliderBar;
    public GameObject poiName, siteName;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            sliderBar = TestScript.instance.sliderTime;
            poiName = GameManager.instance.poiNameObj;
            siteName = GameManager.instance.siteNameObj;
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
        //#if UNITY_EDITOR
        //        if (Input.GetKeyDown(KeyCode.A))
        //        {
        //            poiName.SetActive(false);
        //            siteName.SetActive(true);
        //            sliderBar.SetActive(false);
        //            int i = 0;
        //            foreach (GameObject item in nalandhaObjects)
        //            {
        //                item.transform.DOScale(modelSize[i], 1f);
        //                i++;
        //            }
        //            modelSize.Clear();
        //        }
        //#endif
        //        if (Input.GetKeyDown(KeyCode.Escape))
        //        {
        //            poiName.SetActive(false);
        //            siteName.SetActive(true);
        //            sliderBar.SetActive(false);
        //            int i = 0;
        //            foreach (GameObject item in nalandhaObjects)
        //            {
        //                item.transform.DOScale(modelSize[i], 1f);
        //                i++;
        //            }
        //            modelSize.Clear();
        //        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.BackButton();
        }
    }

    public void ObjectSetActive(string buildingID)
    {
        if (modelSize.Count > 0)
            ResetObjects();
        print(buildingID + "_OLD" + "modelSize.Count : "+ modelSize.Count);
        foreach (GameObject item in nalandhaObjects)
        {
            if (item.name == buildingID || item.name == buildingID + "_OLD")
            {
                modelSize.Add(item.transform.localScale);
            }
            else
            {
                item.transform.DOScale(0f, 1f);
                modelSize.Add(item.transform.localScale);
            }
        }
    }

    public void ResetObjects()
    {
        int i = 0;
        poiName.SetActive(false);
        siteName.SetActive(true);
        //sliderBar.SetActive(false);
        print("Resetting Objects");
        foreach (GameObject item in nalandhaObjects)
        {
            item.transform.DOScale(modelSize[i], 1f);
            i++;
        }
        //modelSize.Clear();
        Debug.LogError("List Count = " + modelSize.Count);
    }
}
