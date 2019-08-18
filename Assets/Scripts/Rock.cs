using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MovingObject {


    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update () {
		
	}

    protected override void AttemptMove(int xDir, int yDir)
    {
        RaycastHit2D hit;//用于存储线性投射检测到的结构体信息（即障碍物）

        if (CheckObs(xDir, yDir, out hit) == Obstacle.NONE)
        {
            StartCoroutine(SmoothMovement((Vector2)(transform.position) + new Vector2(xDir, yDir)));
            return;
        }
        else
        {
            string tag = hit.transform.tag;
            //if something is in the way
            Transform hitComponent = hit.transform;
            switch (obstacle)
            {
                case Obstacle.BLOCK:
                    OnBlocking(hitComponent, tag);
                    break;
                case Obstacle.WIN:
                    OnBlocking(hitComponent, tag);
                    break;
                case Obstacle.PUSH:
                    OnPushing(hit.transform, tag, xDir, yDir);
                    break;
                default:
                    break;

            }
        }
    }

    protected override void OnBlocking(Transform transform, string tag)
    {
        if (tag == "Wall")
        {
            Debug.Log(name+" encounter Wall");
        }
        else if (tag == "Spike")
        {
            Debug.Log(name + " encounter Spike");
        }
        else if (tag == "Win")
        {
            Debug.Log(name + " encounter Win");
        }
    }

    //protected override void OnPushing(Transform transform, string tag, int xDir, int yDir)
    //{
    //    if (tag == "Rock" || tag.StartsWith("word"))
    //    {
    //        Debug.Log("encounter Rock, Push");
    //        Rock pushRock = transform.GetComponent<Rock>();
    //        pushRock.Pushed(xDir, yDir); //I think this is beautiful!!! I made it using Recursion
    //        if (pushRock.obstacle == Obstacle.NONE)
    //        {
    //            this.obstacle = Obstacle.NONE;
    //            StartCoroutine(SmoothMovement((Vector2)(transform.position)));
    //        }
    //        // multi push will be allowed
    //    }
    //}

    protected override void OnWinning(string tag)
    {
        //do nothing
    }

    
}
