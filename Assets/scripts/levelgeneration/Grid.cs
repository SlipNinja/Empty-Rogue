using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grid : MonoBehaviour
{
    public GameObject cellPrefab;
    public int width;
    public int height;
    public int maxVariations;

    private GameObject cellInstance;
    private List<Cell> cells;
    private int hillPoint;
    public bool end = false;

    void Awake()
    {
        cells = new List<Cell>();

        //GenerateLevel();
    }

    void Update()
    {
        if(end){
            return;
        }

        end = PlayerAtTheEnd();
    }

    private bool PlayerAtTheEnd()
    {
        /*if(Input.GetKeyDown(KeyCode.Space))// To change
        {
            return true;
        }*/

        return false;
    }

    public void DeleteLevel()
    {
        foreach (Cell c in cells.Reverse<Cell>())
        {
            if(c != null)
                Destroy(c.gameObject);
        }

        cells.Clear();
    }

    public void GenerateLevel()
    {

        GenerateBounds();
        ChooseHillPoint();

        for(int i = 0; i < maxVariations; i++)
        {
            SetAllNeighbours();
            GenerateVariations(i);
        }
        

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

    public List<Cell> GetPossibleSpawns()
    {
        List<Cell> spawns = new List<Cell>();

        for (int w=0; w <= width; w++)
        {
            for (int h=0; h <= height; h++)
            {
                Cell tmpCell = GetCell(w, h);
                if(!tmpCell)
                {
                    Cell spawnCell = GetCell(w, h-1);
                    if(spawnCell)
                    {
                        spawns.Add(spawnCell);
                    }
                }
            }
        }

        return spawns;
    }

    public Cell GetPlayerSpawnCell()
    {
        for (int w=0; w <= width; w++)
        {
            for (int h=0; h <= height; h++)
            {
                Cell tmpCell = GetCell(w, h);
                if(!tmpCell)
                {
                    Cell underCell = GetCell(w, h-1);
                    Cell rightCell = GetCell(w + 1, h);
                    Cell leftCell = GetCell(w - 1, h);
                    Cell upCell = GetCell(w, h + 1);
                    if(underCell && !rightCell && !upCell && !leftCell)
                    {
                        return underCell;
                    }
                }
            }
        }

        return null;
    }

    public int CellCount()
    {
        return cells.Count;
    }

    private void GenerateVariations(int variationNumber)
    {
        List<Cell> tmpCells = new List<Cell>(cells);
        foreach (Cell c in tmpCells)
        {
            int distWithHill = Mathf.Abs(hillPoint - c.GetCoordinates().x);
            foreach (KeyValuePair<string, Cell> neighbour in c.neighbours)
            {
                if(neighbour.Value == null)
                {
                    int chances;

                    if(distWithHill < 5)
                    {
                        chances = 10;
                    }
                    
                    else if(distWithHill < 10)
                    {
                        chances = 9;
                    }
                    
                    else if(distWithHill < 15)
                    {
                        chances = 8;
                    }

                    else
                    {
                        chances = 5;
                    }

                    chances -= (int)variationNumber/2;
                    Mathf.Clamp(chances, 0, 10);

                    int tmp = Random.Range(0, maxVariations);
                    if(tmp<chances)
                    {
                        Vector2Int newCoords = GetCellCoords(neighbour.Key, c.GetCoordinates());
                        CreateCell(newCoords);
                    }
                }
            }
        }
    }

    private void ChooseHillPoint()
    {
        hillPoint = Random.Range(1, width);
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
            //Debug.Log("Already exist : " + coords.x + " : " + coords.y);
            return;
        }

        cellInstance = Instantiate(cellPrefab, new Vector3(coords.x, coords.y, 0), Quaternion.identity);
        Cell newCell = cellInstance.GetComponent<Cell>();
        newCell.Init();
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
        string[] directions = new string[4] {"north", "south", "east", "west"};

        Vector2Int tmpCoords = Vector2Int.zero;

        // foreach (KeyValuePair<string, Cell> kvp in cell.neighbours)
        // {
            
        // }

        foreach (string direction in directions)
        {

            tmpCoords = GetCellCoords(direction, cell.GetCoordinates());
            if(!validCoordinates(tmpCoords)){continue;}

            Cell neighbCell = GetCell((int)tmpCoords.x, (int)tmpCoords.y);

            if(cell.neighbours.ContainsKey(direction))
            {
                cell.neighbours[direction] = neighbCell;
            }

            else
            {
                cell.neighbours.Add(direction, neighbCell);
            }
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
