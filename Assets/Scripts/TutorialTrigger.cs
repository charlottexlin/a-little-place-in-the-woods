using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;

    private void OnTriggerEnter2D(Collider2D other) {
        if (tutorial && other.gameObject.tag == "Player")
            tutorial.SetActive(false);
    }
}
