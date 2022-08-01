using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagon : MonoBehaviour
{
    public float maxTorque;
    public Rigidbody2D rb;

    public float maxVelocity;
    private float velocity;

    private float torque;

    private float screenTop;
    private float screenBottom;
    private float screenRight;
    private float screenLeft;

    private int points = 1;

    private GameObject score;
    private Transform player;

    public AudioClip destroy_sound;

    Animator anim_pentagon;

    void Start()
    {
        screenTop = 5.88f;
        screenBottom = -5.88f;
        screenRight = 7.2f;
        screenLeft = -7.2f;

        torque = Random.Range(-maxTorque, maxTorque);
        velocity = Random.Range(0, maxVelocity);

        rb.AddTorque(torque);

        anim_pentagon = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        score = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {
        ScreenRepeating();
        MoveTowardPlayer();
    }

    void MoveTowardPlayer() 
    {
        if (player != null)
            transform.position = Vector2.MoveTowards(transform.position, player.position, velocity * Time.deltaTime);
        else
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, velocity * Time.deltaTime);
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

                Destroy(GetComponent<Rigidbody2D>());
                Destroy(GetComponent<PolygonCollider2D>());
                AudioSource.PlayClipAtPoint(destroy_sound, transform.position, .5f);

                anim_pentagon.SetTrigger("fired");
                break;
            case "Enemy":
                gameObject.tag = "Untagged";
                Destroy(other.gameObject);

                Destroy(GetComponent<Rigidbody2D>());
                Destroy(GetComponent<PolygonCollider2D>());
                AudioSource.PlayClipAtPoint(destroy_sound, transform.position, .5f);

                anim_pentagon.SetTrigger("fired");
                break;
            case "RedProjectile":
                gameObject.tag = "Untagged";
                Destroy(other.gameObject);

                Destroy(GetComponent<Rigidbody2D>());
                Destroy(GetComponent<PolygonCollider2D>());
                AudioSource.PlayClipAtPoint(destroy_sound, transform.position, .5f);

                anim_pentagon.SetTrigger("fired");
                break;
        }
    }
}