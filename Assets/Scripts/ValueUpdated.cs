using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using AdvancedDissolve_Example;

[ExecuteInEditMode]
public class ValueUpdated : MonoBehaviour
{
    public Controller_Mask_XYZ_Axis controller1;
    public Controller_Mask_XYZ_Axis controller2;
    public float num;
    public float endValue;
    void Start()
    {

    }

    void Update()
    {
        controller1.offset = controller2.offset = num;
    }

    void OnEnable()
    {
        DOTween.To(() => num, x => num = x, endValue, 3).SetDelay(1f);
    }
}
