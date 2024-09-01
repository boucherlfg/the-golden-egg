using UnityEngine;

public class MoveScript : MonoBehaviour {
    [SerializeField]
    private Rigidbody2D _rigidBody;
    [SerializeField]
    private float maxSpeed = 2.25f;
    public float Speed {get => speed; set => speed = value; }
    public float MaxSpeed => maxSpeed;
    public float speed = 3;

    private void Update() {
        var move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _rigidBody.velocity = move * speed;
    }
}