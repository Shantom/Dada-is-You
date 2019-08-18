using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

    List<GameObject> explosions;
	// Use this for initialization
	void Start () {
        explosions = new List<GameObject>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool lose = false;
        if (collision.tag == "Dada")
        {
            int count = GameObject.FindGameObjectsWithTag("Dada").Length;
            if (count == 1)
                lose = true;
        }
        GameObject explosion = Instantiate(GameManager.instance.Death, transform.position, Quaternion.identity);
        explosions.Add(explosion);
        Destroy(collision.gameObject);
        Invoke("DestroyAnim", .1f);
        if (lose)
        {
            Debug.Log("You died");
            GameManager.instance.Invoke("RestartLevel", .1f);
        }
        
    }

    private void DestroyAnim()
    {
        Destroy(explosions[0]);
        explosions.RemoveAt(0);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
