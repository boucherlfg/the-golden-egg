using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject badEndingMenu;
    public GameObject goodEndingMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        ServiceManager.Instance.Get<OnDeath>().Subscribe(HandleDeath);
        ServiceManager.Instance.Get<OnExit>().Subscribe(HandleExit);
    }

    private void HandleExit()
    {
        ServiceManager.Instance.Get<OnExit>().Unsubscribe(HandleExit);
        goodEndingMenu.gameObject.SetActive(true);
    }

    private void HandleDeath()
    {
        ServiceManager.Instance.Get<OnDeath>().Unsubscribe(HandleDeath);
        badEndingMenu.gameObject.SetActive(true);
    }
}
