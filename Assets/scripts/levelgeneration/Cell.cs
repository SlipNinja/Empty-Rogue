using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cell : MonoBehaviour
{

    private Vector2Int coordinates;

    public Dictionary<string, Cell> neighbours;

    void Start()
    {
        neighbours = new Dictionary<string, Cell>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCoordinates(int x, int y)
    {
        coordinates = new Vector2Int(x, y);
    }

    public Vector2Int GetCoordinates()
    {
        return coordinates;
    }
}
