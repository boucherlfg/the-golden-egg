using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class ChickenAI : MonoBehaviour
{
    private bool end;
    [SerializeField]
    private float speed = 2;
    private MoveScript toFollow;
    [SerializeField]
    AIPath pathfinder;
    // Start is called before the first frame update
    void Start()
    {
        ServiceManager.Instance.Get<OnDeath>().Subscribe(HandleEnd);
        ServiceManager.Instance.Get<OnExit>().Subscribe(HandleEnd);
    }

    private void HandleEnd()
    {
        ServiceManager.Instance.Get<OnDeath>().Unsubscribe(HandleEnd);
        ServiceManager.Instance.Get<OnExit>().Unsubscribe(HandleEnd);
        end = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(end) return;
        toFollow = toFollow ? toFollow : FindObjectOfType<MoveScript>();

        if(!toFollow) return;
        pathfinder.maxSpeed = speed;

        pathfinder.destination = toFollow.transform.position + (Vector3)Random.insideUnitCircle;
    }
}

