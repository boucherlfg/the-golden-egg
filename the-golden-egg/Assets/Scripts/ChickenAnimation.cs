using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimation : MonoBehaviour
{
    private Vector2 lastVelocity;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    private IVelocityProvider velocityProvider;
    // Start is called before the first frame update
    void Start()
    {
        velocityProvider = GetComponent<IVelocityProvider>();
    }

    // Update is called once per frame
    void Update()
    {
        
        var velocity = velocityProvider.Velocity;

        if(Mathf.Abs(velocity.x) > 0.1f) {
            lastVelocity = velocity;
        }
        if(velocity.magnitude > 0.1f) {
            _animator.Play("Walk");
        }
        else {
            _animator.Play("Idle");
        }

        _spriteRenderer.flipX = lastVelocity.x < -0.1f;

    }
}
