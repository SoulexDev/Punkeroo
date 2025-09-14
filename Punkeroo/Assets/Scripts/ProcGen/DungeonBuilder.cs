using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonBuilder : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private DungeonRoom startRoom;
    [SerializeField] private DungeonRoom endRoom;

    private bool[,] tilePlaced = new bool[1000, 2000];
    private void Awake()
    {
        StartCoroutine(BuildMap());
    }
    IEnumerator BuildMap()
    {
        Tile[] tiles;
        BoundsInt roomBounds;

        int tlXPos = 0;
        int tlYPos = 0;

        DungeonRoom room = startRoom;

        for (int i = 0; i < 10; i++)
        {
            tiles = new Tile[room.roomBounds.x * room.roomBounds.y];
            System.Buffer.BlockCopy(room.tiles, 0, tiles, 0, room.roomBounds.x);

            roomBounds = new BoundsInt(tlXPos, tlYPos, 0, room.roomBounds.x, room.roomBounds.y, 0);

            tilemap.SetTilesBlock(roomBounds, tiles);
            yield return new WaitForSeconds(0.01f);
        }
    }
    public bool TileFilled(int tlX, int tlY, int checkSizeX, int checkSizeY)
    {
        Task<bool> checkTask = Task<bool>.Factory.StartNew(() =>
        {
            for (int x = 0; x < checkSizeX; x++)
            {
                for (int y = 0; y < checkSizeY; y++)
                {
                    if (tilePlaced[tlX + x, tlY + y])
                        return true;
                }
            }
            return false;
        });

        return checkTask.Result;
    }
}