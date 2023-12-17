using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : MonoBehaviour
{
    public string chasedEnemyName;
    private GameObject chasedEnemy;
    private Rigidbody rocketRigidBody;
    private float speed = 10.0f;
    public float rocketStrength = 2.5f;
    private float knockbackScaleRateWithMass = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        chasedEnemy = GameObject.Find(chasedEnemyName);

        if (chasedEnemy is null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (chasedEnemy.transform.position - transform.position).normalized;
        rocketRigidBody.velocity = direction * speed;
        transform.up = direction;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == chasedEnemyName)
        {
            Rigidbody enemyRigidBody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 outwardsDirection = (enemyRigidBody.transform.position - transform.position).normalized;
            // Rocket power scales with mass, this is why we multiply by sqrt of mass.
            enemyRigidBody.AddForce(outwardsDirection * rocketStrength * Mathf.Pow(enemyRigidBody.mass, knockbackScaleRateWithMass), ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}
