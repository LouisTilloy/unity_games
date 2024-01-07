using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailOnMouseClickHold : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector3> linePoints;
    private List<float> pointsDeathTime;
    // Start is called before the first frame update    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        linePoints = new List<Vector3>();
        //linePoints.Add(new Vector3(-2.0f, 5.0f, 0.0f));
        //linePoints.Add(new Vector3(-1.0f, 5.0f, 0.0f));
        //linePoints.Add(new Vector3(0.0f, 5.0f, 0.0f));

        pointsDeathTime = new List<float>();
        //pointsDeathTime.Add(Time.time + 2.0f);
        //pointsDeathTime.Add(Time.time + 4.0f);
        //pointsDeathTime.Add(Time.time + 6.0f);
    }

    // Update is called once per frame
    void Update()
    {
        linePoints.Add(new Vector3(Time.timeSinceLevelLoad, 5.0f, 0.0f));
        pointsDeathTime.Add(Time.time + 6.0f);

        while (Time.time > pointsDeathTime[0])
        {
            Debug.Log(Time.timeSinceLevelLoad);
            pointsDeathTime.RemoveAt(0);
            linePoints.RemoveAt(0);
        }

        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
    }
}
