using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Tooltip("How much this layer moves relative to the world speed")]
    [Range(0f, 1f)]
    public float parallaxMultiplier = 0.5f;

    private WorldScroller scroller;

    void Start()
    {
        scroller = FindObjectOfType<WorldScroller>();
    }

    void Update()
    {
        if (scroller == null) return;

        float speed = scroller.GetComponent<WorldScroller>().defaultScrollSpeed;

        transform.position += Vector3.left * speed * parallaxMultiplier * Time.deltaTime;
    }
}