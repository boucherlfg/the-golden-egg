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
    public int maximumClucking = 30;
    private static int clucking ;

    // Start is called before the first frame update
    void Start()
    {
        ServiceManager.Instance.Get<OnEggTaken>().Subscribe(HandleEggTaken);
        spawnTimer += Random.value * spawnInterval;
    }

    void OnDestroy() {
        ServiceManager.Instance.Get<OnEggTaken>().Unsubscribe(HandleEggTaken);
        clucking = 0;
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
        var chickenInstance = Instantiate(chicken, transform.position, Quaternion.identity);
        chickens.Add(chickenInstance);
        var audio = chickenInstance.GetComponent<AudioSource>();
        clucking ++;
        if(clucking >= maximumClucking) {
            audio.Stop();
            Destroy(audio);
        }
        else {
            audio.pitch += 0.25f * (Random.value - 0.5f);
        }
    }
}
