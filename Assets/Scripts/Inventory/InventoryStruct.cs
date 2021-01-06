using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStruct
{
    private short[,] inventoryGrid = new short[,] { { 1 } };
    public List<InventoryPart> inventoryList = new List<InventoryPart>();

    // this is the constructor for inventory grid, idk what should be passed into it so far, probably a heart item
    public InventoryStruct()
    {

    }

    public bool checkValid(InventoryPart iPart, int[] origin)
    {
        int selectedIndex = -1;

        for(int i = 0; i<inventoryList.Count; i++)
            if(inventoryList[i] == iPart)
            {
                selectedIndex = i;
                break;
            }

        bool[,] partShape = iPart.part.getPartGrid();

        for(int i = 0; i<partShape.GetLength(1); i++)
            for(int j = 0; j<partShape.GetLength(0); j++)
            {
                 if (i + origin[0] > inventoryGrid.GetLength(1) || j + origin[1] > inventoryGrid.GetLength(0) || i + origin[0] < 0 || j + origin[1] < 0)
                   {
                     Debug.Log("OOB " + i + origin[0] + " " + j + origin[1] + "\n");
                         return false;
                   }
                if (partShape[j, i] && !(inventoryGrid[j + origin[1], i + origin[0]] == -2 || inventoryGrid[j + origin[1], i + origin[0]] == selectedIndex))
                {
                    Debug.Log(i + origin[0] + " " + j + origin[1] + "\n");
                    return false;
                }
            }
        return true;

    }

    //Pass in a heart for this method to create an empty inventory
    public void setHeart(AbstractHeart heart)
    {
        bool[,] gridShape = heart.getHeartGrid();

        setInitialShape(gridShape);
    }

    //This method will add a heart to the grid
    public int[] getHeartOffset(AbstractHeart heart)
    {
        int[] gridSize = { inventoryGrid.GetLength(0), inventoryGrid.GetLength(1) };

        bool[,] heartDims = heart.getPartGrid();

        int[] heartOffset =
        {
            (gridSize[0]-heartDims.GetLength(0))/2,
            (gridSize[1]-heartDims.GetLength(1))/2
        };

        return heartOffset;
    }
    
    //Will need an item reference to run this method later
    public void addPart(int[] offsets, AbstractPart part)
    {
        //Add a new item here
        //inventoryList.Add();
        //Temporary Appending to the list
       // inventoryList.Add(0);

        int partIndex = inventoryList.Count - 1;
        bool[,] bools = part.getPartGrid();
        int boundX = bools.GetLength(1);
        int boundY = bools.GetLength(0);
        for (int i = 0; i<boundX; i++)
        {
            for(int j = 0; j<boundY; j++)
            {
                if (bools[j,i])
                    inventoryGrid[i + offsets[0],j + offsets[1]] = (short)partIndex;
            }
        }
    }

    //This method will be used to return the item information of a given grid tile
    public InventoryPart getGridItem(int i, int j)
    {
        int index = inventoryGrid[i, j];
        if(index>=0)
        {
            return inventoryList[index];
        }
        else
        {
            return null;
        }
    }

    public int getGridListIndex(int i, int j)
    {
        int index = inventoryGrid[i, j];
        if (index >= 0)
        {
            return index;
        }
        else
        {
            return -1;
        }
    }

    public void removeReferences(int index)
    {
        for (int i = 0; i < inventoryGrid.GetLength(1); i++)
            for (int j = 0; j < inventoryGrid.GetLength(0); j++)
                if (inventoryGrid[i, j] == index)
                    inventoryGrid[i, j] = -1;
                    
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
