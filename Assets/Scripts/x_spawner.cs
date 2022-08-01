using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class x_spawner : MonoBehaviour
{
    public GameObject hexagon;
    public GameObject circle;
    public GameObject diamond;
    public GameObject ellipse;
    public GameObject isosceles;
    public GameObject octagon;
    public GameObject parallelogram;
    public GameObject pentagon;
    public GameObject rectangle;
    public GameObject square;
    public GameObject trapeze;
    public GameObject triangle1;
    public GameObject triangle2;

    private float spawnTime = 0.8f;
    private int randomSpawner;

    void Start()
    {
        InvokeRepeating("addEnemy", spawnTime, spawnTime);
        randomSpawner = 0;
    }

    void addEnemy()
    {
        Renderer rend = GetComponent<Renderer>();

        float y1 = transform.position.y - rend.bounds.size.y / 2;
        float y2 = transform.position.y + rend.bounds.size.y / 2;

        Vector2 spawnPoint = new Vector2(transform.position.x, Random.Range(y1, y2));

        randomSpawner = Random.Range(0, 13);

        switch (randomSpawner) 
        {
            case 0:
                Instantiate(circle, spawnPoint, Quaternion.identity);
                break;
            case 1:
                Instantiate(diamond, spawnPoint, Quaternion.identity);
                break;
            case 2:
                Instantiate(ellipse, spawnPoint, Quaternion.identity);
                break;
            case 3:
                Instantiate(hexagon, spawnPoint, Quaternion.identity);
                break;
            case 4:
                Instantiate(isosceles, spawnPoint, Quaternion.identity);
                break;
            case 5:
                Instantiate(octagon, spawnPoint, Quaternion.identity);
                break;
            case 6:
                Instantiate(parallelogram, spawnPoint, Quaternion.identity);
                break;
            case 7:
                Instantiate(pentagon, spawnPoint, Quaternion.identity);
                break;
            case 8:
                Instantiate(rectangle, spawnPoint, Quaternion.identity);
                break;
            case 9:
                Instantiate(square, spawnPoint, Quaternion.identity);
                break;
            case 10:
                Instantiate(trapeze, spawnPoint, Quaternion.identity);
                break;
            case 11:
                Instantiate(triangle2, spawnPoint, Quaternion.identity);
                break;
            case 12:
                Instantiate(triangle1, spawnPoint, Quaternion.identity);
                break;
        }

    }

    void Update()
    {

    }
}