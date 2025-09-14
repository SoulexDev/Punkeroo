using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Bone/Dungeon Room")]
public class DungeonRoom : SerializedScriptableObject
{
    public DungeonRoomHorizontalState horizontalState;
    public DungeonRoomVerticalState verticalState;

    public Dictionary<Direction, DungeonRoom> connectableRooms;
    public Vector2Int roomBounds;

    [TableMatrix(SquareCells = true)]
    public Tile[,] tiles;
}