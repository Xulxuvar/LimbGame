using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDragging : AbstractInventoryState
{

    //Method that runs when the highlighted grid section changes
    public override void highlightedChanged(InventoryRender render)
    {
        // int[] origin = new int[] { render.mouseIndex[0] - render.savedOffsetIndex[0], render.mouseIndex[1] - render.savedOffsetIndex[1] };
        int[] origin = render.getOriginArray(render.mouseIndex, render.savedOffsetIndex,render.selected);

        bool validPosition = render.inventoryGrid.checkValid(render.selected, origin);
        
        if (validPosition)
            render.selected.toggleTint(false);
        else
            render.selected.toggleTint(true);

        float[] position = render.getCoordsFromIndex(origin,render.selected);
        render.selected.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(position[0], position[1]);
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
      
    }


    //Method runs when the mouse is released in the inventory
    public override void mouseRelease(InventoryRender render)
    {
        float[] position = render.getCoordsFromIndex(new int[] { render.savedIndex[0] - render.savedOffsetIndex[0], render.savedIndex[1] - render.savedOffsetIndex[1] }, render.selected);
        render.selected.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(position[0], position[1]);
        render.selected = null;
        render.state = new PlayerActiveInventory();
    }
}

