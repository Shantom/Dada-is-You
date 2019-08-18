using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour {

    public Transform LevelHolder;
    public Button LevelButton;

	// Use this for initialization
	void Start () {
        GameObject levels = GameObject.Find("Levels");
        Vector2 buttonPos = new Vector2(-690f, 228f);
        for (int i = 1; i < GameManager.instance.levelCount+1; i++) {
            
            LevelButton.transform.GetChild(0).GetComponent<Text>().text = ""+(i);
            Button newButton = Instantiate(LevelButton, LevelHolder);
            newButton.name = "Level " + (i);
            newButton.GetComponent<RectTransform>().anchoredPosition = buttonPos;
            int j = i;
            newButton.onClick.AddListener(()=>GameManager.instance.startLevel(j));
            if (buttonPos.x < 680f) 
                buttonPos.x += 345f;
            else
            {
                buttonPos.x = -690f;
                buttonPos.y -= 345f;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
