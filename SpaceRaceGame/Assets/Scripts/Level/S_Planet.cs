using UnityEngine;
using System.Collections;

public class S_Planet : MonoBehaviour {

    private bool ShopOpened = false;

    // Resource prices
    private float RockValue = 1;
    private float MetalValue = 5;
    private float CrystalValue = 10;
    private float UraniumValue = 30;

    public int PlanetLevel = 1; // higher the level, the better the shop is

    private GameObject ShopTab;
    private GameObject PlanetButtons;
    private GameObject PlayerRef;

    void Start ()
    {
        // Find shop tab
        ShopTab = GameObject.Find("ShopTab");
        if (ShopTab == null)
        {
            //refind shop tab
            Debug.Log("Cant find shop tab! I am " + this.gameObject.name);
        }

        PlanetButtons = GameObject.Find("PlanetButtons");
        // Refs
        PlayerRef = GameObject.FindGameObjectWithTag("Player");

        PlanetLevelCalcs();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Planet Triggered!");

        if (col.gameObject.tag == "Player" && !ShopOpened)
        {
            // open shop ui
            ShopTab.SetActive(true);
            PlanetButtons.SetActive(true);

            // set ShopOpened to true
            ShopOpened = true;

            // Stop Player movement
            PlayerRef.GetComponent<S_Player>().ActivatePauseMovement();            
        }
        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && ShopOpened)
        {
            // After user closes wait until til they leave to set open to false
            ShopTab.SetActive(false);
            PlanetButtons.SetActive(false);
            ShopOpened = false;

            // Reactivate Player movement
            PlayerRef.GetComponent<S_Player>().DeactivatePauseMovement();
        }
    }

    public float GetRockValue()
    {
        return RockValue;
    }

    public float GetMetalValue()
    {
        return MetalValue;
    }

    public float GetCrystalValue()
    {
        return CrystalValue;
    }

    public float GetUraniumValue()
    {
        return UraniumValue;
    }

    void PlanetLevelCalcs()
    {
        // Size
        if (PlanetLevel <= 0) // Dont go below or equal to 0 scale
        {
            gameObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0);
        }
        else // Set scale equal to planet level
        {
            gameObject.transform.localScale = new Vector3(transform.localScale.x * PlanetLevel, transform.localScale.y * PlanetLevel, 0);
        }

        // Shop Values
        MetalValue = MetalValue - PlanetLevel;
        CrystalValue = CrystalValue - PlanetLevel;
        UraniumValue = UraniumValue - PlanetLevel;
    }
}
