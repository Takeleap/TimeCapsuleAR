using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public SiteManager siteManager;
    public GameObject[] infoObjects;

    public void TurnOnInfoObjects ()
    {
        if (siteManager.currentInfoManager != null)
            siteManager.currentInfoManager.TurnOffInfoObjects();

        siteManager.currentInfoManager = this;

        foreach(GameObject info in infoObjects)
        {
            info.SetActive(true);
        }
    }

    public void TurnOffInfoObjects()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}