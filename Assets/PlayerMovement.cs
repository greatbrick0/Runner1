using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int lane = 1;
    [SerializeField]
    int maxLanes = 3;
    [SerializeField]
    private float laneDistance = 1.5f;

    [SerializeField]
    private float jumpForce = 6;
    [SerializeField]
    private float strafeTime = 0.5f;

    private bool canJump = true;
    private bool canStrafe = true;

    private Rigidbody rb;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetKeyDown(KeyCode.A)) Strafe(-1);
        if (Input.GetKeyDown(KeyCode.D)) Strafe(1);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Jump()
    {
        if (!canJump) return;

        canJump = false;
        rb.velocity += Vector3.up * jumpForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void Strafe(int direction)
    {
        if (!canStrafe) return;
        if (lane + direction < 0 || lane + direction >= maxLanes) return;

        lane += direction;
        canStrafe = false;
        LeanTween.moveX(gameObject, transform.position.x + laneDistance * direction, strafeTime).setEaseInOutCubic();
        Invoke("FinishStrafe", strafeTime);
    }

    private void FinishStrafe()
    {
        canStrafe = true;
    }
}
