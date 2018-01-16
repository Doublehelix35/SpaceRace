using UnityEngine;
using System.Collections;

public class S_Bullet : MonoBehaviour {

    public float moveSpeed = 1f;
    public float duration = 2f;
    public bool IsPlayerBullet = true;
    Vector3 BulletDir = Vector3.right;

    private void Start()
    {
        Destroy(this.gameObject, duration);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(BulletDir * Time.deltaTime * moveSpeed * 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D col)
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

    private void OnTriggerEnter2D(Collider2D col)
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
        BulletDir = dir;
    }
}
