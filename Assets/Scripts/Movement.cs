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
        float movementValue = move.ReadValue<float>();
        if (movementValue == -1) {
            MoveLeft(-1);
        }
        else if (movementValue == 1) {
            MoveRight(1);
        }
        if (jump.IsPressed() && isGrounded)
            Jump();
    }

    private void MoveRight(float direction) { // direction = 1
        Debug.Log("Right movement !");
        rb.linearVelocity = new Vector3(direction * movementSpeed, rb.linearVelocity.y, 0);
    }

    private void MoveLeft(float direction) { // direction = -1
        Debug.Log("Left movement !");
        rb.linearVelocity = new Vector3(direction * movementSpeed, rb.linearVelocity.y, 0);
    }

    private void Jump() {
        Debug.Log("Jump!");
        rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
    }
}
