using UnityEngine;
using System.Collections;

public class SealScript : MonoBehaviour
{
    public float speed = 2f; // Movement speed
    private Vector2 direction;
    private bool isMoving = true;
    private Camera mainCamera;
    private float spriteWidth, spriteHeight;

    void Start()
    {
        mainCamera = Camera.main;
        direction = Random.insideUnitCircle.normalized; // Get a random direction

        // Calculate sprite size in world units
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteWidth = spriteRenderer.bounds.extents.x;
            spriteHeight = spriteRenderer.bounds.extents.y;
        }

        StartCoroutine(ChangeDirectionRoutine());
    }

    void Update()
    {
        if (isMoving)
        {
            MoveRandomly();
        }
    }

    void MoveRandomly()
    {
        Vector3 newPosition = transform.position + (Vector3)direction * speed * Time.deltaTime;

        // Get screen bounds in world units
        float screenHeight = mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;

        // Check if hitting a wall and change direction
        if (newPosition.x <= -screenWidth + spriteWidth || newPosition.x >= screenWidth - spriteWidth)
        {
            direction.x = -direction.x; // Reverse X direction
        }
        if (newPosition.y <= -screenHeight + spriteHeight || newPosition.y >= screenHeight - spriteHeight)
        {
            direction.y = -direction.y; // Reverse Y direction
        }

        // Constrain the sprite within screen bounds
        newPosition.x = Mathf.Clamp(newPosition.x, -screenWidth + spriteWidth, screenWidth - spriteWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, -screenHeight + spriteHeight, screenHeight - spriteHeight);

        transform.position = newPosition;
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            float randomMoveDuration = Random.Range(1f, 5f); // Random interval between stops
            yield return new WaitForSeconds(randomMoveDuration);
            isMoving = false;
            float randomStopDuration = Random.Range(1f, 5f); // Random stop duration between 1 and 5 seconds
            yield return new WaitForSeconds(randomStopDuration);
            isMoving = true;
            direction = Random.insideUnitCircle.normalized; // Change to a new random direction
        }
    }

    void OnMouseDown()
    {
        isMoving = false; // Stop moving when clicked
    }
}
