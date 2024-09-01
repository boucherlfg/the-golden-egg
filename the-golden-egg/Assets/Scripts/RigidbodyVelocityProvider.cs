using UnityEngine;

public class RigibodyVelocityProvider : MonoBehaviour, IVelocityProvider
{
    [SerializeField]
    private Rigidbody2D _rigidBody;
    public Vector2 Velocity 
    {
        get => _rigidBody.velocity;
    }
}