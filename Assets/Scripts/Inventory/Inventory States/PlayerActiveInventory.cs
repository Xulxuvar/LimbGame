using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveInventory : AbstractInventoryState
{
   
    //Method that runs when the highlighted grid section changes
    public override void highlightedChanged(InventoryRender render)
    {

    }

    //Method runs when the inventory is entered with the mouse
    public override void mouseEntered(InventoryRender render)
    {

    }
    //Method runs when the inventory is exited with the mouse
    public override void mouseExit(InventoryRender render)
    {

    }

    //Method runs when the mouse is pressed in the inventory
    public override void mousePress(InventoryRender render)
    {
        int[] debug = render.mouseIndex;

        if (debug != null)
        {
            InventoryPart selected = render.inventoryGrid.getGridItem(debug[0],debug[1]);
            if (selected)
            {
                render.selected = selected;
                render.selected.setRotation(1);
                render.savedIndex = debug;
                render.savedOffsetIndex = new int[] { render.savedIndex[0] - render.selected.topLeftIndex[0], render.savedIndex[1] - render.selected.topLeftIndex[1], };
                render.state = new PlayerDragging();
            }
        }
    }


    //Method runs when the mouse is released in the inventory
    public override void mouseRelease(InventoryRender render)
    {

    }
}

