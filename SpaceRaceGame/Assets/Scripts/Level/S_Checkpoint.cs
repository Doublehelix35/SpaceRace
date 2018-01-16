using UnityEngine;
using System.Collections;

public class S_Checkpoint : MonoBehaviour {

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if( col.gameObject.tag == "Player")
        {
            // Update spawn point
            S_GameManager.gameManager.RespawnRefresh(this.gameObject.transform);
            Debug.Log("CheckPoint!");
        }
       
    }
}
