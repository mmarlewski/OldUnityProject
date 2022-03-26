using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : Singleton<PathfindingManager>
{
    public MapTile currentTile;
    public MapTile startTile;
    public MapTile endTile;
    public List<MapTile> openTiles;
    public List<MapTile> closedTiles;
    public List<MapTile> neighbourTiles;
    public List<MapTile> path;
    public bool isTraversable;

    void Start()
    {
        currentTile = null;
        startTile = null;
        endTile = null;
        openTiles = new List<MapTile>();
        closedTiles = new List<MapTile>();
        neighbourTiles = new List<MapTile>();
        path = new List<MapTile>();
        isTraversable = false;
    }

    public int GetDistance(MapTile start, MapTile end)
    {
        int distance = 0;

        int yDist = Mathf.Abs(start.position.y - end.position.y);
        int xDist = Mathf.Abs(start.position.x - end.position.x);

        if (yDist < xDist) distance = 14 * yDist + 10 * (xDist - yDist);
        else distance = 14 * xDist + 10 * (yDist - xDist);

        return distance;
    }

    public void FindPath(Vector3Int startPath, Vector3Int endPath)
    {
        startTile = MapManager.I.GetTile(CameraManager.I.currCameraLevel, startPath);
        endTile = MapManager.I.GetTile(CameraManager.I.currCameraLevel, endPath);

        openTiles.Clear();
        closedTiles.Clear();
        neighbourTiles.Clear();
        path.Clear();
        isTraversable = false;
        openTiles.Add(startTile);

        bool continueLoop = true;

        while (openTiles.Count > 0 && continueLoop)
        {
            currentTile = openTiles[0];//Debug.Log("Current tile: " + currentTile.position.x + " " + currentTile.position.y);

            foreach (MapTile m in openTiles)
            {
                if (currentTile.fCost > m.fCost)
                {
                    currentTile = m;
                }
                else if (currentTile.fCost == m.fCost)
                {
                    if (currentTile.hCost > m.hCost)
                    {
                        currentTile = m;
                    }
                }
            }

            openTiles.Remove(currentTile);
            closedTiles.Add(currentTile);

            neighbourTiles.Clear();

            MapTile tileLeft = MapManager.I.GetTileLeft(currentTile);
            MapTile tileRight = MapManager.I.GetTileRight(currentTile);
            MapTile tileUp = MapManager.I.GetTileUp(currentTile);
            MapTile tileDown = MapManager.I.GetTileDown(currentTile);
            MapTile tileUpLeft = MapManager.I.GetTileUpLeft(currentTile);
            MapTile tileUpRight = MapManager.I.GetTileUpRight(currentTile);
            MapTile tileDownLeft = MapManager.I.GetTileDownLeft(currentTile);
            MapTile tileDownRight = MapManager.I.GetTileDownRight(currentTile);

            if (tileUpLeft != null) neighbourTiles.Add(tileUpLeft);
            if (tileUp != null) neighbourTiles.Add(tileUp);
            if (tileUpRight != null) neighbourTiles.Add(tileUpRight);
            if (tileRight != null) neighbourTiles.Add(tileRight);
            if (tileDownRight != null) neighbourTiles.Add(tileDownRight);
            if (tileDown != null) neighbourTiles.Add(tileDown);
            if (tileDownLeft != null) neighbourTiles.Add(tileDownLeft);
            if (tileLeft != null) neighbourTiles.Add(tileLeft);

            foreach (MapTile m in neighbourTiles)
            {
                if (m == endTile)
                {
                    isTraversable = true;

                    MapTile tile = currentTile;

                    while (tile != startTile)
                    {
                        path.Add(tile);
                        tile = tile.parent;
                    }

                    path.Reverse();

                    path.Add(endTile);

                    continueLoop = false;

                    break;
                }

                if (m.isGround && !m.isEntity && !closedTiles.Contains(m))
                {
                    if (currentTile.gCost + GetDistance(currentTile, m) < m.gCost || !openTiles.Contains(m))
                    {
                        m.gCost = currentTile.gCost + GetDistance(currentTile, m);
                        m.hCost = GetDistance(endTile, m);
                        m.fCost = m.gCost + m.hCost;
                        m.parent = currentTile;

                        if (!openTiles.Contains(m))
                        {
                            openTiles.Add(m);
                        }
                    }
                }
            }
        }
    }
}
