using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChickenCounter : MonoBehaviour
{
    private bool isDead;
    [SerializeField]
    private float maxChickenDistance;
    [SerializeField]
    private SpriteRenderer screen;
    [SerializeField]
    private MoveScript move;
    [SerializeField]
    int maxChickenCount = 5;
    // Start is called before the first frame update
    void Start()
    {
        ServiceManager.Instance.Get<OnDeath>().Subscribe(HandleDeath);
    }

    void HandleDeath(){
        isDead = true;
        ServiceManager.Instance.Get<OnDeath>().Unsubscribe(HandleDeath);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;
        var hits = Physics2D.OverlapCircleAll(transform.position, maxChickenDistance).Count(x => x.GetComponent<ChickenAI>());
        if(hits >= maxChickenCount) 
        {
            ServiceManager.Instance.Get<OnDeath>().Invoke();
            gameObject.SetActive(false);
        }
        
        var color = screen.color;
        color.a = ((float)hits) / maxChickenCount;
        screen.color = color;

        move.Speed = move.MaxSpeed * (1 - (hits / maxChickenCount));
    }
}
