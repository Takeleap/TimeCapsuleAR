using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using AdvancedDissolve_Example;

public class ShaderController : MonoBehaviour
{
    public  Controller_Mask_XYZ_Axis    controller;
    public  float endValue;

    void OnEnable()
    {
        DOTween.To(()=> controller.offset, x=> controller.offset = x, endValue, 3).SetDelay(1f);
    }
}
