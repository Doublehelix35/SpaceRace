using UnityEngine;
using System.Collections;

public class S_SaveGame : MonoBehaviour {

	
	// Update is called once per frame
	void Update ()
    {
        //S_GameManager.gameManager.DeleteSave();

       if (Input.GetKeyDown(KeyCode.F5))
        {
            ProcessLoad();
        }

	}

    public void ProcessSave()
    {
        // Update related variables, ready to be saved

        // Position
        S_GameManager.gameManager.PlayerPositionX = transform.position.x;
        S_GameManager.gameManager.PlayerPositionY = transform.position.y;
        S_GameManager.gameManager.PlayerPositionZ = transform.position.z;

        // Health
        S_GameManager.gameManager.PlayerHealthFloat = S_GameManager.gameManager.PlayerHealthInt;

        // Resources
        S_GameManager.gameManager.NumOfRockFloat = S_GameManager.gameManager.GetNumOfRock();
        S_GameManager.gameManager.NumOfMetalFloat = S_GameManager.gameManager.GetNumOfMetal();
        S_GameManager.gameManager.NumOfCrystalFloat = S_GameManager.gameManager.GetNumOfCrystal();
        S_GameManager.gameManager.NumOfUraniumFloat = S_GameManager.gameManager.GetNumOfUranium();
        S_GameManager.gameManager.NumOfCoinsFloat = S_GameManager.gameManager.GetNumOfCoins();

        // Call the save function
        S_GameManager.gameManager.Save();
    }

    public void ProcessLoad()
    {
        // Call load function
        S_GameManager.gameManager.LoadSave();

        // Convert loaded save into targeted variables

        // Position
        transform.position = new Vector3
        (
            S_GameManager.gameManager.PlayerPositionX,
            S_GameManager.gameManager.PlayerPositionY,
            S_GameManager.gameManager.PlayerPositionZ
        );

        // Health
        S_GameManager.gameManager.PlayerHealthInt = (int)S_GameManager.gameManager.PlayerHealthFloat;

        // Resources
        S_GameManager.gameManager.SetNumOfRock((int)S_GameManager.gameManager.NumOfRockFloat);
        S_GameManager.gameManager.SetNumOfMetal((int)S_GameManager.gameManager.NumOfMetalFloat);
        S_GameManager.gameManager.SetNumOfCrystal((int)S_GameManager.gameManager.NumOfCrystalFloat);
        S_GameManager.gameManager.SetNumOfUranium((int)S_GameManager.gameManager.NumOfUraniumFloat);
        S_GameManager.gameManager.SetNumOfCoins(S_GameManager.gameManager.NumOfCoinsFloat);
    }
}
