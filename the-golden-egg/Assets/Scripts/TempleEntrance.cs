using UnityEngine;
using UnityEngine.Events;

public class TempleEntrance : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.GetComponent<MoveScript>()) return;
        ServiceManager.Instance.Get<TempleEntered>().Invoke();
        gameObject.SetActive(false);
    }
}