using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabTile;
    public bool floor_2 = true;
    public int xSize = 8;
    public int zSize = 15;
    public GameObject[,] grid;
    private Hashtable pairs;

    // Start is called before the first frame update
    void Start()
    {
        CreateBoard(xSize, zSize);

        // tag all tiles that are walls as obstace, so that later fire cannot pass
        TagTile();
    }

    private void TagTile()
    {
        if (floor_2)
        {
            pairs = DefineWalls(1);
        }
        else {pairs = DefineWalls(2);}

        for (int x_ = 0; x_ < xSize; x_++)
        {
            for (int z_ = 0; z_ < zSize; z_++)
            {
                // check for existence
                if (pairs.ContainsKey(new Vector2(x_, z_)))
                {
                    // element exists, tag grid tile as obstacle
                    grid[x_,z_].tag = "Obstacle";
                }
            }
        }
    }

    private Vector3 GetSize(GameObject tile)
    {
        return new Vector3(tile.GetComponentInChildren<BoxCollider>().bounds.size.x, 0f ,tile.GetComponentInChildren<BoxCollider>().bounds.size.z);
    }

    private void CreateBoard(int width, int height)
    {
        grid = new GameObject[width, height];
        Vector3 startPos = this.transform.position;

        for(int x=0; x < xSize; x++)
        {
            for (int z=0; z < zSize; z++)
            {
                GameObject tile = Instantiate(prefabTile);
                tile.transform.parent = this.transform;

                // Vector3( ..., startPos.y, startPos.z + (GetSize(prefabTile).z ...
                tile.transform.position = new Vector3(startPos.x + (GetSize(prefabTile).x * x), startPos.y, startPos.z + (GetSize(prefabTile).z * z));
                grid[x,z] = tile;
            } 
        }
    }

    Hashtable DefineWalls(int level)
    {
        // initialize pairs
        Hashtable pairs = new Hashtable();

        if (level == 1)
        {
            for(int y=0; y < 7; y++)
            {
                pairs.Add(new Vector2(3, y), true);
            }
            for(int y=6; y < 12; y++)
            {
                pairs.Add(new Vector2(1, y), true);
                pairs.Add(new Vector2(8, y), true);
            }    
            for(int x=1; x < 7; x++)
            {
                pairs.Add(new Vector2(x, 14), true);
            }

            pairs.Add(new Vector2(6, 0), true);
            pairs.Add(new Vector2(8, 0), true);
            pairs.Add(new Vector2(5, 1), true);
            pairs.Add(new Vector2(6, 1), true);

            pairs.Add(new Vector2(4, 6), true);
            pairs.Add(new Vector2(5, 6), true);
            pairs.Add(new Vector2(6, 6), true);

            pairs.Add(new Vector2(2, 11), true);
            pairs.Add(new Vector2(3, 11), true);
            pairs.Add(new Vector2(4, 11), true);
            pairs.Add(new Vector2(6, 11), true);
            pairs.Add(new Vector2(7, 11), true);

            pairs.Add(new Vector2(4, 12), true);
            pairs.Add(new Vector2(6, 12), true);
        }

        if (level == 2)
        {
            for(int y=0; y < 5; y++)
            {
                pairs.Add(new Vector2(6, y), true);
            }
            for(int y=10; y < 14; y++)
            {
                pairs.Add(new Vector2(1, y), true);
            }
   
            for(int x=0; x < 3; x++)
            {
                pairs.Add(new Vector2(x, 3), true);
                pairs.Add(new Vector2(x, 6), true);
            }
            for(int x=0; x < 7; x++)
            {
                pairs.Add(new Vector2(x, 9), true);
            }
            for(int x=1; x < 7; x++)
            {
                pairs.Add(new Vector2(x, 14), true);
            }

            pairs.Add(new Vector2(3, 0), true);
            pairs.Add(new Vector2(3, 1), true);
            pairs.Add(new Vector2(3, 3), true);
            pairs.Add(new Vector2(3, 4), true);
            pairs.Add(new Vector2(3, 6), true);
            pairs.Add(new Vector2(3, 7), true);

            pairs.Add(new Vector2(6, 6), true);
            pairs.Add(new Vector2(6, 7), true);
            pairs.Add(new Vector2(6, 12), true);
            pairs.Add(new Vector2(6, 13), true);

            pairs.Add(new Vector2(4, 1), true);

            pairs.Add(new Vector2(8, 7), true);
        }

        return pairs;
    }    
}
