using UnityEngine;
using System.Collections;

public class S_AI_AimNShoot : MonoBehaviour {

    public int rotationOffset = 0;
    public float fireRate = 0;
    float timeToFire = 0;
    Transform firePoint;
    public GameObject Projectile;
    private GameObject PlayerRef;

    public float ActivationDistance = 1f;

    void Awake()
    {

        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("FirePoint NOT found!");
        }
        PlayerRef = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float Distance = Vector3.Distance(PlayerRef.gameObject.transform.position, this.transform.position); // calculate distance

        if (Distance < ActivationDistance)

        {
            // Calcualate difference then rotate towards the player
            Vector3 difference = PlayerRef.transform.position - gameObject.transform.position;
            difference.Normalize();
            float rotaZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            float rot = rotaZ + rotationOffset;

            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rot);

            // Shoot        
            if (Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {

        Vector3 firePointPosition = new Vector3(firePoint.position.x, firePoint.position.y, 0);

        Instantiate(Projectile, firePointPosition, firePoint.rotation);


    }
}
