using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrieat : MonoBehaviour {

    public GameObject[] enemy;

    private float time = 5;
    private int num = 0;
    

	void Start ()
    {
		
	}
	

	void Update ()
    {
        time += Time.deltaTime;
        if (time > 5)
        {
            num = Random.Range(0, 100) % enemy.Length;
            Instantiate(enemy[num], new Vector3(0, 6.5f, 0), Quaternion.identity);
            time = 0;
            GameObject.Find("PL").GetComponent<player>().enemy_level += (num+1)*3;
        }

	}
}
