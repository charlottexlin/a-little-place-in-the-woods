using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 cameraDistance;
    // Coordinates that the camera should not pass
    [SerializeField] private int leftBound;
    [SerializeField] private int rightBound;
    [SerializeField] private int topBound;
    [SerializeField] private int bottomBound;

    private void LateUpdate() {
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
