using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    public TextAsset mapFile;

    public MapTile[,,] map;
    public bool isMapLoaded;
    public int mapHeight;
    public int mapWidth;
    public int mapLevels;

    public Vector3Int startPlayerPos;
    public int startPlayerLevel;

    void Start()
    {
        map = null;
        isMapLoaded = false;
        mapHeight = 0;
        mapWidth = 0;
        mapLevels = 0;

        startPlayerPos = Vector3Int.zero;
        startPlayerLevel = 0;
}

    public MapTile GetTile(int level, Vector3Int position)
    {
        MapTile mapTile = null;

        if (level >= 0 && level < mapLevels && position.x >= 0 && position.x < mapWidth && position.y >= 0 && position.y < mapHeight)
            mapTile = map[level, position.y, position.x];

        return mapTile;
    }

    public MapTile GetTileLeft(MapTile tileIn)
    {
        MapTile tileOut = null;
        if (tileIn.position.x > 0)
            tileOut = map[tileIn.level, tileIn.position.y, tileIn.position.x - 1];
        return tileOut;
    }

    public MapTile GetTileRight(MapTile tileIn)
    {
        MapTile tileOut = null;
        if (tileIn.position.x < mapWidth - 1)
            tileOut = map[tileIn.level, tileIn.position.y, tileIn.position.x + 1];
        return tileOut;
    }

    public MapTile GetTileUp(MapTile tileIn)
    {
        MapTile tileOut = null;
        if (tileIn.position.y < mapHeight -1)
            tileOut = map[tileIn.level, tileIn.position.y + 1, tileIn.position.x];
        return tileOut;
    }

    public MapTile GetTileDown(MapTile tileIn)
    {
        MapTile tileOut = null;
        if (tileIn.position.y > 0)
            tileOut = map[tileIn.level, tileIn.position.y - 1, tileIn.position.x];
        return tileOut;
    }

    public MapTile GetTileUpLeft(MapTile tileIn)
    {
        MapTile tileOut = null;
        if (tileIn.position.y < mapHeight -1 && tileIn.position.x > 0)
            tileOut = map[tileIn.level, tileIn.position.y + 1, tileIn.position.x - 1];
        return tileOut;
    }

    public MapTile GetTileUpRight(MapTile tileIn)
    {
        MapTile tileOut = null;
        if (tileIn.position.y < mapHeight -1 && tileIn.position.x < mapWidth - 1)
            tileOut = map[tileIn.level, tileIn.position.y + 1, tileIn.position.x + 1];
        return tileOut;
    }

    public MapTile GetTileDownLeft(MapTile tileIn)
    {
        MapTile tileOut = null;
        if (tileIn.position.y > 0 && tileIn.position.x > 0)
            tileOut = map[tileIn.level, tileIn.position.y - 1, tileIn.position.x - 1];
        return tileOut;
    }

    public MapTile GetTileDownRight(MapTile tileIn)
    {
        MapTile tileOut = null;
        if (tileIn.position.y > 0 && tileIn.position.x < mapWidth - 1)
            tileOut = map[tileIn.level, tileIn.position.y - 1, tileIn.position.x + 1];
        return tileOut;
    }

    public MapTile GetTileBelow(MapTile tileIn)
    {
        MapTile tileOut = null;
        if (tileIn.level > 0)
            tileOut = map[tileIn.level - 1, tileIn.position.y, tileIn.position.x];
        return tileOut;
    }

    public MapTile GetTileAbove(MapTile tileIn)
    {
        MapTile tileOut = null;
        if (tileIn.level < mapLevels - 1)
            tileOut = map[tileIn.level + 1, tileIn.position.y, tileIn.position.x];
        return tileOut;
    }

    public void RefreshEntities()
    {
        foreach(MapTile m in map)
        {
            m.isEntity = false;
            m.entity = null;
        }

        foreach(Entity e in GameManager.I.entities)
        {
            GetTile(e.level, e.position).isEntity = true;
            GetTile(e.level, e.position).entity = e;
        }
    }

    public void LoadMap()
    {
        string[] mapLines = null;

        mapLines = mapFile.text.Split('\n');

        mapLevels = int.Parse(mapLines[0]);
        mapHeight = int.Parse(mapLines[1]);
        mapWidth = int.Parse(mapLines[2]);

        map = new MapTile[mapLevels, mapHeight, mapWidth];

        for (int i = 0; i < mapLevels; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                for (int k = 0; k < mapWidth; k++)
                {
                    MapTile mapTile = new MapTile();

                    mapTile.level = i;
                    mapTile.position.y = j;
                    mapTile.position.x = k;

                    mapTile.isWall = mapLines[4 + i * (mapHeight + 1) + j][k] == 'x';

                    if (!mapTile.isWall && (GetTileBelow(mapTile) == null || GetTileBelow(mapTile).isWall))
                    {
                        mapTile.isGround = true;
                    }

                    map[i, j, k] = mapTile;
                }
            }
        }

        int numOfEntities = int.Parse(mapLines[4 + mapLevels * (mapHeight + 1)]);

        GameManager.I.entities = new Entity[numOfEntities];

        for (int i = 0; i < numOfEntities; i++)
        {
            string entity = mapLines[4 + mapLevels * (mapHeight + 1) + 1 + i];

            string[] parts = entity.Split(' ');

            switch (parts[0])
            {
                case "player":

                    GameManager.I.entities[i] = new Entity(EntityType.player,
                        int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
                    GameManager.I.playerEntity = GameManager.I.entities[i];
                    startPlayerLevel = GameManager.I.entities[i].level;
                    startPlayerPos = GameManager.I.entities[i].position;

                    break;
                case "elevator":

                    GameManager.I.entities[i] = new EntityElevator(int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]),
                        bool.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6]), int.Parse(parts[7]));

                    break;
            }
        }
    }
}
