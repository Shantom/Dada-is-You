using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

    public float timer = 0.15f;

    Animator animator;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime; //limit the frequecy of player moving

        if (timer>0) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
            vertical = 0;

        if (horizontal != 0 || vertical != 0)
        {
            timer = .15f;
            AttemptMove(horizontal, vertical);
        }

    }

    protected override void AttemptMove(int xDir, int yDir)
    {
        base.AttemptMove(xDir, yDir);
    }

    protected override void OnBlocking(Transform transform, string tag)
    {
        if (tag == "Wall")
        {
            Debug.Log(name + " encounter Wall, Block");
        }
        else 
        {
            Debug.Log(name + " encounter " + transform.name);
            
        }
    }

    //protected override void OnPushing(Transform transform, string tag, int xDir, int yDir) 
    //{
    //    if(tag=="Rock"||tag.StartsWith("word"))
    //    {
    //        Debug.Log("encounter Rock, Push");
    //        Rock pushRock = transform.GetComponent<Rock>();
    //        pushRock.Pushed(xDir, yDir);
    //        if(pushRock.obstacle==Obstacle.NONE)
    //        {
    //            this.obstacle = Obstacle.NONE;
    //            StartCoroutine(SmoothMovement((Vector2)(transform.position)));
    //        }
    //    }
    //    else if(tag=="Dada")
    //    {
    //        Player pushYou= transform.GetComponent<Player>();
    //        pushYou.Pushed(xDir, yDir);
    //        if (pushYou.obstacle == Obstacle.NONE)
    //        {
    //            this.obstacle = Obstacle.NONE;
    //            StartCoroutine(SmoothMovement((Vector2)(transform.position)));
    //        }
    //    }
    //}

    protected override void OnWinning(string tag)
    {
        if (tag == "Flag")
        {
            Debug.Log("You Win");
            Instantiate(GameManager.instance.Win, transform.position, Quaternion.identity);
            GameManager.instance.Invoke("NextLevel", .1f);
        }
        // REPLACED BY ANOTHER WAY
        //else if (tag == "Spike")
        //{
        //    Debug.Log("You died");
        //    Instantiate(GameManager.instance.Death, transform.position, Quaternion.identity);
        //    GameManager.instance.Invoke("RestartLevel",.1f);
        //}
        
    }
}
