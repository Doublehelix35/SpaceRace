using UnityEngine;
using System.Collections;

public class S_BackgroundScrolling : MonoBehaviour {

    [Range(0,1)] public float XSpeed = 1; // Speed to move on the x axis
    [Range(0,1)] public float YSpeed = 1; // Speed to move on the y axis

    Vector2 offset = Vector2.zero;
    Material mat; // Material to move

    void Awake()
    {
        // Get material on gameobject
        mat = GetComponent<MeshRenderer>().material;
    }

    public void MoveTexture(Vector2 pos)
    {
        // Calculate offset
        offset.x += pos.x * XSpeed;
        offset.y += pos.y * YSpeed;

        if(offset.x > 1f)
        {
            offset.x -= 1f;
        }
        else if(offset.x < -1f)
        {
            offset.x += 1f;
        }
        if(offset.y > 1f)
        {
            offset.y -= 1f;
        }
        else if (offset.y < -1f)
        {
            offset.y += 1f;
        }

        // Set mat offset to new offset
        mat.mainTextureOffset = offset;
    }

    // Failed attempt...

    /*public float TileSize = 5.12f;

    private GameObject PlayerRef;
    public GameObject Background;
    private GameObject[] BackgroundRefArray;
    private Camera CameraRef;

    float CameraWidth;
    float CameraHeight;

    public float offset = 1f;

    // Use this for initialization
    void Start()
    {
        CameraRef = Camera.main;
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        CameraHeight = 2f * CameraRef.orthographicSize;
        CameraWidth = CameraHeight * CameraRef.aspect;

        BackgroundRefArray = GameObject.FindGameObjectsWithTag("Background");

        Debug.Log(BackgroundRefArray.Length);

    }

    // Update is called once per frame
    void Update()
    {
        // Check if any edges of camera is touching any edges of a background
        // What edge is it touching? top, bottom, left or right?

        // Calculate where the edges of the camera are
        float CamLeft = PlayerRef.transform.position.x - (CameraWidth / 2);
        float CamRight = PlayerRef.transform.position.x + (CameraWidth / 2);
        float CamUp = PlayerRef.transform.position.x + (CameraHeight / 2);
        float CamDown = PlayerRef.transform.position.x - (CameraHeight / 2);

        // Loop through the array
        for (int i = 0; i < BackgroundRefArray.Length; i++)
        {
            // Calculate where the edges of the background are
            float DistLeft = BackgroundRefArray[i].transform.position.x - (TileSize / 2);
            float DistRight = BackgroundRefArray[i].transform.position.x + (TileSize / 2);
            float DistUp = BackgroundRefArray[i].transform.position.y + (TileSize / 2);
            float DistDown = BackgroundRefArray[i].transform.position.y - (TileSize / 2);
            
            // Compare where the backgrounds edges are to the camera's edges

            // Left
            if (CamLeft <= DistLeft + offset) 
            {
                // Check space is clear for background
                bool IsSpaceClear = false;
                
                // Loop and check
                for (int j = 0; j < BackgroundRefArray.Length; j++)
                {
                    float iPosX = BackgroundRefArray[i].transform.position.x - (TileSize / 2); // position of the left most edge of i on the x axis
                    float jPosX = BackgroundRefArray[j].transform.position.x - (TileSize / 2); // position of the left most edge of j on the x axis
                    if (iPosX >= jPosX && iPosX - TileSize <= jPosX)
                    {
                        IsSpaceClear = false;
                    }
                    else
                    {
                        IsSpaceClear = true;
                    }

                    if (IsSpaceClear)
                    {
                        // spawn background at intended location to the left
                        Instantiate(Background, new Vector3(BackgroundRefArray[i].transform.position.x - TileSize, BackgroundRefArray[i].transform.position.y, 0), BackgroundRefArray[i].transform.rotation);
                        Debug.Log("LEFT");
                        UpdateBackgroundArray(); // Add to background array
                    }
                }

                
                
            }

            // Right
            if (CamRight >= DistRight - offset)
            {
                // Check space is clear for background
                bool IsSpaceClear = false;

                // Loop and check
                for (int j = 0; j < BackgroundRefArray.Length; j++)
                {
                    float iPosX = BackgroundRefArray[i].transform.position.x + (TileSize / 2); // position of the left most edge of i on the x axis
                    float jPosX = BackgroundRefArray[j].transform.position.x + (TileSize / 2); // position of the left most edge of j on the x axis
                    if (iPosX <= jPosX && iPosX - TileSize >= jPosX)
                    {
                        IsSpaceClear = false;
                    }
                    else
                    {
                        IsSpaceClear = true;
                    }

                    if (IsSpaceClear)
                    {
                        // spawn background at intended location to the right
                        Instantiate(Background, new Vector3(BackgroundRefArray[i].transform.position.x + TileSize, BackgroundRefArray[i].transform.position.y, 0), BackgroundRefArray[i].transform.rotation);
                        Debug.Log("RIGHT");
                        UpdateBackgroundArray(); // Add to background array
                    }
                }
            }

            // Up
            if (CamUp >= DistUp - offset)
            {
                    // Check space is clear for background
                    bool IsSpaceClear = false;

                    // Loop and check
                    for (int j = 0; j < BackgroundRefArray.Length; j++)
                    {
                        float iPosX = BackgroundRefArray[i].transform.position.y + (TileSize / 2); // position of the left most edge of i on the x axis
                        float jPosX = BackgroundRefArray[j].transform.position.y + (TileSize / 2); // position of the left most edge of j on the x axis
                        if (iPosX <= jPosX && iPosX - TileSize >= jPosX)
                        {
                            IsSpaceClear = false;
                        }
                        else
                        {
                            IsSpaceClear = true;
                        }

                        if (IsSpaceClear)
                        {
                            // spawn background at intended location up
                            Instantiate(Background, new Vector3(BackgroundRefArray[i].transform.position.x, BackgroundRefArray[i].transform.position.y + TileSize, 0), BackgroundRefArray[i].transform.rotation);
                            Debug.Log("UP");
                            UpdateBackgroundArray(); // Add to background array
                    }
                    }
            }

            // Down
            if (CamDown <= DistDown + offset)
            {
                // Check space is clear for background
                bool IsSpaceClear = false;

                // Loop and check
                for (int j = 0; j < BackgroundRefArray.Length; j++)
                {
                    float iPosX = BackgroundRefArray[i].transform.position.y - (TileSize / 2); // position of the left most edge of i on the x axis
                    float jPosX = BackgroundRefArray[j].transform.position.y - (TileSize / 2); // position of the left most edge of j on the x axis
                    if (iPosX >= jPosX && iPosX - TileSize <= jPosX)
                    {
                        IsSpaceClear = false;
                    }
                    else
                    {
                        IsSpaceClear = true;
                    }

                    if (IsSpaceClear)
                    {
                        // spawn background at intended location down
                        Instantiate(Background, new Vector3(BackgroundRefArray[i].transform.position.x, BackgroundRefArray[i].transform.position.y - TileSize, 0), BackgroundRefArray[i].transform.rotation);
                        Debug.Log("DOWN");
                        UpdateBackgroundArray(); // Add to background array
                    }
                }
            }

        }

        // Spawn Background at that location

         
            // Instantiate(Background, PlayerRef.transform.position, PlayerRef.transform.rotation); // spawn background at intended location
        

         
    }
    
    void UpdateBackgroundArray()
    {
        BackgroundRefArray = GameObject.FindGameObjectsWithTag("Background");
    }
    */
}
