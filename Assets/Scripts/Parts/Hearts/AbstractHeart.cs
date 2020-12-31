using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractHeart : AbstractPart
{
    private bool[,] heartGrid;

    public AbstractHeart( int spriteIndex, bool[,] gridShape, bool[,] heartGrid) : base(spriteIndex, gridShape)
    {
        this.heartGrid = heartGrid;
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */

    public bool[,] getHeartGrid()
    {
        return heartGrid;
    }
}
