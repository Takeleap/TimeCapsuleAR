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

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            int i = 0;
            foreach (GameObject item in nalandhaObjects)
            {
                item.transform.DOScale(modelSize[i], 1f);
                i++;
            }
            modelSize.Clear();
        }
    }

    public void ObjectSetActive(string buildingID)
    {
        print(buildingID + "_OLD");
        foreach (GameObject item in nalandhaObjects)
        {
            if (item.name == buildingID || item.name ==buildingID+"_OLD")
            {
                print("True");
                item.SetActive(true);
                modelSize.Add(item.transform.localScale);
            }
            else
            {
                print("False");
                //item.SetActive(false);
                item.transform.DOScale(0f, 1f);
                modelSize.Add(item.transform.localScale);
            }
        }
    }

    void ResetObjects()
    {
        int i = 0;
        foreach (GameObject item in nalandhaObjects)
        {
            item.transform.DOScale(modelSize[i], 1f);
            i++;
        }
        modelSize.Clear();
    }
}
