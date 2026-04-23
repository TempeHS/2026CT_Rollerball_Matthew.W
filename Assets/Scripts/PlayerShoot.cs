using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootCooldown = 0.2f;
    
    private float cooldownTimer = 0f;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && cooldownTimer <= 0f)
        {
            Shoot();
            cooldownTimer = shootCooldown;
        }
    }

    // Update is called once per frame
    void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
