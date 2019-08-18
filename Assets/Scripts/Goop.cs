using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool lose = false;
        if(collision.tag=="Dada")
        {
            int count = GameObject.FindGameObjectsWithTag("Dada").Length;
            if (count == 1)
                lose = true;
        }
        Destroy(collision.gameObject);

        if (lose)
        {
            Debug.Log("You died");
            GameManager.instance.Invoke("RestartLevel", .1f);
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
