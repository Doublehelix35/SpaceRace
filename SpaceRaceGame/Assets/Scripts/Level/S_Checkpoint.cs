using UnityEngine;
using System.Collections;

public class S_Checkpoint : MonoBehaviour {

    
    void OnTriggerEnter2D(Collider2D col)
    {
        if( col.gameObject.tag == "Player")
        {
            // Update spawn point
            S_GameManager.gameManager.RespawnRefresh(gameObject.transform);
            Debug.Log("CheckPoint!");
        }
    }
}
