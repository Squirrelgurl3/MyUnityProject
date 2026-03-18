using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Needed for IEnumerator

public class ResetOnTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player"; // Tag of your player
    [SerializeField] private float resetDelay = 0.5f; // Delay in seconds before reset

    // Called when something enters the trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            // Start the delayed reset
            StartCoroutine(ResetSceneCoroutine());
        }
    }

    // Coroutine that waits before reloading the scene
    private IEnumerator ResetSceneCoroutine()
    {
        yield return new WaitForSeconds(resetDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
