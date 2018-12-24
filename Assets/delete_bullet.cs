using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete_bullet : MonoBehaviour {

    private float time = 0;
    private float size = 1;
    private Vector3 start_size;

	void Start ()
    {
        start_size = this.transform.localScale;

    }

	void Update ()
    {
        time += Time.deltaTime;
        if (time > 3)
        {
            size -= Time.deltaTime * 0.2f;
            this.GetComponent<Transform>().localScale = start_size * size;
        }
        if (size < 0.1)
        {
            Destroy(this.gameObject);
        }
        
	}

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
