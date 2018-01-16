using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class S_GameManager : MonoBehaviour {
     
    // Saving
    public static S_GameManager gameManager;

    public float PlayerPositionX; // Stores player X axis position
    public float PlayerPositionY; // Stores player Y axis position
    public float PlayerPositionZ; // Stores player Z axis position
    public float PlayerHealthFloat; // Stores player health as float
    public float NumOfRockFloat; // Stores num of rock as float
    public float NumOfMetalFloat; // Stores num of metal as float
    public float NumOfCrystalFloat; // Stores num of crystal as float
    public float NumOfUraniumFloat; // Stores num of uranium as float
    public float NumOfCoinsFloat; // Stores num of coins as float


    // Text
    public Text PlayerHealthText;
    public int PlayerHealthInt = 10;
    private int PlayerHealthMax = 6;

    public Text RockText;
    public Text MetalText;
    public Text CrystalText;
    public Text UraniumText;
    public Text CoinsText;

    // Resource nums
    private int NumOfRock = 0;
    private int NumOfMetal = 0;
    private int NumOfCrystal = 0;
    private int NumOfUranium = 0;
    private float NumOfCoins = 50;

    
    private Transform PlayerRespawn;

    // Object refs
    private GameObject PlanetRef;
    private GameObject PlayerRef;
    private GameObject SettingsTab;

    private bool GamePaused = false;

    public bool IsBossFight = false;

    private void Awake()
    {
        if (gameManager == null)
        {
            DontDestroyOnLoad(gameObject); // Dont destroy this
            gameManager = this;
        }
        else if(gameManager!= this)
        {
            Destroy(gameObject); // Destroy this as another exists
        }
    }

    // Use this for initialization
    void Start () {

        // Health
        PlayerHealthMax = PlayerHealthInt;

        // Shop
	    GameObject ShopTab = GameObject.Find("ShopTab");
        if(ShopTab != null)
        {
            ShopTab.SetActive(false);
        }
        

        GameObject PlanetButtons = GameObject.Find("PlanetButtons");
        if(PlanetButtons != null)
        {
            PlanetButtons.SetActive(false);
        }
        

        // Settings
        SettingsTab = GameObject.Find("SettingsTab");
        if(SettingsTab != null)
        {
            SettingsTab.SetActive(false);
        }
        

        // Gameobject refs
        PlanetRef = GameObject.FindGameObjectWithTag("Planet");
        PlayerRef = GameObject.FindGameObjectWithTag("Player");

        // Refresh text
        RockTextRefresh();
        MetalTextRefresh();
        CrystalTextRefresh();
        UraniumTextRefresh();
        CoinsTextRefresh();
        HealthTextRefresh();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Escape))
        {
            SettingsTab.SetActive(true);
            PauseTime();
        }

	
        if (GamePaused)
        {
            // Pause time
            Time.timeScale = 0;
        }
        else
        {
            // Unpause time
            Time.timeScale = 1;
        }
	}


    // **************** Health *****************************

    public void HealthEquals (int HealthValue)
    {
        PlayerHealthInt = HealthValue;
        HealthTextRefresh();
    }

    public void HealthPlus ()
    {
        PlayerHealthInt++;
        HealthTextRefresh();
    }

    public void HealthMinus ()
    {
        PlayerHealthInt--;
        HealthTextRefresh();
        if (PlayerHealthInt <= 0)
        {
            PlayerDead();
        }
    }

    void HealthTextRefresh()
    {
        PlayerHealthText.text = PlayerHealthInt.ToString();
    }

    void PlayerDead()
    {
        // Open GameOver UI

        if (!IsBossFight) // Non boss fight game overs
        {
            RespawnPlayer(); // sets player position to checkpoint position
            PlayerHealthInt = PlayerHealthMax; // resets player health
            HealthTextRefresh();

            // If the player died in the shop, reset movement
            if (PlayerRef.GetComponent<S_Player>().IsInShop == true)
            {
                PlayerRef.GetComponent<S_Player>().DeactivatePauseMovement();
                PlayerRef.GetComponent<S_Player>().IsInShop = false;
            }
        }
        else // boss fight game overs
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload the current scene
        }
        
        
    }


    // *************** Resources **********************

        // Add one to a resource
    public void UpdateResourceAdd(int ResourceType)
    {
        // Check who is calling function, shop or player? Fuction should only minus coins if the resources are bought from the shop
        bool IsShop;
        if (PlayerRef.GetComponent<S_Player>().GetUpdatingUIBool())
        {
            IsShop = false;
        }
        else
        {
            IsShop = true;
        }
        
        switch (ResourceType)
        {
            case 0:
                NumOfRock++; // Increase number of resource
                RockTextRefresh();
                if (IsShop)
                {
                    NumOfCoins -= PlanetRef.GetComponent<S_Planet>().GetRockValue() + 1; // Get value of Rock and then minus it from coin total
                    CoinsTextRefresh();
                }
                break;
            case 1:
                NumOfMetal++; // Increase number of resource
                MetalTextRefresh();
                if (IsShop)
                {
                    NumOfCoins -= PlanetRef.GetComponent<S_Planet>().GetMetalValue() + 1; // Get value of Metal and then minus it from coin total
                    CoinsTextRefresh();
                }
                break;
            case 2:
                NumOfCrystal++; // Increase number of resource
                CrystalTextRefresh();
                if (IsShop)
                {
                    NumOfCoins -= PlanetRef.GetComponent<S_Planet>().GetCrystalValue() + 1; // Get value of Crystal and then minus it from coin total
                    CoinsTextRefresh();
                }
                break;
            case 3:
                NumOfUranium++; // Increase number of resource
                UraniumTextRefresh();
                if (IsShop)
                {
                    NumOfCoins -= PlanetRef.GetComponent<S_Planet>().GetUraniumValue() + 1; // Get value of Uranium and then minus it from coin total
                    CoinsTextRefresh();
                }
                break;
            case 4:
                NumOfCoins++; // Increase number of resource
                CoinsTextRefresh();
                break;
            default:
                //ERROR!!!
                Debug.Log("Cant add Resource! Resource type no. doesnt exist. I am " + this.gameObject.name);
                break;
        }
    }

        // Minus one from a resource
    public void UpdateResourceMinus(int ResourceType)
    {
        // Check who is calling function, shop or player? Fuction should only add coins if the resources are sold to the shop
        bool IsShop;

        if (PlayerRef.GetComponent<S_Player>().GetUpdatingUIBool())
        {
            IsShop = false;
        }
        else
        {
            IsShop = true;
        }

        switch (ResourceType)
        {
            case 0:

                // Stop number going below zero
                if(NumOfRock <= 0)
                {
                    break;
                }
                NumOfRock--; // Decrease number of resource
                RockTextRefresh();
                if (IsShop)
                {
                    NumOfCoins += PlanetRef.GetComponent<S_Planet>().GetRockValue(); // Get value of Rock and then add it to coin total
                    CoinsTextRefresh();
                }
                
                break;
            case 1:

                // Stop number going below zero
                if (NumOfMetal <= 0)
                {
                    break;
                }
                NumOfMetal--; // Decrease number of resource
                MetalTextRefresh();
                if (IsShop)
                {
                    NumOfCoins += PlanetRef.GetComponent<S_Planet>().GetMetalValue(); // Get value of Metal and then add it to coin total
                    CoinsTextRefresh();
                }
                break;
            case 2:

                // Stop number going below zero
                if (NumOfCrystal <= 0)
                {
                    break;
                }
                NumOfCrystal--; // Decrease number of resource
                CrystalTextRefresh();
                if (IsShop)
                {
                    NumOfCoins += PlanetRef.GetComponent<S_Planet>().GetCrystalValue(); // Get value of Crystal and then add it to coin total
                    CoinsTextRefresh();
                }
                break;
            case 3:

                // Stop number going below zero
                if (NumOfUranium <= 0)
                {
                    break;
                }
                NumOfUranium--; // Decrease number of resource
                UraniumTextRefresh();
                if (IsShop)
                {
                    NumOfCoins += PlanetRef.GetComponent<S_Planet>().GetUraniumValue();  // Get value of Uranium and then add it to coin total
                    CoinsTextRefresh();
                }
                break;
            case 4:
                NumOfCoins--; // Decrease number of resource
                CoinsTextRefresh();
                break;
            default:
                //ERROR!!!
                Debug.Log("Cant minus Resource! Resource type no. doesnt exist. I am " + this.gameObject.name);
                break;
        }
    }


  
        // Resource Getters

        public int GetNumOfRock()
    {
            return NumOfRock;
    }

    public int GetNumOfMetal()
    {
        return NumOfMetal;
    }

    public int GetNumOfCrystal()
    {
        return NumOfCrystal;
    }

    public int GetNumOfUranium()
    {
        return NumOfUranium;
    }

    public float GetNumOfCoins()
    {
        return NumOfCoins;
    }

        // Resource Setters

    public void SetNumOfRock(int RockNum)
    {
        NumOfRock = RockNum;
        RockTextRefresh();
    }

    public void SetNumOfMetal(int MetalNum)
    {
        NumOfMetal = MetalNum;
        MetalTextRefresh();
    }

    public void SetNumOfCrystal(int CrystalNum)
    {
        NumOfCrystal = CrystalNum;
        CrystalTextRefresh();
    }

    public void SetNumOfUranium(int UraniumNum)
    {
        NumOfUranium = UraniumNum;
        UraniumTextRefresh();
    }

    public void SetNumOfCoins(float CoinsNum)
    {
        NumOfCoins = CoinsNum;
        CoinsTextRefresh();
    } 


    // ************* Text Refresh ******************

    void RockTextRefresh()
    {
        RockText.text = NumOfRock.ToString();
    }

    void MetalTextRefresh()
    {
        MetalText.text = NumOfMetal.ToString();
    }

    void CrystalTextRefresh()
    {
        CrystalText.text = NumOfCrystal.ToString();
    }

    void UraniumTextRefresh()
    {
        UraniumText.text = NumOfUranium.ToString();
    }

    void CoinsTextRefresh()
    {
        CoinsText.text = NumOfCoins.ToString();
    }


    // *************** Respawn *********************

    public void RespawnRefresh(Transform NewSpawn) // takes a new transform and sets player respawn to it
    {
        PlayerRespawn = NewSpawn;
    }

    private void RespawnPlayer() // sets player position to checkpoint position
    {
        PlayerRef.transform.position = PlayerRespawn.position;
    }


    // ************** Pause Time ******************

    public void PauseTime()
    {
        GamePaused = true;
    }

    public void UnpauseTime()
    {
        GamePaused = false;
    }

    // ************* Save Game ********************

    public void Save()
    {
        // Create a binary formatter and a new file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        // Create an object to save information to
        PlayerData data = new PlayerData();
        // Position
        data.PlayerPosX = PlayerPositionX;
        data.PlayerPosY = PlayerPositionY;
        data.PlayerPosZ = PlayerPositionZ;

        // Health
        data.PlayerHealth = PlayerHealthFloat;

        // Resources
        data.RockNum = NumOfRockFloat;
        data.MetalNum = NumOfMetalFloat;
        data.CrystalNum = NumOfCrystalFloat;
        data.UraniumNum = NumOfUraniumFloat;
        data.CoinsNum = NumOfCoinsFloat;

        // Write the object to the file and close it afterwards
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadSave()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            // Create a binary formatter and open the save file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            // Create an object to store information from the file in and then close the file
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            // Using save data, update varibles
            PlayerPositionX = data.PlayerPosX;
            PlayerPositionY = data.PlayerPosY;
            PlayerPositionZ = data.PlayerPosZ;

            // Health
            PlayerHealthFloat = data.PlayerHealth;
            
            // Resources
            NumOfRockFloat = data.RockNum;
            NumOfMetalFloat = data.MetalNum;
            NumOfCrystalFloat = data.CrystalNum;
            NumOfUraniumFloat = data.UraniumNum;
            NumOfCoinsFloat = data.CoinsNum;
        }
    }

    public void DeleteSave()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
        }
    }
}

[Serializable]
class PlayerData
{
    public float PlayerPosX;
    public float PlayerPosY;
    public float PlayerPosZ;
    public float PlayerHealth;
    public float RockNum;
    public float MetalNum;
    public float CrystalNum;
    public float UraniumNum;
    public float CoinsNum;

}
