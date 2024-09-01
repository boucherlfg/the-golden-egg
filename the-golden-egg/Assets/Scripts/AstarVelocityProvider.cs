using Pathfinding;
using UnityEngine;

public class AstarVelocityProvider : MonoBehaviour, IVelocityProvider {
    [SerializeField]
    private AIPath pathfinder;
    public Vector2 Velocity 
    {
        get => pathfinder.velocity;
    }
}