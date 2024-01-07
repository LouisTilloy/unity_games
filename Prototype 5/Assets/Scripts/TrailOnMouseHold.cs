using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailOnMouseHold : MonoBehaviour
{
    private TrailRenderer trailRenderer;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        trailRenderer.enabled = Input.GetMouseButton(0) && gameManager.isGameActive;

        trailRenderer.transform.position = mainCamera.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane)
        );
    }
}
