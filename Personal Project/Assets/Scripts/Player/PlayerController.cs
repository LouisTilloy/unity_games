using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ObjectPooling objectPooling;
    [SerializeField] PauseMenu pauseMenu;
    Animator animator;
    Quaternion initialRotation;
    public float speed;
    List<GameObject> launchedProjectiles;
    PowerupManager powerupManager;

    private void Start()
    {
        launchedProjectiles = new List<GameObject>();
        powerupManager = GetComponent<PowerupManager>();
        animator = GetComponent<Animator>();
        initialRotation = transform.rotation;
        EventsHandler.OnGameOver += LaunchDeathAnimation;
    }

    private void OnDestroy()
    {
        EventsHandler.OnGameOver -= LaunchDeathAnimation;
    }

    void LaunchDeathAnimation()
    {
        animator.SetBool("Death_b", true);
        // Allows playing the death animation even with time being frozen at the end of the game.
        animator.updateMode = AnimatorUpdateMode.UnscaledTime; 
    }

    int MaxProjectileCount()
    {
        return powerupManager.powerupLevels[0] + 1;
    }

    public void DeleteAllActiveProjectiles()
    {
        foreach (GameObject projectile in launchedProjectiles)
        {
            projectile.SetActive(false);
        }
    }

    void Update()
    {
        // Left and right movement
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput, Space.World);
        
        // Animations
        animator.SetFloat("Speed_f", Mathf.Abs(horizontalInput));
        transform.rotation = Quaternion.Euler(new Vector3(0, 90 * horizontalInput, 0) + initialRotation.eulerAngles);

        // Clean list of any deactivated projectile
        launchedProjectiles.RemoveAll(projectile => !projectile.activeInHierarchy);

        // Shoot projectile
        GameObject lastProjectile;
        if (!pauseMenu.isGamePaused && Input.GetButtonDown("Fire") && launchedProjectiles.Count < MaxProjectileCount())
        {
            lastProjectile = objectPooling.GetPooledObject();
            lastProjectile.transform.position = transform.position;
            // lastProjectile.transform.rotation = transform.rotation;
            lastProjectile.SetActive(true);
            launchedProjectiles.Add(lastProjectile);

            EventsHandler.InvokeOnProjectileShot();
        }
    }
}
