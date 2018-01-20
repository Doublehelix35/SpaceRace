using UnityEngine;
using System.Collections;

public class S_Bullet : MonoBehaviour {

    public float moveSpeed = 1f; // Bullet speed
    public float duration = 2f; // Time until its destroyed
    public bool IsPlayerBullet = true;
    Vector3 BulletDir = Vector3.right;

    private void Start()
    {
        Destroy(gameObject, duration);
    }

    void Update()
    {
        // Move bullet
        transform.Translate(BulletDir * Time.deltaTime * moveSpeed * 0.5f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (IsPlayerBullet)
        {
            if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Boss")
            {
                Destroy(gameObject, 0);
            }
        }

        if (!IsPlayerBullet)
        {
            if (col.gameObject.tag == "Player")
            {
                Destroy(gameObject, 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (IsPlayerBullet)
        {
            if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Boss")
            {
                Destroy(gameObject, 0);
            }
        }

        if (!IsPlayerBullet)
        {
            if (col.gameObject.tag == "Player")
            {
                Destroy(gameObject, 0);
            }
        }
    }

    public void ChangeBulletDirection(Vector3 dir)
    {
        // Set direction
        BulletDir = dir;
    }
}
