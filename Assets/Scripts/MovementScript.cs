using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce  =10f;
    [SerializeField] private float slideDuration = .5f;
    [SerializeField] private float laneSwitchSpeed = 15f;
    [Header("World Settings")]
    [SerializeField] private float laneDistance = 2f;
    
    private Vector3 moveDirection;
    private CharacterController controller;
    private Animator anim;
    private bool isSliding;

    private int targetLane = 1;
    private Coroutine laneSwitchCoroutine;

    #region InputRegion
    #pragma warning disable IDE0051
    private void OnMove(InputValue _input)
    {
        int direction = Mathf.Clamp(Mathf.RoundToInt(_input.Get<Vector2>().x), -1, 1);
        targetLane += direction;
        targetLane = Mathf.Clamp(targetLane, 0, 2);
        if (laneSwitchCoroutine != null)
        {
            StopCoroutine(laneSwitchCoroutine);
        }
        laneSwitchCoroutine = StartCoroutine(SwitchLane(targetLane));
    }
    private void OnJump()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = jumpForce;
            anim.SetTrigger("Jump");
        }
    }

    private void OnSlide()
    {
        if (controller.isGrounded && !isSliding)
        {
            StartCoroutine(slide());
        }
    }
    #pragma warning restore IDE0051
    #endregion

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        moveDirection = speed * Time.deltaTime * Vector3.forward;
        moveDirection.y+=Physics.gravity.y*Time.deltaTime;
        controller.Move(moveDirection);
    }
/// <summary>
/// This Coroutine makes the player slide for a set duration
/// </summary>
/// <returns></returns>
    private IEnumerator slide()
    {
        isSliding = true;
        anim.SetBool("IsSliding", isSliding);
        yield return new WaitForSeconds(slideDuration);
        isSliding = false;
        anim.SetBool("IsSliding", isSliding);
    }
    /// <summary>
    /// This Coroutine makes the player switch lanes.
    /// </summary>
    /// <param name="_lane">Lane to switch to</param>
    /// <returns></returns>
    private IEnumerator SwitchLane(int _lane)
    {
        float posX = _lane * laneDistance - laneDistance; 
        Vector3 targetPosition = new (posX, 0, 0);

        while (Mathf.Abs(transform.position.x - targetPosition.x) > 0.1f)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, new (targetPosition.x, transform.position.y, transform.position.z), laneSwitchSpeed * Time.deltaTime);
            controller.Move(newPosition - transform.position);
            yield return null;
        }
    }
}