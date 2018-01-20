using UnityEngine;
using System.Collections;

public class S_Explode : MonoBehaviour {


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Bullet")
        {
            // explode sprite change
            Destroy(gameObject, 0f);           
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Bullet")
        {
            // explode sprite change
            Destroy(gameObject, 0f);
        }
    }
}
