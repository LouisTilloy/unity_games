using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePosWithResolution : MonoBehaviour
{
    [SerializeField] float posX1080p;  // Resolution of reference: 1920p x 1080p

    void Start()
    {
        MatchWallPosWithScreen();
        EventsHandler.OnScreenResolutionChange += MatchWallPosWithScreen;
    }

    void MatchWallPosWithScreen()
    {
        float posX = posX1080p * SharedUtils.AspectRatioScalingFactor();
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }

    private void OnDestroy()
    {
        EventsHandler.OnScreenResolutionChange -= MatchWallPosWithScreen;
    }

}
