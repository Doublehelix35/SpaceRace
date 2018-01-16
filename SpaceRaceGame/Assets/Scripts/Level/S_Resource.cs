using UnityEngine;
using System.Collections;

public class S_Resource : MonoBehaviour {

    public int ResourceType = 4; // 0 = rock, 1 = metal, 2 = Crystal, 3 = Uranium, 4 = Coin
    public float speed = 0.5f;

    float randomX = 0f;
    float randomY = 0f;

    //private int DirectionToHead = 0;  // 1 up, 2 up & right, 3 right, 4 down & right, 5 down, 6 down & left, 7 left, 8 up & left

    private void Start()
    {
        //DirectionToHead = Random.Range(1, 8);

        randomX = Random.Range(-1, 2);
        randomY = Random.Range(-1, 2);
        if(randomX == 0f && randomY == 0f)
        {
            randomX = 1f;
        }
    }

    void Update()
    {

        transform.Translate(new Vector3(randomX, randomY, 0f) * Time.deltaTime * speed, this.gameObject.transform);

        /*
        switch (DirectionToHead)
        {
            case 1:
                transform.Translate(Vector3.up * Time.deltaTime * speed, this.gameObject.transform); // move up
                break;
            case 2:
                transform.Translate(Vector3.up * Time.deltaTime * speed, this.gameObject.transform); // move up
                transform.Translate(Vector3.right * Time.deltaTime * speed, this.gameObject.transform); // move right
                break;
            case 3:
                transform.Translate(Vector3.right * Time.deltaTime * speed, this.gameObject.transform); // move right
                break;
            case 4:
                transform.Translate(Vector3.down * Time.deltaTime * speed, this.gameObject.transform); // move down
                transform.Translate(Vector3.right * Time.deltaTime * speed, this.gameObject.transform); // move right
                break;
            case 5:
                transform.Translate(Vector3.down * Time.deltaTime * speed, this.gameObject.transform); // move down
                break;
            case 6:
                transform.Translate(Vector3.down * Time.deltaTime * speed, this.gameObject.transform); // move down
                transform.Translate(Vector3.left * Time.deltaTime * speed, this.gameObject.transform); // move left
                break;
            case 7:
                transform.Translate(Vector3.left * Time.deltaTime * speed, this.gameObject.transform); //move left
                break;
            case 8:
                transform.Translate(Vector3.up * Time.deltaTime * speed, this.gameObject.transform); // move up
                transform.Translate(Vector3.left * Time.deltaTime * speed, this.gameObject.transform); // move left
                break;
            default:
                // error!!!
                Debug.Log("Cant move in that direction! Direction no. doesnt exist. I am " + this.gameObject.name);
                break;
        }
        */
    }
}
