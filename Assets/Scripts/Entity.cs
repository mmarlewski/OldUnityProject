using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    none,
    player,
    elevator
}

public class Entity
{
    public EntityType type;
    public Vector3Int position;
    public int level;

    public Entity()
    {
        type = EntityType.none;
        position = Vector3Int.zero;
        level = 0;
    }

    public Entity(EntityType t, int x,int y, int l)
    {
        type = t;
        position = new Vector3Int(x, y, 0);
        level = l;
    }
}

public class EntityElevator : Entity
{
    public Vector3Int destPos;
    public int destLevel;
    public bool isUp;
    public EntityElevator(int x, int y, int l, bool b, int dx, int dy, int dl) : base(EntityType.elevator, x, y, l)
    {
        destPos = new Vector3Int(dx, dy, 0);
        destLevel = dl;
        isUp = b;
    }
}
