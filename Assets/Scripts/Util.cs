﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    
    public static Vector2 rightDirection = new Vector2(1, 0);
    public static Vector2 leftDirection = new Vector2(-1, 0);
    public static Vector2 UpDirection = new Vector2(0, 1);
    public static Vector2 DownDirection = new Vector2(0, -1);

    public const int RIGHT_DIR = 0;
    public const int LEFT_DIR = 1;
    public const int UP_DIR = 2;
    public const int DOWN_DIR = 3;

    public static int mapSize = 5;

    public static int playableArea = 4;

    public enum RoomType
    {
        GOOD = 0, BAD = 1, NORMAL = 2, MAIN = 3
    }

    public enum Curse
    {
        NOTHING, FREEZEBULLET, SPEEDUPENEMIES, LIGHTSOUT, FRIENDFIRE, INCREASETHREAT 
    }

    public static float bulletSpeed = 1f;
    public static float enemiesSpeed = 1f;
    public static float rotationSpeed = 1f;
    public static bool lightsOut = false;
    public static bool friendFire = false;
    public static bool increaseThreat = false;
    public static bool reduceTime = false;
    
    public static void setCurseType(int currentCurse)
    {
        switch (currentCurse)
        {
            case 0:
                //Nothing
                bulletSpeed = 1f;
                enemiesSpeed = 1f;
                rotationSpeed = 1f;
                lightsOut = false;
                friendFire = false;
                increaseThreat = false;
                reduceTime = false;
                break;
            case 1:
                //Freeze Bullets
                bulletSpeed = 0.45f;
                enemiesSpeed = 1f;
                rotationSpeed = 1f;
                lightsOut = false;
                friendFire = false;
                increaseThreat = false;
                break;
            case 2:
                //Reduce time
                bulletSpeed = 1f;
                enemiesSpeed = 1f;
                rotationSpeed = 1f;
                lightsOut = false;
                friendFire = false;
                increaseThreat = false;
                reduceTime = true;
                break;
            case 3:
                //Speed up enemies
                bulletSpeed = 1f;
                enemiesSpeed = 1.8f;
                rotationSpeed = 2f;
                lightsOut = false;
                friendFire = false;
                increaseThreat = false;
                reduceTime = false;
                break;
            case 4:
                //Lights out
                bulletSpeed = 1f;
                enemiesSpeed = 1f;
                rotationSpeed = 1f;
                lightsOut = true;
                friendFire = false;
                increaseThreat = false;
                reduceTime = false;
                break;
            case 5:
                //Friend fire
                bulletSpeed = 1f;
                enemiesSpeed = 1f;
                rotationSpeed = 1f;
                lightsOut = false;
                friendFire = true;
                increaseThreat = false;
                reduceTime = false;
                break;
            case 6:
                //Increase threat
                bulletSpeed = 1f;
                enemiesSpeed = 1f;
                rotationSpeed = 1f;
                lightsOut = false;
                friendFire = false;
                increaseThreat = true;
                reduceTime = false;
                break;
        }
    }
    

    public static Vector2 getRandomPosition(Transform parent, float substractOffset)
    {
        float minX = parent.position.x - playableArea + substractOffset;
        float maxX = parent.position.x + playableArea - substractOffset;
        float minY = parent.position.y - playableArea + substractOffset;
        float maxY = parent.position.y + playableArea - substractOffset;

        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    public static bool isInsideMinDistance(float minDistance, List<Vector2> positions, Vector2 newPos)
    {
        foreach (Vector2 pos in positions)
        {
            if (Vector2.Distance(pos, newPos) < minDistance) return true;
        }
        return false;
    }

    public static Vector2[] getCorners(Vector2 currentPos)
    {
        Vector2[] corners = new Vector2[4];

        corners[0] = new Vector2(currentPos.x + 4f, currentPos.y - 3.5f);
        corners[1] = new Vector2(currentPos.x + 4f, currentPos.y + 3.5f);
        corners[2] = new Vector2(currentPos.x - 4f, currentPos.y + 3.5f);
        corners[3] = new Vector2(currentPos.x - 4f, currentPos.y - 3.5f);

        return corners;
    }

    

    public static Vector3 getOneRandomSidePosition(Transform parent)
    {
        Vector2 parentPosition = parent.transform.position;
        Vector3[] posiblePositions =
        {
            new Vector3(parentPosition.x, parentPosition.y + playableArea, 0),
            new Vector3(parentPosition.x, parentPosition.y - playableArea, 1),
            new Vector3(parentPosition.x + playableArea, parentPosition.y, 2),
            new Vector3(parentPosition.x - playableArea, parentPosition.y, 3),
        };
        return posiblePositions[Random.Range(0, posiblePositions.Length)];
    }

    public static CentipedePoint getNearestTarget(Transform currentPosition, CentipedePoint[] elements)
    {
        CentipedePoint nearestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        foreach (CentipedePoint potentialTarget in elements)
        {
            if (!potentialTarget.gameObject.activeInHierarchy) break;
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                nearestTarget = potentialTarget;
            }
        }

        return nearestTarget;
    }
}
