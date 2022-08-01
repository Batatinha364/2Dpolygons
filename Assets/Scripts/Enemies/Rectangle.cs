using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : MonoBehaviour
{
    public float maxThrust;
    public float maxTorque;
    public Rigidbody2D rb;

    private float screenTop;
    private float screenBottom;
    private float screenRight;
    private float screenLeft;

    private int points = 1;
    private GameObject score;

    public AudioClip destroy_sound;

    Animator anim_rectangle;

    void Start()
    {
        screenTop = 5.88f;
        screenBottom = -5.88f;
        screenRight = 7.2f;
        screenLeft = -7.2f;

        Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        float torque = Random.Range(-maxTorque, maxTorque);

        rb.AddForce(thrust);
        rb.AddTorque(torque);

        anim_rectangle = GetComponent<Animator>();

        score = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {
        ScreenRepeating();
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

                anim_rectangle.SetTrigger("fired");
                break;
            case "Enemy":
                gameObject.tag = "Untagged";
                Destroy(other.gameObject);

                Destroy(GetComponent<Rigidbody2D>());
                Destroy(GetComponent<PolygonCollider2D>());
                AudioSource.PlayClipAtPoint(destroy_sound, transform.position, .5f);

                anim_rectangle.SetTrigger("fired");
                break;
            case "RedProjectile":
                gameObject.tag = "Untagged";
                Destroy(other.gameObject);

                Destroy(GetComponent<Rigidbody2D>());
                Destroy(GetComponent<PolygonCollider2D>());
                AudioSource.PlayClipAtPoint(destroy_sound, transform.position, .5f);

                anim_rectangle.SetTrigger("fired");
                break;
        }
    }
}