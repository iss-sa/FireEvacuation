using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadingFire : MonoBehaviour
{
    [SerializeField] private GameObject fireVisual;
    private BoardManager board;
    
    // Start is called before the first frame update
    void Start()
    {
        board = this.GetComponent<BoardManager>();
        int y = 0;
        int x;
        if (board.floor_2)
        {
            x = 0;
        }
        else 
        {
            x = 5;
        }
        
        StartCoroutine(FirstFloodFill(x,y));
    }

    public IEnumerator FloodFill(int x, int y)
    {
        yield return new WaitForSeconds(5);
        if (x >= 0 && x < board.xSize && y >= 0 && y < board.zSize)
        {
            if (board.grid[x,y].GetComponent<fireVisual>().active == false && board.grid[x,y].GetComponent<fireVisual>().used == false ) //if (board.grid[x,y].GetComponentInChildren<SpriteRenderer>().color == oldColor)
            {
                
                if (board.grid[x,y].tag != "Obstacle") // if obstacle or not decided in BoardManager.cs
                {
                    board.grid[x,y].GetComponent<fireVisual>().active = true;
                    board.grid[x,y].GetComponent<fireVisual>().used = true;
                
                    StartCoroutine(FloodFill(x + 1, y));
                    StartCoroutine(FloodFill(x - 1, y));
                    StartCoroutine(FloodFill(x, y + 1));
                    StartCoroutine(FloodFill(x, y - 1));

                    yield return new WaitForSeconds(60);
                    board.grid[x,y].GetComponent<fireVisual>().active = false;
                }
            }
            Debug.Log("exit");
            yield break;
        }
    }
    public IEnumerator FirstFloodFill(int x, int y)
    {
        yield return new WaitForSeconds(10);
        if (x >= 0 && x < board.xSize && y >= 0 && y < board.zSize)
        {
            if (board.grid[x,y].GetComponent<fireVisual>().active == false && board.grid[x,y].GetComponent<fireVisual>().used == false ) //if (board.grid[x,y].GetComponentInChildren<SpriteRenderer>().color == oldColor)
            {
                
                if (board.grid[x,y].tag != "Obstacle") // if obstacle or not decided in BoardManager.cs
                {
                    board.grid[x,y].GetComponent<fireVisual>().active = true;
                    board.grid[x,y].GetComponent<fireVisual>().used = true;
                
                    StartCoroutine(FloodFill(x + 1, y));
                    StartCoroutine(FloodFill(x - 1, y));
                    StartCoroutine(FloodFill(x, y + 1));
                    StartCoroutine(FloodFill(x, y - 1));

                    yield return new WaitForSeconds(60);
                    board.grid[x,y].GetComponent<fireVisual>().active = false;
                }
            }
            Debug.Log("exit");
            yield break;
        }
    }
}
