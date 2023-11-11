using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 cameraDistance;

    private void LateUpdate() {
        transform.position = player.transform.position + cameraDistance;
    }
}
