using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDragging : AbstractInventoryState
{

    //Method that runs when the highlighted grid section changes
    public override void highlightedChanged(InventoryRender render)
    {
        // int[] origin = new int[] { render.mouseIndex[0] - render.savedOffsetIndex[0], render.mouseIndex[1] - render.savedOffsetIndex[1] };
        Debug.Log(render.savedOffsetIndex[0] + " " + render.savedOffsetIndex[1]+"\n");
        int[] origin = render.getOriginArray(render.mouseIndex, render.savedOffsetIndex,render.selected);
        int[] positionIndex = new int[] { render.mouseIndex[0] - origin[0], render.mouseIndex[1] - origin[1] };

        bool validPosition = render.inventoryGrid.checkValid(render.selected, positionIndex);

        if (validPosition)
            render.selected.toggleTint(false);
        else
        {
            render.selected.toggleTint(true);
            render.printInventoryGrid();
            
        }

        

        float[] position = render.getCoordsFromIndex(positionIndex,render.selected);
        render.selected.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(position[0], position[1]);
    }

    //Method runs when the inventory is entered with the mouse
    public override void mouseEntered(InventoryRender render)
    {

    }
    //Method runs when the inventory is exited with the mouse
    public override void mouseExit(InventoryRender render)
    {
        PartInventoryList.defaultInventory.addButton(render.selected.part);
        render.inventoryGrid.removePart(render.selected);
        Destroy(render.selected.gameObject);
        render.selected = null;
        render.state = new PlayerActiveInventory();
        
    }

    //Method runs when the mouse is pressed in the inventory
    public override void mousePress(InventoryRender render)
    {
      
    }

    public override void spacebarPressed(InventoryRender render)
    {
        render.rotatePart(1, render.selected, render.mouseIndex);
    }


    //Method runs when the mouse is released in the inventory
    public override void mouseRelease(InventoryRender render)
    {
        int[] origin = render.getOriginArray(render.mouseIndex, render.savedOffsetIndex, render.selected);
        int[] positionIndex = new int[] { render.mouseIndex[0] - origin[0], render.mouseIndex[1] - origin[1] };
        bool validPosition = render.inventoryGrid.checkValid(render.selected, positionIndex);
        render.selected.toggleTint(false);

        if (validPosition)
        {
            render.inventoryGrid.removePart(render.selected);
            render.inventoryGrid.addPart(positionIndex, render.selected);
            render.printInventoryGrid();
            render.inventoryGrid.printInventoryList();
        }
        else
        {
            render.selected.setRotation(render.savedRot);
            float[] position = render.getCoordsFromIndex(new int[] { render.savedIndex[0] - render.savedOffsetIndex[0], render.savedIndex[1] - render.savedOffsetIndex[1] }, render.selected);
            render.selected.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(position[0], position[1]);
           
        }
        render.selected = null;
        render.state = new PlayerActiveInventory();
    }
}

