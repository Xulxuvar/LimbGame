using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActiveInventory : AbstractInventoryState
{
   
    //Method that runs when the highlighted grid section changes
    public override void highlightedChanged(InventoryRender render)
    {

    }

    //Method runs when the inventory is entered with the mouse, add a method in inventoryRender that creates an inventoryPart and returns it to simplify this section later
    public override void mouseEntered(InventoryRender render)
    {
        if(PartButton.draggingButton)
        {
            AbstractPart part = PartButton.draggingButton.part;
            Destroy(PartButton.draggingButton.gameObject);
            PartButton.draggingButton = null;

            bool[,] partSize = part.getPartGrid();
            Image empty = Instantiate(render.itemTemplate);
            InventoryPart emptyScript = empty.gameObject.GetComponent<InventoryPart>();
            empty.sprite = part.getSprite();
            empty.transform.SetParent(render.background.transform);
            emptyScript.setVisible();
            emptyScript.part = part;
            emptyScript.topLeftIndex = new int[]{ -1,-1};
            emptyScript.setPartSize(partSize.GetLength(0), partSize.GetLength(1));

            render.selected = emptyScript;
            render.savedIndex = new int[] { -1, -1 };
            render.savedRot = render.selected.rotation;
            render.savedOffsetIndex = new int[] { 0, 0 };
            render.state = new PlayerDragging();
            render.mouseHeld = true;
        }
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
            InventoryPart selected = render.inventoryGrid.getGridItem(debug[1],debug[0]);
            if (selected)
            {
                render.selected = selected;
                render.savedIndex = debug;
                render.savedRot = selected.rotation;
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

