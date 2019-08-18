using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsWord : MonoBehaviour {

    GameObject core;
    Collider2D coreColl;
    GameObject horizontal;
    Collider2D horiColl;
    GameObject vertical;
    Collider2D verColl;

    // Use this for initialization
    void Start () {
        core = transform.Find("Core").gameObject;
        coreColl = core.GetComponent<Collider2D>();
        horizontal = transform.Find("HoriColl").gameObject;
        horiColl = core.GetComponent<Collider2D>();
        vertical = transform.Find("VerColl").gameObject;
        verColl = core.GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
