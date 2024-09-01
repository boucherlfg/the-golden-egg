using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryPanel : MonoBehaviour
{
    private bool working = false;
    int index = 0;
    [SerializeField]
    private GameObject[] pages;

    void OnEnable() {
        pages[index].SetActive(false);
        index = 0;
        pages[index].SetActive(true);
    }

    public void ToPage(int page) {
        if(working) return;
        StartCoroutine(GoToPage(page));
    }
    public void Next() {
        ToPage(index + 1);
    }
    public void Previous() {
        ToPage(index - 1);
    }

    IEnumerator GoToPage(int page) {
        working = true;
        for(float i = 1; i > 0; i -= Time.deltaTime) {
            pages[index].GetComponent<CanvasGroup>().alpha = i;
            yield return null;
        }
        pages[index].SetActive(false);
        index = page;
        if(index < 0) index = 0;
        if(index >= pages.Length) {
             index = pages.Length - 1;
             gameObject.SetActive(false);
        }

        pages[index].SetActive(true);
        for(float i = 1; i > 0; i -= Time.deltaTime) {
            pages[index].GetComponent<CanvasGroup>().alpha = 1 - i;
            yield return null;
        }
        working = false;
    }
}
