using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Parent class for the state machine used for inventories
public class AbstractInventoryState : ScriptableObject
{

    public enum States
    {
        playerActive,
        playerInactive,
        playerDragging,
        enemy
    }

    //Method that runs when the highlighted grid section changes
    public virtual void highlightedChanged(InventoryRender render)
    {

    }

    //Method runs when the inventory is entered with the mouse
    public virtual void mouseEntered(InventoryRender render)
    {

    }

    //Method runs when the inventory is exited with the mouse
    public virtual void mouseExit(InventoryRender render)
    {

    }

    //Method runs when the mouse is pressed in the inventory
    public virtual void mousePress(InventoryRender render)
    {

    }

    //Method runs when the mouse is released in the inventory
    public virtual void mouseRelease(InventoryRender render)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
