using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 cameraDistance;
    // Coordinates that the camera should not pass
    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;
    [SerializeField] private float topBound;
    [SerializeField] private float bottomBound;
    private bool disabled = false;

    private void LateUpdate() {
        if (!disabled) {
            // Calculate new camera position based on player position
            Vector3 newPosition = player.transform.position + cameraDistance;
            // Ensure camera does not surpass bounds
            if (newPosition.x < leftBound) {
                newPosition.x = leftBound;
            } else if (newPosition.x > rightBound) {
                newPosition.x = rightBound;
            }
            if (newPosition.y < bottomBound) {
                newPosition.y = bottomBound;
            } else if (newPosition.y > topBound) {
                newPosition.y = topBound;
            }
            // Move camera
            transform.position = newPosition;
        }
    }

    public Vector3 GetCameraDistance() {
        return cameraDistance;
    }

    // Disable the camera from following the player
    public void Disable() {
        disabled = true;
    }

    // Enable the camera to follow the player
    public void Enable() {
        disabled = false;
    }
}
