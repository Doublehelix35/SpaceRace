using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GhostNightmare : MonoBehaviour {

    public float MinTimeTilDrop = 2f; // Min time until ghost attacks player
    public float MaxTimeTilDrop = 10f; // Max time until ghost attacks player
    float RandTimeTilDrop; // Time til ghost attacks player
    float LastDropTime; // Time.time of last attack
    public float RespawnTime = 3f; // Time until respawn
    bool AbleToDrop = true;

    public GameObject PlayerRef;
    Vector3 StartPos;
    Vector2 Target; // Target to head for
    bool HeadRight = true; // Tell Object to head right or left when pathfinding
    bool HeadDown = false; // Tell Object to move down or not

    public float DropSpeed = 1f; // Speed of ghost
    public float WaveAmplitude = 0f; // How far left and right the ghost will go on its journey down
    public float Health = 5f; // Current health
    float HealthMax; // Max health

    float SquareWaveTimer;
    float TriangleWaveTimer;

    public enum AttackPatterns
    {
        SineWave,
        SquareWave,
        TriangleWave,
        Pursuit
    }
    public AttackPatterns Pattern; // Decides how the ghost will behave

    void Start()
    {
        // Initialize values
        LastDropTime = Time.time;
        RandTimeTilDrop = Random.Range(MinTimeTilDrop, MaxTimeTilDrop);
        HealthMax = Health;
        SquareWaveTimer = WaveAmplitude / 2; // Only move half of amplitude initially
        // Store start position
        StartPos = transform.position;

        // Find target destination
        Target = new Vector2(10f, -10f);
    }

    void Update()
    {
        // Check if ghost can drop
        if (LastDropTime + RandTimeTilDrop < Time.time && AbleToDrop)
        {
            switch (Pattern)
            {
                case AttackPatterns.SineWave:
                    float SineWave = Mathf.Sin(Time.time) * WaveAmplitude;
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + SineWave, transform.position.y + Target.y), DropSpeed * Time.deltaTime);
                    break;
                case AttackPatterns.SquareWave:
                    SquareWaveTimer -= Time.deltaTime; // Count down timer

                    if (HeadDown) // Down
                    {
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + Target.y), DropSpeed * Time.deltaTime);

                        if (SquareWaveTimer <= 0f)
                        {
                            HeadDown = false;
                            HeadRight = !HeadRight; // Toggle value
                            SquareWaveTimer = WaveAmplitude;
                            Debug.Log(HeadRight);
                        }
                    }
                    else if (HeadRight) // Right
                    {
                        Target.x = 10f;
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + Target.x, transform.position.y), DropSpeed * Time.deltaTime);

                        if (SquareWaveTimer <= 0f)
                        {
                            HeadDown = true;
                            SquareWaveTimer = WaveAmplitude;
                        }
                    }
                    else if (!HeadRight) // Left
                    {
                        Target.x = -10f;
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + Target.x, transform.position.y), DropSpeed * Time.deltaTime);

                        if (SquareWaveTimer <= 0f)
                        {
                            HeadDown = true;
                            SquareWaveTimer = WaveAmplitude;
                        }
                    }
                    break;
                case AttackPatterns.TriangleWave:
                    SquareWaveTimer -= Time.deltaTime; // Count down timer

                    if (HeadRight)
                    {
                        Target.x = 10f;
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + Target.x, transform.position.y + Target.y), DropSpeed * Time.deltaTime);

                        if (SquareWaveTimer <= 0f)
                        {
                            HeadRight = false;
                            SquareWaveTimer = WaveAmplitude;
                        }
                    }
                    else
                    {
                        Target.x = -10f;
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + Target.x, transform.position.y + Target.y), DropSpeed * Time.deltaTime);

                        if (SquareWaveTimer <= 0f)
                        {
                            HeadRight = true;
                            SquareWaveTimer = WaveAmplitude;
                        }
                    }
                    break;
                case AttackPatterns.Pursuit:
                    // Move to player position
                    transform.position = Vector2.MoveTowards(transform.position, PlayerRef.transform.position, DropSpeed * Time.deltaTime);
                    break;
                default:
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            //Lose health
            Health--;

            if (Health <= 0) // Check health
            {
                StartCoroutine("GhostRespawn");
            }
        }

        if (col.gameObject.tag == "KillZone")
        {
            StartCoroutine("GhostRespawn");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine("GhostRespawn");

        }
    }

    IEnumerator GhostRespawn()
    {
        // Set alpha to 0
        Color TempColor = gameObject.GetComponent<SpriteRenderer>().color;
        TempColor.a = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = TempColor;

        gameObject.GetComponent<Collider2D>().enabled = false; // Turn off collision
        AbleToDrop = false;
        

        yield return new WaitForSeconds(RespawnTime);

        transform.position = StartPos; // Move back to the top
        StartCoroutine("FadeIn");

        yield return new WaitForSeconds(0.3f);

        gameObject.GetComponent<Collider2D>().enabled = true; // Turn on collision
        Health = HealthMax; // Reset health
        LastDropTime = Time.time; // Reset timer
        AbleToDrop = true;
    }

    IEnumerator FadeIn()
    {
        // Create temp color for fade in
        Color TempColor = gameObject.GetComponent<SpriteRenderer>().color;

        float FadeProgress = 0.0f; // Percentage of progress

        while (FadeProgress < 1)
        {
            // Smoothly go from 0 to 100% alpha
            TempColor.a = Mathf.Lerp(0f, 1f, FadeProgress);
            gameObject.GetComponent<SpriteRenderer>().color = TempColor;

            FadeProgress += Time.deltaTime * 0.8f; // Update fade progress
            yield return null;
        }
    }

}
