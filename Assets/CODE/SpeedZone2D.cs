using UnityEngine;

public class SpeedZone2D : MonoBehaviour
{
    public float zoneSpeed = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        WorldScroller scroller = FindObjectOfType<WorldScroller>();

        if (other.CompareTag("Player") && scroller != null)
        {
            scroller.SetSpeed(zoneSpeed);
        }
    }
}