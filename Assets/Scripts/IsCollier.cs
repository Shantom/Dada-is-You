using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCollier : MonoBehaviour {

    new Collider2D collider;
    enum Item { NONE, WALL, SPIKE, FLAG, ROCK, DADA, GOOP, LAVA };
    string[] ItemString = { "None", "Walls", "Spikes", "Flags", "Rocks", "Dadas", "Goops","Lavas" };
    enum Property { NONE, STOP, KILL, WIN, PUSH, YOU, SINK, HOT, MELT, FLASH};
    List<Item> item;
    Item exitItem = Item.NONE;
    List<Property> property;
    Property exitProperty = Property.NONE;
    const int defaultLayer = 0;
    const int blockingLayer = 8;
    const int pushingLayer = 9;
    const int losingLayer = 10;
    const int winningLayer = 11;

    // Use this for initialization
    void Start () {
        collider = GetComponent<Collider2D>();
        item = new List<Item>();
        property = new List<Property>();
        //property.Add(Property.NONE);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.StartsWith("word"))
        {
            if (other.tag.EndsWith("wall"))
            {
                Debug.Log("wall entered");
                item.Add(Item.WALL);
            }
            else if (other.tag.EndsWith("rock"))
            {
                Debug.Log("rock entered");
                item.Add(Item.ROCK);
            }
            else if (other.tag.EndsWith("dada"))
            {
                Debug.Log("dada entered");
                item.Add(Item.DADA);
            }
            else if (other.tag.EndsWith("flag"))
            {
                Debug.Log("flag entered");
                item.Add(Item.FLAG);
            }
            else if (other.tag.EndsWith("spike"))
            {
                Debug.Log("spike entered");
                item.Add(Item.SPIKE);
            }
            else if (other.tag.EndsWith("goop"))
            {
                Debug.Log("goop entered");
                item.Add(Item.GOOP);
            }
            else if (other.tag.EndsWith("lava"))
            {
                Debug.Log("lava entered");
                item.Add(Item.LAVA);
            }
            else if (other.tag.EndsWith("stop"))
            {
                Debug.Log("stop entered");
                //property.Remove(Property.NONE);
                property.Add(Property.STOP);
            }
            else if (other.tag.EndsWith("push"))
            {
                Debug.Log("push entered");
               // property.Remove(Property.NONE);
                property.Add(Property.PUSH);
            }
            else if (other.tag.EndsWith("you"))
            {
                Debug.Log("you entered");
                //property.Remove(Property.NONE);
                property.Add(Property.YOU);
            }
            else if (other.tag.EndsWith("win"))
            {
                Debug.Log("win entered");
               // property.Remove(Property.NONE);
                property.Add(Property.WIN);
            }
            else if (other.tag.EndsWith("kill"))
            {
                Debug.Log("kill entered");
                //property.Remove(Property.NONE);
                property.Add(Property.KILL);
            }
            else if (other.tag.EndsWith("sink"))
            {
                Debug.Log("sink entered");
               // property.Remove(Property.NONE);
                property.Add(Property.SINK);
            }
            else if (other.tag.EndsWith("hot"))
            {
                Debug.Log("hot entered");
               //property.Remove(Property.NONE);
                property.Add(Property.HOT);
            }
            else if (other.tag.EndsWith("melt"))
            {
                Debug.Log("melt entered");
               // property.Remove(Property.NONE);
                property.Add(Property.MELT);
            }
            else if (other.tag.EndsWith("flash"))
            {
                Debug.Log("flash entered");
                //property.Remove(Property.NONE);
                property.Add(Property.FLASH);
            }
        }

        CheckMatching();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.StartsWith("word"))
        {
            if (other.tag.EndsWith("wall"))
            {
                Debug.Log("wall exited");
                item.Remove(Item.WALL);
                exitItem = Item.WALL;
            }
            else if (other.tag.EndsWith("rock"))
            {
                Debug.Log("rock exited");
                item.Remove(Item.ROCK);
                exitItem = Item.ROCK;
            }
            else if (other.tag.EndsWith("dada"))
            {
                Debug.Log("dada exited");
                item.Remove(Item.DADA);
                exitItem = Item.DADA;
            }
            else if (other.tag.EndsWith("flag"))
            {
                Debug.Log("flag exited");
                item.Remove(Item.FLAG);
                exitItem = Item.FLAG;
            }
            else if (other.tag.EndsWith("spike"))
            {
                Debug.Log("spike exited");
                item.Remove(Item.SPIKE);
                exitItem = Item.SPIKE;
            }
            else if (other.tag.EndsWith("goop"))
            {
                Debug.Log("goop exited");
                item.Remove(Item.GOOP);
                exitItem = Item.GOOP;
            }
            else if (other.tag.EndsWith("lava"))
            {
                Debug.Log("lava exited");
                item.Remove(Item.LAVA);
                exitItem = Item.LAVA;
            }
            else if (other.tag.EndsWith("stop"))
            {
                Debug.Log("stop exited");
                property.Remove(Property.STOP);
                exitProperty = Property.STOP;
            }
            else if (other.tag.EndsWith("push"))
            {
                Debug.Log("push exited");
                property.Remove(Property.PUSH);
                exitProperty = Property.PUSH;
            }
            else if (other.tag.EndsWith("you"))
            {
                Debug.Log("you exited");
                property.Remove(Property.YOU);
                exitProperty = Property.YOU;
            }
            else if (other.tag.EndsWith("win"))
            {
                Debug.Log("win exited");
                property.Remove(Property.WIN);
                exitProperty = Property.WIN;
            }
            else if (other.tag.EndsWith("kill"))
            {
                Debug.Log("kill exited");
                property.Remove(Property.KILL);
                exitProperty = Property.KILL;
            }
            else if (other.tag.EndsWith("sink"))
            {
                Debug.Log("sink exited");
                property.Remove(Property.SINK);
                exitProperty = Property.SINK;
            }
            else if (other.tag.EndsWith("hot"))
            {
                Debug.Log("hot exited");
                property.Remove(Property.HOT);
                exitProperty = Property.HOT;
            }
            else if (other.tag.EndsWith("melt"))
            {
                Debug.Log("melt exited");
                property.Remove(Property.MELT);
                exitProperty = Property.MELT;
            }
            else if (other.tag.EndsWith("flash"))
            {
                Debug.Log("flash exited");
                property.Remove(Property.FLASH);
                exitProperty = Property.FLASH;
            }
        }

        CheckMatching();
    }

    void resetPropertyToItem(Item item, Property property)
    {
        string objectName = ItemString[(int)item];
        GameObject objs = GameObject.Find(objectName);
        for (int i = 0; i < objs.transform.childCount; i++)
        {
            Transform obj = objs.transform.GetChild(i);
            
            switch(property)
            {
                case Property.PUSH:
                    Destroy(obj.gameObject.GetComponent<Rock>());
                    break;
                case Property.YOU:
                    Destroy(obj.gameObject.GetComponent<Player>());
                    break;
                case Property.SINK:
                    Destroy(obj.gameObject.GetComponent<Goop>());
                    break;
                case Property.KILL:
                    Destroy(obj.gameObject.GetComponent<Spike>());
                    break;
                case Property.HOT:
                    Destroy(obj.gameObject.GetComponent<Lava>());
                    break;
            }
            if (property == Property.FLASH)
            {
                if (obj.Find("Flash") != null)
                    Destroy(obj.Find("Flash").gameObject);
            }
            else if (property == Property.MELT)
            {
                Destroy(obj.gameObject.GetComponent<Melt>());
            }
            else
            {
                obj.gameObject.tag = "Untagged";
                obj.gameObject.layer = defaultLayer;
            }
        }
    }

    void CheckMatching()
    {
        if (item.Count == 1 && property.Count == 1) // Normal situation
        {
            setPropertyToItem(item[0], property[0]);
            Debug.Log("Set " + item[0] + " " + property[0]);
        }

        if ((item.Count == 2 || property.Count == 2)) // 2 items or 2 properties
            return;
        if (item.Count == 0 && property.Count == 0) // nothing
            return;

        if (item.Count == 1 || property.Count == 1) // need to reset property to item
        {
            /*There is a bug that I do not know how to solve it. When a 'word' is pushed, it has to turn off and turn on its collider, but the time interval is too small to make this function act at a proper time. Techonically, the order should have been turn off -> exit -> turn on -> enter. But the truth is turn off -> turn on -> exit -> enter. Luckily, no misaction so far.*/
            if (exitItem != Item.NONE) //if item leaves
            {
                Debug.Log("Reset " + exitItem + " " + property[0]);
                resetPropertyToItem(exitItem, property[0]);
                exitItem = Item.NONE;
            }
            else if (exitProperty != Property.NONE)// if property leaves
            {
                Debug.Log("Reset " + item[0] + " " + exitProperty);
                resetPropertyToItem(item[0], exitProperty);
                exitProperty = Property.NONE;
            }
            //else: no way to be here
        }
        else// the last one leaves, means nothing remains
        {
            exitItem = Item.NONE;
            exitProperty = Property.NONE;
        }


    }

    void setPropertyToItem(Item item, Property property)
    {
        string objectName = ItemString[(int)item];
        GameObject objs = GameObject.Find(objectName);
        for(int i=0; i< objs.transform.childCount;i++)
        {
            Transform obj = objs.transform.GetChild(i);
            switch(property)
            {
                case Property.STOP:
                    obj.gameObject.layer = blockingLayer;
                    obj.gameObject.tag = "Wall";
                    break;
                case Property.PUSH:
                    obj.gameObject.layer = pushingLayer;
                    obj.gameObject.tag = "Rock";
                    obj.gameObject.AddComponent<Rock>();
                    break;
                case Property.YOU:
                    obj.gameObject.layer = pushingLayer;
                    obj.gameObject.tag = "Dada";
                    obj.gameObject.AddComponent<Player>();
                    break;
                case Property.WIN:
                    obj.gameObject.layer = winningLayer;
                    obj.gameObject.tag = "Flag";
                    break;
                case Property.KILL:
                    obj.gameObject.layer = defaultLayer;
                    obj.gameObject.tag = "Spike";
                    obj.gameObject.AddComponent<Spike>();
                    break;
                case Property.SINK:
                    obj.gameObject.layer = defaultLayer;
                    obj.gameObject.tag = "Goop";
                    obj.gameObject.AddComponent<Goop>();
                    break;
                case Property.HOT:
                    obj.gameObject.layer = defaultLayer;
                    obj.gameObject.tag = "Lava";
                    obj.gameObject.AddComponent<Lava>();
                    break;
                case Property.MELT:
                    obj.gameObject.AddComponent<Melt>();
                    break;
                case Property.FLASH:
                    Instantiate(GameManager.instance.Flash, obj).name = "Flash";
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
