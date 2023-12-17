using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;

    private float animationTime;
    private float startPlayerPosition;
    private float endPlayerPosition = 0.0f;

    void Start()
    {
        startPlayerPosition = transform.position.x;

        animator = GetComponent<Animator>();
        animator.SetFloat("Speed_f", 0.3f);

        animationTime = GetComponent<PlayerController>().delayGameStart;
    }

    // Update is called once per frame
    void Update()
    {
        float x_pos = Mathf.Lerp(startPlayerPosition, endPlayerPosition, Time.time / animationTime);
        transform.position = new Vector3(x_pos, transform.position.y, transform.position.z);
    }
}
