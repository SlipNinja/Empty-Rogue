using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grid : MonoBehaviour
{
    public GameObject cellPrefab;
    public int width;
    public int height;

    private GameObject cellInstance;
    private List<Cell> cells;

    void Start()
    {
        cells = new List<Cell>();

        GenerateLevel();
    }

    void Update()
    {
        
    }

    private void GenerateLevel()
    {

        GenerateBounds();

        SetAllNeighbours();

        //GenerateVariations();

        // for (int w=0; w <= width; w++)
        // {
        //     for (int h=0; h <= height; h++)
        //     {
        //         cellInstance = Instantiate(cellPrefab, new Vector3(w, h, 0), Quaternion.identity);
        //         cellInstance.transform.SetParent(transform);
        //         cells.Add(cellInstance);
        //     }
        // }
    }

    private void GenerateVariations()
    {
        foreach (Cell c in cells)
        {
            foreach (KeyValuePair<string, Cell> neighbour in c.neighbours)
            {
                if(neighbour.Value == null)
                {
                    Vector2Int newCoords = GetCellCoords(neighbour.Key, c.GetCoordinates());
                    CreateCell(newCoords);
                }
            }
        }
    }

    private void GenerateBounds()
    {
        for (int w=0; w <= width; w++)
        {
            CreateCell(w, 0);
            CreateCell(w, height);
        }

        for (int h=0; h <= height; h++)
        {
            CreateCell(0, h);
            CreateCell(width, h);
        }
    }

    private void CreateCell(int x, int y)
    {
        CreateCell(new Vector2Int(x, y));
    }
    private void CreateCell(Vector2Int coords)
    {
        if(GetCell(coords.x, coords.y))
        {
            Debug.Log("Already exist : " + coords.x + " : " + coords.y);
            return;
        }

        cellInstance = Instantiate(cellPrefab, new Vector3(coords.x, coords.y, 0), Quaternion.identity);
        Cell newCell = cellInstance.GetComponent<Cell>();
        newCell.SetCoordinates(coords.x, coords.y);
        newCell.transform.SetParent(transform);
        cells.Add(newCell);
    }

    private Cell GetCell(int x, int y)
    {
        Cell cell = null;
        Vector2Int coords = new Vector2Int(x, y);

        foreach(Cell c in cells)
        {
            if(c.GetCoordinates() == coords)
            {
                cell = c;
                break;
            }
        }

        return cell;
    }

    private Vector2Int GetCellCoords(string direction, Vector2Int baseCoords)
    {
        Vector2Int newCoords = new Vector2Int(-1, -1);

        switch (direction)
        {
            case "north":
                newCoords = new Vector2Int(baseCoords.x, baseCoords.y + 1);
                break;

            case "south":
                newCoords = new Vector2Int(baseCoords.x, baseCoords.y - 1);
                break;

            case "east": 
                newCoords = new Vector2Int(baseCoords.x + 1, baseCoords.y);
                break;

            case "west":
                newCoords = new Vector2Int(baseCoords.x - 1, baseCoords.y);
                break;
            
            default:
                Debug.Log("Wrong direction given");
                break;
        }

        return newCoords;
    }

    private void SetAllNeighbours()
    {
        foreach (Cell c in cells)
        {
            SetNeighbours(c);
        }
    }

    private void SetNeighbours(Cell cell)
    {
        if(cell.neighbours is null)
        {
            Debug.Log("IT'S NULL");
            cell.neighbours = new Dictionary<string, Cell>();
            //return;
        }
        string[] directions = new string[4] {"north", "south", "east", "west"};

        Vector2Int tmpCoords = Vector2Int.zero;

        foreach (string direction in directions)
        {

            tmpCoords = GetCellCoords(direction, cell.GetCoordinates());
            if(!validCoordinates(tmpCoords)){continue;}

            Cell neighbCell = GetCell((int)tmpCoords.x, (int)tmpCoords.y);

            cell.neighbours.Add(direction, neighbCell);
        }
    }

    private bool validCoordinates(Vector2Int coords)
    {
        if(coords.x > width || coords.x < 0 || coords.y > height || coords.y < 0)
        {
            return false;
        }

        return true;
    }
}
