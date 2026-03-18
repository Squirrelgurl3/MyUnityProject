using UnityEngine;

public class WorldScroller : MonoBehaviour
{
    [Header("Default Speed")]
    public float defaultScrollSpeed = 3f;

    private float currentScrollSpeed;

    void Start()
    {
        currentScrollSpeed = defaultScrollSpeed;
    }

    void Update()
    {
        transform.position += Vector3.left * currentScrollSpeed * Time.deltaTime;
    }

    public void SetSpeed(float newSpeed)
    {
        currentScrollSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        currentScrollSpeed = defaultScrollSpeed;
    }
}