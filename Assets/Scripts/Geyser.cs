using UnityEngine;

public class Geyser : MonoBehaviour {
	private Animator animator;

	private BoxCollider2D physicsCollider;

	[SerializeField]
	private float maxHeight;

	[SerializeField]
	private float risingSpeed = .06f;

	private bool activated;

	private AudioSource audioSource;

	void Awake() {
		physicsCollider = GetComponentInChildren<BoxCollider2D>();
		animator = GetComponentInChildren<Animator>();
		audioSource = GetComponentInChildren<AudioSource>();
	}

	void Start() {
		Deactivate();
	}

	void Update() {
		if ( activated && physicsCollider.size.y < maxHeight )
			Scale(risingSpeed);
		if( !activated && physicsCollider.size.y > Mathf.Epsilon )
			Scale(-risingSpeed);

	}

	private void Scale(float delta) {
		physicsCollider.offset = new Vector2(
			physicsCollider.offset.x,
			delta / 2
		);
		physicsCollider.size = new Vector2(
			physicsCollider.size.x,
			physicsCollider.size.y + delta
		);
		// Physics2D.SyncTransforms();
	}

	void OnTriggerStay2D(Collider2D collision) {
		// if the idle player stays collided with the geyser and the geyser is enabled...
		if( activated && collision.gameObject.CompareTag("IdlePerson") ) {
			idleScript person = collision.GetComponent<idleScript>();

			// calculates the depth of the idle player
			float playerHeight = collision.bounds.min.y;
			float geyserHeight = physicsCollider.bounds.max.y;
			float depth = geyserHeight - playerHeight;

			if( depth < .01 ) {
				// decreases speed of idle player
				person.SlowDown();
			}
			else {
				// adds upwards force to idle player based on depth
				person.Float(depth);
			}
		}
	}

	public void Activate() {
		activated = true;
		audioSource.Play();
		animator.Play("fire_hydrant_start");
	}

	public void Deactivate() {
		activated = false;
		audioSource.Pause();
		animator.Play("fire_hydrant_off");
	}
}