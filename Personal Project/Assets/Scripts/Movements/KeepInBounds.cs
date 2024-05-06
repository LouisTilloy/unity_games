using UnityEngine;

public class KeepInBounds : MonoBehaviour
{
    // Reference Resolution: 1920p x 1080p
    float horizontalBound;
    [SerializeField] float referenceHorizontalBound;

    private void Start()
    {
        ScaleBoundWithScreen();
        EventsHandler.OnScreenResolutionChange += ScaleBoundWithScreen;
    }

    void ScaleBoundWithScreen()
    {
        horizontalBound = referenceHorizontalBound * SharedUtils.AspectRatioScalingFactor();
    }

    void Update()
    {
        if (transform.position.x > horizontalBound)
        {
            transform.position = new Vector3(horizontalBound, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -horizontalBound)
        {
            transform.position = new Vector3(-horizontalBound, transform.position.y, transform.position.z);
        }
    }

    private void OnDestroy()
    {
        EventsHandler.OnScreenResolutionChange -= ScaleBoundWithScreen;
    }
}
