using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : Singleton<TileManager>
{
    public TileBase ground;
    public TileBase rock;
    public TileBase upwall;
    public TileBase downwall;
    public TileBase hole;
    public TileBase holeUp;

    public TileBase yellowSelectTile;
    public TileBase greenSelectTile;
    public TileBase blueSelectTile;
    public TileBase redSelectTile;

    public TileBase player;
    public TileBase elevUp;
    public TileBase elevDown;

    /*public TileBase room;
    public TileBase pit;
    public TileBase rockNoCorn;
    public TileBase rockLeftCorn;
    public TileBase rockRightCorn;
    public TileBase rockTwoCorn;
    public TileBase wallNoCorn;
    public TileBase wallLeftCorn;
    public TileBase wallRightCorn;
    public TileBase wallTwoCorn;
    public TileBase rockwallNoCorn;
    public TileBase rockwallLeftCorn;
    public TileBase rockwallRightCorn;
    public TileBase rockwallTwoCorn;
    public TileBase edgeNoCorn;
    public TileBase edgeLeftCorn;
    public TileBase edgeRightCorn;
    public TileBase edgeTwoCorn;*/

    void Start()
    {
        //
    }
}
