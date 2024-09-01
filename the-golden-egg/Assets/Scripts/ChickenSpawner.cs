using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawner : MonoBehaviour
{
    private bool isActive = false;
    private List<GameObject> chickens = new();
    public GameObject chicken;
    public float spawnInterval;
    private float spawnTimer = 0;
    public int maxSpawn = 30;

    // Start is called before the first frame update
    void Start()
    {
        ServiceManager.Instance.Get<OnEggTaken>().Subscribe(HandleEggTaken);
    }

    void OnDestroy() {
        ServiceManager.Instance.Get<OnEggTaken>().Unsubscribe(HandleEggTaken);
    }

    void HandleEggTaken() {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive) return;

        chickens.RemoveAll(x => !x);
        spawnTimer += Time.deltaTime;
        if(spawnTimer < spawnInterval) return;
        if(chickens.Count >= maxSpawn) return;
        spawnTimer = 0;

        chickens.Add(Instantiate(chicken, transform.position, Quaternion.identity));
    }
}
