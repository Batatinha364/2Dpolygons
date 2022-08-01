using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle1 : MonoBehaviour
{
    //public float maxTorque;
    //public Rigidbody2D rb;

    //public float maxVelocity;
    private float velocity;
    private float shotDelay = 0f;
    bool isEnemyDestroyed;

    public float range;
    public float maxDistance;
    Vector2 wayPoint;

    private float screenTop;
    private float screenBottom;
    private float screenRight;
    private float screenLeft;

    private int points = 2;

    private GameObject score;
    public GameObject projectile;

    public AudioClip destroy_sound;

    Animator anim_triangle1;

    void Start()
    {
        screenTop = 5.88f;
        screenBottom = -5.88f;
        screenRight = 7.2f;
        screenLeft = -7.2f;
        isEnemyDestroyed = false;
        SetNewDestination();

        velocity = Random.Range(2, 5);
        shotDelay = Random.Range(1, 5);

        anim_triangle1 = GetComponent<Animator>();
        score = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {
        ScreenRepeating();
        MoveTowardDestination();
        Shot();
    }

    void Shot() 
    {
        if (shotDelay > 0f)
            shotDelay -= Time.deltaTime;

        if (isEnemyDestroyed == false) 
        {
            if (shotDelay <= 0)
            {
                Instantiate(projectile, transform.position, transform.rotation);
                shotDelay = Random.Range(1, 5);
                anim_triangle1.SetTrigger("shooting");
            }
        }
    }

    void SetNewDestination() 
    {
        wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
    }

    void MoveTowardDestination() 
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoint, velocity * Time.deltaTime);
        transform.up = new Vector2(wayPoint.x - transform.position.x, wayPoint.y - transform.position.y);

        if (Vector2.Distance(transform.position, wayPoint) < range) 
        {
            SetNewDestination();
        }
    }

    void ScreenRepeating()
    {
        Vector2 newPos = transform.position;

        if (transform.position.y > screenTop)
            newPos.y = screenBottom;

        if (transform.position.y < screenBottom)
            newPos.y = screenTop;

        if (transform.position.x > screenRight)
            newPos.x = screenLeft;

        if (transform.position.x < screenLeft)
            newPos.x = screenRight;

        transform.position = newPos;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "BlueProjectile":
                gameObject.tag = "Untagged";
                score.SendMessage("ScorePointsAdd", points);
                Destroy(other.gameObject);

                isEnemyDestroyed = true;
                Destroy(GetComponent<Rigidbody2D>());
                Destroy(GetComponent<PolygonCollider2D>());
                AudioSource.PlayClipAtPoint(destroy_sound, transform.position, .5f);

                anim_triangle1.SetTrigger("fired");
                break;
                
            case "Enemy":
                transform.position = Vector2.MoveTowards(transform.position, -wayPoint, velocity * Time.deltaTime);
                transform.up = new Vector2(wayPoint.x - transform.position.x, wayPoint.y - transform.position.y);
                break;
            case "RedProjectile":
                gameObject.tag = "Untagged";
                Destroy(other.gameObject);

                isEnemyDestroyed = true;
                Destroy(GetComponent<Rigidbody2D>());
                Destroy(GetComponent<PolygonCollider2D>());
                AudioSource.PlayClipAtPoint(destroy_sound, transform.position, .5f);

                anim_triangle1.SetTrigger("fired");
                break;
        }
    }
}