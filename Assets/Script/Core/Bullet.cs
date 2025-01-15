using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float defaultSpeed = 10;
	private Rigidbody2D rb;

	private void Awake()
		=> rb = GetComponent<Rigidbody2D>();

	public void MoveBullet(Vector2 dir)
	{
		rb.linearVelocity =  defaultSpeed * dir; 
	}
}

