﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour
{
    public CentipedeBody[] body;
    public Room currentRoom;
    public GameObject centipedeHead;
    public GameObject centipedeBody;

    private int maxBody;

    public void initBody(Room r)
    {
        currentRoom = r;
        
        createBody();

        for(int i = 0; i < body.Length; i++)
        {
            if (i == 0)
            {
                body[i].setCentipedeAttributes(i, currentRoom);
                body[i].nextBody = null;
                body[i].lastBody = body[i + 1];
            }
            else
            {
                ((CentipedeTail)body[i]).currentHead = (CentipedeHead)body[0];
                body[i].setCentipedeAttributes(i, currentRoom);
                if (i == body.Length - 1) body[i].lastBody = null;
                else body[i].lastBody = body[i + 1];
                body[i].nextBody = body[i - 1];
            }
            Main.Instance.enemies.Add(body[i].transform);
        }
    }

    private void createBody()
    {
        maxBody = Random.Range(10, 15);
        body = new CentipedeBody[maxBody];
        for (int i = 0; i < body.Length; i++)
        {
            if(i == 0)
            {
                GameObject newCentipedeHead = Instantiate(centipedeHead, transform);
                body[i] = newCentipedeHead.transform.GetChild(1).GetComponent<CentipedeHead>();
            }
            else
            {
                GameObject newCentipedeTail = Instantiate(centipedeBody, transform);
                body[i] = newCentipedeTail.transform.GetChild(1).GetComponent<CentipedeTail>();
            }
        }
    }
}
