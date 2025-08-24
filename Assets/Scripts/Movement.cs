using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] InputAction jump;
    [SerializeField] InputAction move;
    [SerializeField] float movementSpeed = 10.0f;
    [SerializeField] float jumpStrength = 10.0f;

    Rigidbody rb;
    bool isGrounded = false;

    private void OnEnable() {
        jump.Enable();
        move.Enable();
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
    }


    // Allowing the player to jump only if it is on Ground
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = true;
            Debug.Log("Collided !");
        }
    }
    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = false;
            Debug.Log("Exited !");
        }
    }


    private void FixedUpdate() {

        rb.freezeRotation = true;

        float movementValue = move.ReadValue<float>();
        if (movementValue == -1) {
            // Left movement
            Move(-1);
        }
        else if (movementValue == 1) {
            // Right movement
            Move(1);
        }
        if (jump.IsPressed() && isGrounded) {
            // Jumping
            Jump();
        }
    }

    private void Move(float direction) { // direction = 1
        Debug.Log("Right movement !");
        if (direction == 1)
            rb.AddForce(Vector3.right * movementSpeed, ForceMode.Force);
        else if (direction == -1)
            rb.AddForce(Vector3.left * movementSpeed, ForceMode.Force);
    }

    private void Jump() {
        Debug.Log("Jump!");
        rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
    }
}
