using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStruct
{
    private short[,] inventoryGrid = new short[,] { { 1 } };
    private List<short> inventoryList = new List<short>();

    // this is the constructor for inventory grid, idk what should be passed into it so far, probably a heart item
    public InventoryStruct()
    {

    }

    //Pass in a heart for this method to create an empty inventory
    public void setHeart()
    {
        bool[,] gridShape = parseHeartDimensions();

        setInitialShape(gridShape);
    }

    //This method will add a heart to the grid
    public int[] getHeartOffset()
    {
        int[] gridSize = { inventoryGrid.GetLength(0), inventoryGrid.GetLength(1) };

        //Replace this with fetching the dimensions of the heart itself
        bool[,] tempHeartDims = parsePartShape();

        int[] heartOffset =
        {
            (gridSize[0]-tempHeartDims.GetLength(0))/2,
            (gridSize[1]-tempHeartDims.GetLength(1))/2
        };

        return heartOffset;
    }
    
    //Will need an item reference to run this method later
    public void addPart(int[] offsets, bool[,] bools)
    {
        //Add a new item here
        //inventoryList.Add();
        //Temporary Appending to the list
        inventoryList.Add(0);
        int partIndex = inventoryList.Count - 1;
        int boundX = bools.GetLength(0);
        int boundY = bools.GetLength(1);
        for (int i = 0; i<boundX; i++)
        {
            for(int j = 0; j<boundY; j++)
            {
                if (bools[i,j])
                    inventoryGrid[i + offsets[0],j + offsets[1]] = (short)partIndex;
            }
        }
    }

    //This method will look at the heart provided grid
    private bool[,] parseHeartDimensions()
    {
        bool[,] tempReturn = 
            { {false, false, true, true, true, false,false },
             { false,true, true,true, true, true,false },
             { true,true, true,true, true, true,true },
             { true,true, true,true, true, true,true },
            { true,true, true,true, true, true,true },
            { false,true, true,true, true, true,false },
            { false,false, true, true, true, false,false } };
        return tempReturn;

    }

    //This method will look at a parts shape, and return a boolean array
    public bool[,] parsePartShape()
    {
        bool[,] tempReturn =
           new bool[,] { { false, true, false }, { true, true, true }, { false, true, false } };
        return tempReturn;
    }

    //This method will be used to return the item information of a given grid tile
    public short getGridItem(int i, int j)
    {
        int index = inventoryGrid[i, j];
        if(index>=0)
        {
            return inventoryList[index];
        }
        else
        {
            return -1;
        }
    }

    public short[,] getGrid()
    {
        return inventoryGrid;
    }

    //resets the dimensions of the heart
    private void setGridDimensions(int width, int height)
    {
        inventoryGrid = new short[width,height];
    }

    private void setInitialShape(bool[,] heartShape)
    {
        setGridDimensions(heartShape.GetLength(0), heartShape.GetLength(1));

        for(int i = 0; i<heartShape.GetLength(0); i++)
        {
            for (int j = 0; j < heartShape.GetLength(1); j++)
            {
                if(heartShape[i,j])
                {
                    inventoryGrid[i, j] = -2;
                }
                else
                {
                    inventoryGrid[i, j] = -1;
                }
            }
        }
    }

    public int[] getGridSize()
    {
        return new int[] {inventoryGrid.GetLength(0),inventoryGrid.GetLength(1) };
    }


}
