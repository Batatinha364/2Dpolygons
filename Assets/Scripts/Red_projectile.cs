using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_projectile : MonoBehaviour
{
    private float speed = 8;
    private float rot = 20;

    PolygonCollider2D polygonCollider;

    public AudioClip shot_sound;

    void Start()
    {
        AudioSource.PlayClipAtPoint(shot_sound, transform.position, .5f);
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        Vector3 projectile_rot = new Vector3(0, 0, rot * Time.deltaTime);
        transform.Rotate(projectile_rot);
    }

    void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
}
