using UnityEngine;

public class WaterBlocker : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
            GameManager.instance.dialogueUI.ShowText(ItemsToCollect.texts["water"]);
    }
}
