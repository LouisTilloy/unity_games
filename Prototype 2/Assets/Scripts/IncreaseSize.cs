using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSize : MonoBehaviour
{   
    public float speed = 1f;
    private Vector3 initialScale;
    private float timer;
    private float maxSize = 10.0f;

    // Start is called before the first frame update
    void Awake()
    {
        initialScale = transform.localScale;
        Initialize();
    }

    public void Initialize()
    {
        transform.localScale = initialScale;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * speed;
        float newScale = Mathf.Lerp(1, maxSize, timer);
        transform.localScale = initialScale * newScale;
    }

}
