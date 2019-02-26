using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinchScale : MonoBehaviour
{
    Touch touchZero, touchOne;
    Vector3 touchZeroPrevPos, touchOnePrevPos;
    float perspectiveZoomSpeed;
    float deltaMagnitudeDiff;
    public Vector3 scale;
    public Vector3 maxScale;
    public Vector3 minScale;
    public float min, max;
    public Text debugText;
    private bool maxscale;
     private float prevPinchDistance = 0;
    private float currentPinchDistance = 0;
    private float scaleMultiplier = 0.001f;
    private float maxPrefabValue =  0.094f;

    void Start()
    {
        perspectiveZoomSpeed = 0.01f;
        scale = this.transform.localScale;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);
            if (secondTouch.phase == TouchPhase.Began)
            {
                prevPinchDistance = Vector2.Distance(firstTouch.position, secondTouch.position);
            }
            else
            {
                currentPinchDistance = Vector2.Distance(firstTouch.position, secondTouch.position);
                float scaleToAdd = (currentPinchDistance - prevPinchDistance) * scaleMultiplier;
                scaleToAdd = scaleToAdd + transform.localScale.x;
                scaleToAdd = Mathf.Clamp(scaleToAdd, 0.03f, 6f);
                if(this.transform.localScale.x < maxPrefabValue)
                {
                    transform.localScale = new Vector3(scaleToAdd, scaleToAdd, scaleToAdd);
                }
                prevPinchDistance = currentPinchDistance;
            }
        }
    }

    void Scale(Vector3 scale)
    {
        transform.localScale += scale;
    }
}
