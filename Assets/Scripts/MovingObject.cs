using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

    public float moveTime = 0.1f;
    public LayerMask blockingLayer = 256;
    public LayerMask pushingLayer = 512;
    public LayerMask winningLayer = 2048;
    public enum Obstacle { NONE, BLOCK, PUSH, WIN };


    [HideInInspector] public Obstacle obstacle;
    BoxCollider2D boxCollider;
    Rigidbody2D rigidBody;
    float moveSpeed;

    // Use this for initialization
    protected virtual void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        moveSpeed = 1f / moveTime;
    }

    protected Obstacle CheckObs(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);
        obstacle = Obstacle.NONE;

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        if (hit.transform != null)
            obstacle = Obstacle.BLOCK;
        else
        {
            hit = Physics2D.Linecast(start, end, pushingLayer);
            if (hit.transform != null)
                obstacle = Obstacle.PUSH;
            else
            {
                hit = Physics2D.Linecast(start, end, winningLayer);

                if (hit.transform != null)
                    obstacle = Obstacle.WIN;
            }
        }
        boxCollider.enabled = true;
        // Enable a collider does not fire OnTriggerEnter but disable one does fire OnTriggerExit
        // How ridiculous!
        if (obstacle == Obstacle.BLOCK)
        {
            transform.position = new Vector2(transform.position.x + 0.000001f, transform.position.y);
            transform.position = new Vector2(transform.position.x - 0.000001f, transform.position.y);
        }
        return obstacle;
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rigidBody.position, end, Time.deltaTime * moveSpeed);
            rigidBody.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

    protected virtual void AttemptMove(int xDir, int yDir) 
    {
        RaycastHit2D hit;//用于存储线性投射检测到的结构体信息（即障碍物）

        if (CheckObs(xDir, yDir, out hit) == Obstacle.NONE)
        {
            StartCoroutine(SmoothMovement((Vector2)(transform.position) + new Vector2(xDir, yDir)));
            //Debug.Log("No obstacle");
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
                case Obstacle.PUSH:
                    OnPushing(hit.transform, tag, xDir, yDir);
                    break;
                case Obstacle.WIN:
                    OnWinning(tag);
                    break;
                default:
                    break;

            }
        }
    }

    protected abstract void OnBlocking(Transform transform, string tag);

    protected virtual void OnPushing(Transform transform, string tag, int xDir, int yDir)
    {
        if (tag == "Rock" || tag.StartsWith("word"))
        {
            Debug.Log(name + " encounter Rock, Push");
            Rock pushRock = transform.GetComponent<Rock>();
            pushRock.Pushed(xDir, yDir);
            if (pushRock.obstacle == Obstacle.NONE)
            {
                this.obstacle = Obstacle.NONE;
                StartCoroutine(SmoothMovement((Vector2)(transform.position)));
            }
        }
        else if (tag == "Dada")
        {
            Player pushYou = transform.GetComponent<Player>();
            pushYou.Pushed(xDir, yDir);
            if (pushYou.obstacle == Obstacle.NONE)
            {
                this.obstacle = Obstacle.NONE;
                StartCoroutine(SmoothMovement((Vector2)(transform.position)));
            }
        }
    }

    protected abstract void OnWinning(string tag);

    public void Pushed(int xDir, int yDir)
    {
        AttemptMove(xDir, yDir); // I want it to change obstacle to BLOCK or PUSH
    }

    // Update is called once per frame
    void Update () {
		
	}
}
