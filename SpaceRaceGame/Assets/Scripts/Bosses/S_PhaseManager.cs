using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class S_PhaseManager : MonoBehaviour {

    public GameObject[] BossBars; // boss bar ui images
    public GameObject Boss; // boss game object
    public Sprite[] BossBarsSprites; // make sure they are same number as boss bars & put them in back to front

    public GameObject Spawner;

    public void PhaseUpdater(int PhaseNum)
    {

        for (int i = 0; i < BossBars.Length; i++)
        {
            if(i == PhaseNum)
            {
                BossBars[i].GetComponent<Image>().sprite = BossBarsSprites[i];
            }
        }

        if(Spawner != null)
        {
            Spawner.GetComponent<S_PhaseSpawn>().SetPhaseNum(PhaseNum);
            Boss.GetComponent<S_ArcShooting>().UpdateShootWaitTime(PhaseNum);
        }        
    }
}
