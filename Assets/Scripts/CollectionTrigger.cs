using UnityEngine;

// This script is attached to a game object with a trigger collider
// Its purpose is to require the player to stand on the trigger in order to pick up the item
public class CollectionTrigger : MonoBehaviour
{
    // The postcard object's script component
    [SerializeField] private Clickable clickable;

    private void OnTriggerEnter2D(Collider2D other) {
        if (clickable && other.gameObject.tag == "Player")
            clickable.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (clickable && other.gameObject.tag == "Player")
            clickable.enabled = false;
    }
}
