using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_delete : MonoBehaviour {

    
    void Start ()
    {
        
	}


    void Update ()
    {
		
	}

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("enemy"))
        {
            GameObject.Find("PL").GetComponent<player>().damege++;
            Destroy(other.gameObject);
        }
    }
}
