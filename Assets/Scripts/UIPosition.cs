using UnityEngine;

public class UIPosition : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    
    private void LateUpdate() {
        Vector3 newPosition = player.transform.position + offset;
        transform.position = newPosition;
    }
}