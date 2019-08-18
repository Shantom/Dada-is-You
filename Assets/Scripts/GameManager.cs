using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    [HideInInspector] public bool playersTurn = true;
    public int columns = 15;
    public int rows = 9;
    public GameObject Border;
    public GameObject Floor;
    public GameObject Death;
    public GameObject Flash;
    public GameObject Win;
    public GameObject PauseMenu;

    GameObject curPauseMenu;
    public int level = 1;
    public int levelCount = 2;

    Transform boardHolder;

    // Use this for initialization
    void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        
    }

    public void loadTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }

    public void levelSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelection");
    }


    public void startLevel(int lv = -1)
    {
        if (lv != -1)
            level = lv;
        Debug.Log(level);
        SceneManager.LoadScene("Level " + level);
    }

    public void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = Floor;
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = Border;
                    instance = Instantiate(toInstantiate, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
            }
        }
    }

    public void NextLevel()
    {
        if (level < levelCount)
        {
            level++;
            startLevel();
        }
        else
        {
            SceneManager.LoadScene("Acknowledgment");
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        startLevel();
    }
    // Update is called once per frame
    void Update () {
        if (SceneManager.GetActiveScene().name.StartsWith("Level ") && Time.timeScale > 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            onPause();
        }
    }
    void onPause()
    {
        Time.timeScale = 0;
        curPauseMenu=Instantiate(PauseMenu);
        Button Resume = GameObject.Find("ResumeButton").GetComponent<Button>();
        Resume.onClick.AddListener(onResume);
        Button Restart = GameObject.Find("RestartButton").GetComponent<Button>();
        Restart.onClick.AddListener(RestartLevel);
    }
    public void onResume()
    {
        Time.timeScale = 1f;
        Debug.Log(curPauseMenu == null);
        Destroy(curPauseMenu);
    }
}
