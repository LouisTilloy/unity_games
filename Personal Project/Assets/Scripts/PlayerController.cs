using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    [SerializeField]
    private GameObject projectilePrefab;
    private List<GameObject> launchedProjectiles;

    private void Start()
    {
        launchedProjectiles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // Left and right movement
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);

        // Clean list of any null projectile
        launchedProjectiles.RemoveAll(prefab => prefab == null);

        // Shoot projectile
        if (Input.GetButtonDown("Fire") && launchedProjectiles.Count == 0)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, projectilePrefab.transform.position.y, transform.position.z);
            launchedProjectiles.Add(Instantiate(projectilePrefab, spawnPosition, transform.rotation));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("You're dead.");
        }
    }
}
