using System.Collections;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup credits;
    [SerializeField] private float fadeTime;

    private void OnEnable() {
        credits.gameObject.SetActive(true);
        credits.alpha = 0f;
        StartCoroutine(ShowCredits());
    }

    // Fade in the credits screen
    private IEnumerator ShowCredits() {
        // Wait for a while
        yield return new WaitForSeconds(4f);
        while (credits.alpha < 1f) {
            credits.alpha += Time.deltaTime / fadeTime;
            yield return null;
        }
    }
}