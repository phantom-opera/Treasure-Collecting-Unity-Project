using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

	public CharacterController controller;
	public Transform cam;
	private Animator animator;

	public float moveSpeed = 6f;
	public float turnSmoothTime = 0.1f;
	float turnSmoothVelocity;

	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;

	public float gravity = -9.81f;
	public float jumpHeight = 1f;
	Vector3 velocity;
	private bool groundedPlayer;

	void Start()
	{
		animator = GetComponentInChildren<Animator>();
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update()
	{

		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		bool runPressed = Input.GetKey("left shift");
		bool jumpPressed = Input.GetKey("space");
		Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
		velocity.y += gravity * Time.deltaTime;

		groundedPlayer = controller.isGrounded;
		if (groundedPlayer && velocity.y < 0)
		{
			velocity.y = 0f;
		}

		if (direction.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);

			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
		}

		// Changes the height position of the player..
		if (jumpPressed && groundedPlayer)
		{
			velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
			animator.SetBool("isJumping", true);

			StartCoroutine(AnimationWait());
		}

		//if (groundedPlayer)
		//{
			//animator.SetBool("isJumping", false);
		//}

		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);

		if (direction.magnitude == 0) //If the player is not moving...
		{

			animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
		}

		if (direction.magnitude > 0) //If the player is moving...
		{

			animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
			moveSpeed = 6f;
		}

		if(direction.magnitude > 0 && runPressed)
		{
			animator.SetFloat("Speed", 2, 0.1f, Time.deltaTime);
			moveSpeed = 12f;
		}

	}

	IEnumerator AnimationWait()
	{
		yield return new WaitForSeconds(1f);
		animator.SetBool("isJumping", false);
	}
}
