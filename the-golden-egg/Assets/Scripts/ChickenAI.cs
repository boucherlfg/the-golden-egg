using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class ChickenAI : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;
    private MoveScript toFollow;
    [SerializeField]
    AIPath pathfinder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        toFollow = toFollow ? toFollow : FindObjectOfType<MoveScript>();

        if(!toFollow) return;
        pathfinder.maxSpeed = speed;

        pathfinder.destination = toFollow.transform.position + (Vector3)Random.insideUnitCircle;
    }
}
