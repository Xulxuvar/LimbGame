using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPart : MonoBehaviour
{

    public InventoryPart()
    {

    }

    public void setPartSize(int xScale, int yScale)
    {
        Vector2 initialSize = gameObject.GetComponent<RectTransform>().sizeDelta;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(initialSize.x * xScale, initialSize.y * yScale);
    }

    public void resizeSlot(float gridsize)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gridsize, gridsize);
    }

    public void setVisible()
    {
        gameObject.SetActive(true);
    }

}
