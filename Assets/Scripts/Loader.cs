using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

    public GameObject gameManager;

    void Awake()
    {
        if (GameManager.instance == null)
            Instantiate(gameManager);
        if (SceneManager.GetActiveScene().name.StartsWith("Level "))
        {
            GameManager.instance.BoardSetup();
            Debug.Log("BoardSetup");
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
