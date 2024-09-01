using System.Collections;
using TMPro;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public TMP_Text label;
    public bool secretWall = false;
    private bool canUse = false;
    public bool open = false;
    public Sprite openDoor;
    public Sprite closedDoor;
    void Start() {
        ServiceManager.Instance.Get<OnEggTaken>().Subscribe(HandleEggTaken);
    }

    void HandleEggTaken() {
        
        ServiceManager.Instance.Get<OnEggTaken>().Unsubscribe(HandleEggTaken);
        open = !open;
        canUse = true;
    }

    void Update() {
        GetComponent<SpriteRenderer>().sprite = open ? openDoor : closedDoor;
        if(TryGetComponent(out Collider2D collider)) collider.isTrigger = open;
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if(secretWall) return;
        if(!other.TryGetComponent(out MoveScript player)) return;
        if(!canUse) {
            StartCoroutine(ShowMessage());
            return;
        }
        if(!open) return;
        ServiceManager.Instance.Get<OnExit>().Invoke();
    }
    IEnumerator ShowMessage() {
        label.text = "You haven't found any treasure yet!";
        label.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        label.gameObject.SetActive(false);
    }
}
