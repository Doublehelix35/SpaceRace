using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class S_PhaseManager : MonoBehaviour {

    public GameObject[] BossBars; // Boss bar ui images
    public GameObject Boss; // Boss game object
    public Sprite[] BossBarsSprites; // Make sure they are same number as boss bars & put them in back to front
    public GameObject Spawner;

    public void PhaseUpdater(int PhaseNum)
    {
        // Update boss bars based on the phase
        for (int i = 0; i < BossBars.Length; i++)
        {
            if(i == PhaseNum)
            {
                BossBars[i].GetComponent<Image>().sprite = BossBarsSprites[i];
            }
        }

        if(Spawner != null)
        {
            Spawner.GetComponent<S_PhaseSpawn>().SetPhaseNum(PhaseNum); // Tell Spawner what the phase is
            Boss.GetComponent<S_ArcShooting>().UpdateShootWaitTime(PhaseNum); // Tell boss what the phase is
        }        
    }
}
