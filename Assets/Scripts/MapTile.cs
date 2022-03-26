using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile
{
    public Vector3Int position;
    public int level;

    public bool isWall;
    public bool isGround;

    public bool isEntity;
    public Entity entity;

    public int gCost;
    public int hCost;
    public int fCost;

    public MapTile parent;

    public MapTile()
    {
        position = Vector3Int.zero;
        level = 0;

        isWall = false;
        isGround = false;

        gCost = 0;
        hCost = 0;
        fCost = 0;

        parent = null;
    }

    public MapTile(int l, int x, int y, bool w, bool g)
    {
        position.x = x;
        position.y = y;
        level = l;

        isWall = w;
        isGround = g;

        gCost = 0;
        hCost = 0;
        fCost = 0;

        parent = null;
    }
}
