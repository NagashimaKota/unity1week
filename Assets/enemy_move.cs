using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move : MonoBehaviour {

    public ParticleSystem explosion;

    private int dame = 0;
    private float dilection = 0.1f;
    private float move = -0.01f;
    private Transform enemy;
    private player count;

	void Start ()
    {
        enemy = this.GetComponent<Transform>();
        count = GameObject.Find("PL").GetComponent<player>();
        move = Random.Range(-0.01f, -0.04f);
	}
	
	
	void Update ()
    {
        enemy.Translate( dilection, move, 0);

        if (this.transform.position.y > 8)
        {
            count.count++;
            Destroy(this.gameObject);
        }
        else if(dame > 9)
        {
            count.count++;
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        dilection *= -1;

        if (col.gameObject.CompareTag("Bullet"))
        {
            explosion.Play();
            this.GetComponent<AudioSource>().Play();
            dame++;
        }
    }
    
}
