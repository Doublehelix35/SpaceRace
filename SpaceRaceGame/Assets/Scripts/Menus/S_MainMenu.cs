using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_MainMenu : MonoBehaviour {

    public GameObject[] Spawners;
    public GameObject[] Bosses;
    GameObject CurrentBoss; // Boss to move and there should only be one at a time
    float SpawnTime;
    public Text Title;
    float SizeToScale = 0.2f;
    float Speed = 1f; // Speed to move bosses at

    int SpawnerNum = 0; // What spawner its on
    int BossNum = 0; // What boss its on

	
	void Start ()
    {
        SpawnBoss();
	}
	
	void Update ()
    {
        Vector3 ScaledVec = new Vector3(Mathf.PingPong(Time.time / 6, SizeToScale) + 1f, 
                                        Mathf.PingPong(Time.time / 6, SizeToScale) + 1f, 
                                        0f);
        Title.rectTransform.localScale = ScaledVec;

    }

    void SpawnBoss()
    {
        CurrentBoss = Instantiate(Bosses[BossNum], Spawners[SpawnerNum].transform.position, Spawners[SpawnerNum].transform.rotation);
        SpawnTime = Time.time;

        StartCoroutine("MoveThenKill");

        SpawnerNum++;
        BossNum++;

        if (SpawnerNum >= Spawners.Length) // Loop back around
        {
            SpawnerNum = 0;
        }

        if (BossNum >= Bosses.Length) // Loop back around
        {
            BossNum = 0;
        }
    }

    IEnumerator MoveThenKill()
    {
        float MovementTime = 4f;
        if (SpawnerNum == 0)
        {
            MovementTime = 7.5f;
        }
        else
        {
            MovementTime = 4f;
        }

        while (SpawnTime + MovementTime > Time.time)
        {
            
            CurrentBoss.transform.Translate(Vector2.right * Speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        Destroy(CurrentBoss);
        SpawnBoss();
    }
}
