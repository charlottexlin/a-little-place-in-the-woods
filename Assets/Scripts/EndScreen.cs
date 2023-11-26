using System.Collections;
using UnityEngine;

public class EndScreen : MonoBehaviour
{

    private void OnEnable() {
        StartCoroutine(ShowCredits());
    }

    private IEnumerator ShowCredits() {
        // TODO
        yield return null;
    }
}