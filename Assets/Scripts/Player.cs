using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float mov_speed = 4;
    float delay = 0f;

    private GameObject score;
    public GameObject projectile;

    Animator anim_triangle;

    Vector2 offset;

    public AudioClip damage_sound;

    void Start()
    {
        score = GameObject.FindWithTag("MainCamera");
        anim_triangle = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        Shot();
        Boundaries();
        Aim();
    }

    void Aim() 
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 scrPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = new Vector2(mousePos.x - scrPoint.x, mousePos.y - scrPoint.y);

        transform.up = offset;
    }

    void Boundaries() 
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -5.7f, 5.7f);
        viewPos.y = Mathf.Clamp(viewPos.y, -4.4f, 4.4f);
        transform.position = viewPos;
    }

    void Shot() 
    {
        if (delay > 0f)
            delay -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && delay <= 0) 
        {
            Instantiate(projectile, transform.position, transform.rotation);
            delay = .5f;
            anim_triangle.SetTrigger("shooting");
        }
    }

    void Movement() 
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(0, mov_speed * Time.deltaTime, 0);
        else if (Input.GetKey(KeyCode.S))
            transform.Translate(0, -mov_speed * Time.deltaTime, 0);
        else if (Input.GetKey(KeyCode.D))
            transform.Translate(mov_speed * Time.deltaTime, 0, 0);
        else if (Input.GetKey(KeyCode.A)) 
            transform.Translate(-mov_speed * Time.deltaTime, 0, 0);
        else 
        {
            if (Vector2.Distance(transform.position, offset) < 5) 
            {
                transform.Translate(1 * Time.deltaTime, 0, 0);
            }
            else 
            {
                transform.Translate(0, 1 * Time.deltaTime, 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                AudioSource.PlayClipAtPoint(damage_sound, transform.position, .5f);
                score.SendMessage("ScorePointsDecr");

                Destroy(other.gameObject);

                anim_triangle.SetTrigger("damaged");
                break;
            case "RedProjectile":
                AudioSource.PlayClipAtPoint(damage_sound, transform.position, .5f);
                score.SendMessage("ScorePointsDecr");

                Destroy(other.gameObject);

                anim_triangle.SetTrigger("damaged");
                break;
            case "EnemyTriangle":
                AudioSource.PlayClipAtPoint(damage_sound, transform.position, .5f);
                score.SendMessage("ScorePointsDecr");

                Destroy(other.gameObject);

                anim_triangle.SetTrigger("damaged");
                break;
        }
    }
}
