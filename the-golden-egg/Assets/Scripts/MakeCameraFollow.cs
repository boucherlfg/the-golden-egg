using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCameraFollow : MonoBehaviour
{
    [SerializeField]
    float quantity = 5;
    Camera _mainCam;

    // Update is called once per frame
    void Update()
    {
        _mainCam = _mainCam ? _mainCam : Camera.main;
        var pos = _mainCam.transform.position;
        pos = Vector2.Lerp(pos, transform.position, Time.deltaTime * quantity);
        pos.z = -10;
        _mainCam.transform.position = pos;
    }
}
