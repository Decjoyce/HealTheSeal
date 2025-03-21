using UnityEngine;
using UnityEngine.Events;

public class SealController : MonoBehaviour
{
    [Header("Seal Stats")]
    public SealData sealData = new SealData(50f, 50f, 50f);

    public enum SealState { Moving, Idle, WallPause, SealCollisionPause }

    [Header("Movement Settings")]
    [Tooltip("Minimum speed for random movement.")]
    [SerializeField] private float minSpeed = 1f;
    [Tooltip("Maximum speed for random movement.")]
    [SerializeField] private float maxSpeed = 2f;
    [Tooltip("Duration for which the seal moves before idling (fixed).")]
    [SerializeField] private float moveDuration = 2f;

    [Header("Idle Settings")]
    [Tooltip("Minimum idle time (in seconds).")]
    [SerializeField] private float minIdleTime = 0.5f;
    [Tooltip("Maximum idle time (in seconds).")]
    [SerializeField] private float maxIdleTime = 1.5f;

    [Header("Collision Settings")]
    [Tooltip("Fixed pause duration when colliding with a wall.")]
    [SerializeField] private float wallPauseDuration = 0.5f;
    [Tooltip("Fixed pause duration when colliding with another seal.")]
    [SerializeField] private float sealCollisionPauseDuration = 0.5f;

    // Event to notify when this seal is clicked
    public UnityEvent<SealController> OnSealClicked;

    private SealState currentState;
    private Vector2 moveDirection;
    private float currentSpeed;
    private float stateTimer;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("SealController: Rigidbody2D component missing!");
        }
    }

    private void Start()
    {
        // Initialize the UnityEvent if it hasn�t been set via the Inspector.
        if (OnSealClicked == null)
            OnSealClicked = new UnityEvent<SealController>();

        // Bind the seal click event to the GameManager's handler (if available).
        if (GameManager.Instance != null)
        {
            OnSealClicked.AddListener(GameManager.Instance.SealClicked);
        }
        else
        {
            Debug.LogWarning("SealController: GameManager instance not found. Ensure GameManager is present in the scene.");
        }

        // Start with the seal moving.
        ChangeState(SealState.Moving);
    }

    private void Update()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0)
        {
            // Switch state when the timer expires.
            switch (currentState)
            {
                case SealState.Moving:
                    ChangeState(SealState.Idle);
                    break;
                case SealState.Idle:
                    ChangeState(SealState.Moving);
                    break;
                case SealState.WallPause:
                case SealState.SealCollisionPause:
                    ChangeState(SealState.Moving);
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        if (currentState == SealState.Moving)
        {
            rb.linearVelocity = moveDirection * currentSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Changes the seal's state and resets the appropriate timer.
    /// </summary>
    /// <param name="newState">The new state to transition to.</param>
    private void ChangeState(SealState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case SealState.Moving:
                currentSpeed = Random.Range(minSpeed, maxSpeed);
                moveDirection = Random.insideUnitCircle.normalized;
                stateTimer = moveDuration; // You could also randomize this if desired.
                break;
            case SealState.Idle:
                stateTimer = Random.Range(minIdleTime, maxIdleTime);
                break;
            case SealState.WallPause:
                stateTimer = wallPauseDuration;
                break;
            case SealState.SealCollisionPause:
                stateTimer = sealCollisionPauseDuration;
                break;
        }
    }

    /// <summary>
    /// Handles collision events with walls, obstacles, or other seals.
    /// </summary>
    /// <param name="collision">Collision data.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Obstacle"))
        {
            // Print to the console when a seal touches a wall or obstacle.
           // Debug.Log("Seal collided with: " + collision.gameObject.name);
            // When hitting a wall or obstacle, pause and change direction away from the wall.
            ChangeState(SealState.WallPause);
            // Use the collision contact normal to set the new movement direction (reversed).
            moveDirection = -collision.contacts[0].normal;
        }
        else if (collision.gameObject.CompareTag("Seal"))
        {
            // Print to the console when a seal touches another seal.
            //Debug.Log("Seal collided with: " + collision.gameObject.name);
            // When colliding with another seal, pause and then resume with a new random direction.
            ChangeState(SealState.SealCollisionPause);
            // (Optionally, you can also trigger the other seal's pause here.)
        }
    }

    /// <summary>
    /// Called when the seal is clicked.
    /// </summary>
    private void OnMouseDown()
    {
        OnSealClicked.Invoke(this);
    }
}
