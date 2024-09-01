using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeIntensity = 0.02f;
    bool doShake;
    // Start is called before the first frame update
    void Start()
    {
        ServiceManager.Instance.Get<OnEggTaken>().Subscribe(HandleEggTaken);
    }

    private void HandleEggTaken()
    {
        ServiceManager.Instance.Get<OnEggTaken>().Unsubscribe(HandleEggTaken);
        doShake = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!doShake) return;
        transform.position += (Vector3)Random.insideUnitCircle * shakeIntensity;
    }
}
