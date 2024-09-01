using UnityEngine;

public class MoveScript : MonoBehaviour {
    [SerializeField]
    private Rigidbody2D _rigidBody;
    [SerializeField]
    private float speed = 3;

    private void Update() {
        var move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _rigidBody.velocity = move * speed;
    }
}