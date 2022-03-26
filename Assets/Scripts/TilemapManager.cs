using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TilemapManager : Singleton<TilemapManager>
{
    public Tilemap downwallTilemap;
    public Tilemap entityTilemap;
    public Tilemap selectTilemap;
    public Tilemap upwallTilemap;
    public Tilemap baseTilemap;

    public Vector3 mousePos;
    public Vector3Int selectPos;
    public Vector3Int newSelectPos;
    public bool isSelWithinBounds;
    public MapTile selectTile;

    void Start()
    {
        selectPos = new Vector3Int(0, 0, 0);
        selectTile = null;
    }

    public void SetTile(Tilemap tilemap, Vector3Int tilePos, TileBase tilebase)
    {
        tilemap.SetTile(tilePos, tilebase);
    }

    public void ClearTilemap(Tilemap tilemap)
    {
        tilemap.ClearAllTiles();
    }

    public void RefreshTilemaps()
    {
        foreach (MapTile m in MapManager.I.map)
        {
            if (m.level == CameraManager.I.currCameraLevel)
            {
                TileBase baseTile = null;
                TileBase upwallTile = null;
                TileBase downwallTile = null;

                MapTile tileLeft = MapManager.I.GetTileLeft(m);
                MapTile tileRight = MapManager.I.GetTileRight(m);
                MapTile tileUp = MapManager.I.GetTileUp(m);
                MapTile tileDown = MapManager.I.GetTileDown(m);
                MapTile tileUpLeft = MapManager.I.GetTileUpLeft(m);
                MapTile tileUpRight = MapManager.I.GetTileUpRight(m);
                MapTile tileDownLeft = MapManager.I.GetTileDownLeft(m);
                MapTile tileDownRight = MapManager.I.GetTileDownRight(m);
                MapTile tileBelow = MapManager.I.GetTileBelow(m);
                MapTile tileAbove = MapManager.I.GetTileAbove(m);

                //bool isLeft = (tileLeft == null || (tileLeft != null && tileLeft.isWall));
                //bool isRight = (tileRight == null || (tileRight != null && tileRight.isWall));
                //bool isDownLeft = (tileDownLeft == null || (tileDownLeft != null && tileDownLeft.isWall));
                //bool isDownRight = (tileDownRight == null || (tileDownRight != null && tileDownRight.isWall));

                if (m.isWall)
                {
                    if (tileDown != null && !tileDown.isWall)
                    {
                        //if (isLeft && isRight) upwallTile = TileManager.I.upwall;
                        //if (isLeft && !isRight) upwallTile = TileManager.I.upwallRight;
                        //if (!isLeft && isRight) upwallTile = TileManager.I.upwallLeft;
                        //if (!isLeft && !isRight) upwallTile = TileManager.I.upwallLeftRight;

                        upwallTile = TileManager.I.upwall;
                    }
                    else
                    {
                        //if (isLeft && isRight) baseTile = TileManager.I.rock;
                        //if (isLeft && !isRight) baseTile = TileManager.I.rockRight;
                        //if (!isLeft && isRight) baseTile = TileManager.I.rockLeft;
                        //if (!isLeft && !isRight) baseTile = TileManager.I.rockLeftRight;

                        baseTile = TileManager.I.rock;
                    }
                }
                else
                {
                    if (m.isGround)
                    {
                        baseTile = TileManager.I.ground;
                    }
                    else
                    {
                        if (tileUp != null && (tileUp.isWall || tileUp.isGround))
                        {
                            baseTile = TileManager.I.holeUp;
                        }
                        else
                        {
                            baseTile = TileManager.I.hole;
                        }

                        //bool isHoleUp = (tileUp != null && (tileUp.isWall || tileUp.isGround));
                        //bool isHoleDown = (tileDown != null && (tileDown.isWall || tileDown.isGround));
                        //bool isHoleLeft = (tileLeft != null && (tileLeft.isWall || tileLeft.isGround));
                        //bool isHoleRight = (tileRight != null && (tileRight.isWall || tileRight.isGround));

                        //if (isHoleUp && isHoleDown && isHoleLeft && isHoleRight) baseTile = TileManager.I.holeUpDownLeftRight;
                        //if (isHoleUp && isHoleDown && isHoleLeft && !isHoleRight) baseTile = TileManager.I.holeUpDownLeft;
                        //if (isHoleUp && isHoleDown && !isHoleLeft && isHoleRight) baseTile = TileManager.I.holeUpDownRight;
                        //if (isHoleUp && isHoleDown && !isHoleLeft && !isHoleRight) baseTile = TileManager.I.holeUpDown;

                        //if (isHoleUp && !isHoleDown && isHoleLeft && isHoleRight) baseTile = TileManager.I.holeUpLeftRight;
                        //if (isHoleUp && !isHoleDown && isHoleLeft && !isHoleRight) baseTile = TileManager.I.holeUpLeft;
                        //if (isHoleUp && !isHoleDown && !isHoleLeft && isHoleRight) baseTile = TileManager.I.holeUpRight;
                        //if (isHoleUp && !isHoleDown && !isHoleLeft && !isHoleRight) baseTile = TileManager.I.holeUp;

                        //if (!isHoleUp && isHoleDown && isHoleLeft && isHoleRight) baseTile = TileManager.I.holeDownLeftRight;
                        //if (!isHoleUp && isHoleDown && isHoleLeft && !isHoleRight) baseTile = TileManager.I.holeDownLeft;
                        //if (!isHoleUp && isHoleDown && !isHoleLeft && isHoleRight) baseTile = TileManager.I.holeDownRight;
                        //if (!isHoleUp && isHoleDown && !isHoleLeft && !isHoleRight) baseTile = TileManager.I.holeDown;

                        //if (!isHoleUp && !isHoleDown && isHoleLeft && isHoleRight) baseTile = TileManager.I.holeLeftRight;
                        //if (!isHoleUp && !isHoleDown && isHoleLeft && !isHoleRight) baseTile = TileManager.I.holeLeft;
                        //if (!isHoleUp && !isHoleDown && !isHoleLeft && isHoleRight) baseTile = TileManager.I.holeRight;
                        //if (!isHoleUp && !isHoleDown && !isHoleLeft && !isHoleRight) baseTile = TileManager.I.hole;
                    }

                    if (tileDown != null && tileDown.isWall)
                    {
                        //if (isDownLeft && isDownRight) downwallTile = TileManager.I.downwall;
                        //if (isDownLeft && !isDownRight) downwallTile = TileManager.I.downwallRight;
                        //if (!isDownLeft && isDownRight) downwallTile = TileManager.I.downwallLeft;
                        //if (!isDownLeft && !isDownRight) downwallTile = TileManager.I.downwallLeftRight;

                        downwallTile = TileManager.I.downwall;
                    }
                }

                SetTile(baseTilemap, m.position, baseTile);
                SetTile(upwallTilemap, m.position, upwallTile);
                SetTile(downwallTilemap, m.position, downwallTile);
                SetTile(selectTilemap, m.position, null);
                SetTile(entityTilemap, m.position, null);
            }
        }

        foreach (Entity e in GameManager.I.entities)
        {
            TileBase entityTile = null;

            if (e.level == CameraManager.I.currCameraLevel)
            {

                switch (e.type)
                {
                    case EntityType.player:

                        entityTile = TileManager.I.player;
                        GameManager.I.playerEntity = e;

                        break;
                    case EntityType.elevator:

                        EntityElevator elevator = (EntityElevator)e;
                        if (elevator.isUp) entityTile = TileManager.I.elevUp;
                        else entityTile = TileManager.I.elevDown;

                        break;
                }

                SetTile(entityTilemap, e.position, entityTile);
            }
        }
    }

    public void ChangeSelection()
    {
        mousePos = CameraManager.I.camera.ScreenToWorldPoint(InputManager.I.mousePosition);
        selectPos = baseTilemap.WorldToCell(mousePos);

        isSelWithinBounds = (selectPos.y >= 0 && selectPos.y <= MapManager.I.mapHeight-1 &&
            selectPos.x >= 0 && selectPos.x <= MapManager.I.mapWidth-1);

        selectTile = MapManager.I.GetTile(CameraManager.I.currCameraLevel, selectPos);

        //ClearTilemap(selectTilemap);

        //Debug.Log(MapManager.I.GetTile(CameraManager.I.currCameraLevel, newSelectPos.x, newSelectPos.y).isGround);
        //Debug.Log(newSelectPos);

        

        if (isSelWithinBounds)
        {
            if(GameManager.I.hasPlayerMoved)
            {
                if(PathfindingManager.I.endTile.isEntity)
                {
                    if(PathfindingManager.I.endTile.entity.type == EntityType.elevator)
                    {
                        EntityElevator elevator = (EntityElevator)PathfindingManager.I.endTile.entity;
                        GameManager.I.playerEntity.position = elevator.destPos;
                        GameManager.I.playerEntity.level = elevator.destLevel;
                        CameraManager.I.currCameraLevel = elevator.destLevel;
                        CameraManager.I.camera.transform.position =
                            new Vector3Int(elevator.destPos.x, elevator.destPos.y, -10);
                        GameManager.I.changeLevel(elevator.destLevel);
                    }
                }

                GameManager.I.hasPlayerMoved = false;
            }

            if(!GameManager.I.isPlayerMoving)
            {
                if (CameraManager.I.currCameraLevel == GameManager.I.playerEntity.level)
                {
                    PathfindingManager.I.FindPath(GameManager.I.playerEntity.position, selectPos);
                }
            }

            if (selectTile.isEntity)
            {
                //SetTile(selectTilemap, newSelectPos, TileManager.I.blueSelectTile);

                GameManager.I.uiEntity.GetComponent<Text>().text = selectTile.entity.type.ToString();

                if (selectTile.entity.type == EntityType.elevator)
                {
                    GameManager.I.uiAction.GetComponent<Text>().text = "use elevator";

                    if (PathfindingManager.I.isTraversable)
                    {
                        GameManager.I.uiAction.GetComponent<Text>().color = Color.green;

                        if (!GameManager.I.isPlayerMoving)
                        {
                            if (InputManager.I.isLeftMouseButton)
                            {
                                GameManager.I.hasPlayerMoved = false;
                                StartCoroutine(MovePlayer());
                            }
                        }
                    }
                    else
                    {
                        GameManager.I.uiAction.GetComponent<Text>().color = Color.red;
                    }
                }
                else
                {
                    GameManager.I.uiAction.GetComponent<Text>().text = "";
                }
            }
            else
            {
                //SetTile(selectTilemap, newSelectPos, TileManager.I.yellowSelectTile);

                GameManager.I.uiEntity.GetComponent<Text>().text = "";
                GameManager.I.uiAction.GetComponent<Text>().text = "move player";

                if (PathfindingManager.I.isTraversable && selectTile.isGround)
                {
                    GameManager.I.uiAction.GetComponent<Text>().color = Color.green;

                    if (!GameManager.I.isPlayerMoving)
                    {
                        if (InputManager.I.isLeftMouseButton)
                        {
                            GameManager.I.hasPlayerMoved = false;
                            StartCoroutine(MovePlayer());
                        }
                    }
                }
                else
                {
                    GameManager.I.uiAction.GetComponent<Text>().color = Color.red;
                }
            }
        }
    }

    public IEnumerator MovePlayer()
    {
        GameManager.I.isPlayerMoving = true;

        SetTile(entityTilemap, GameManager.I.playerEntity.position, null);

        foreach (MapTile m in PathfindingManager.I.path)
        {
            SetTile(entityTilemap, m.position, TileManager.I.player);
            GameManager.I.playerEntity.position = m.position;
            yield return new WaitForSeconds(0.1f);
            SetTile(entityTilemap, m.position, null);
        }

        SetTile(entityTilemap, PathfindingManager.I.endTile.position, TileManager.I.player);

        GameManager.I.isPlayerMoving = false;
        GameManager.I.hasPlayerMoved = true;
    }
}
