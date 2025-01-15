using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float speed = 10;
	[SerializeField] private float dampValue = 0.2f;

	private Rigidbody2D rb;
	private Vector2 dir;

	//input
	private PlayerInput input; 
	private InputAction walk;


	private void OnEnable()
	{
		input = FindFirstObjectByType<PlayerInput>();
		walk = input.actions["Move"];
		walk.Enable();
	}
	
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		dir = walk.ReadValue<Vector2>();
		Vector2 refVel = Vector2.zero;
		
		rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, dir * speed, ref refVel, dampValue);
	}
}
