using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPart : MonoBehaviour
{
    public AbstractPart part = null;
    private Image image;
    public int[] topLeftIndex;

    //Rotation 0 is basic, 1 is 90deg clockwise ... ... 3 is 270deg clockwise
    public int rotation = 0;

    public InventoryPart()
    {
        
    }

    public void rotateClockwise()
    {
        rotation += 1;
        if (rotation > 3)
            rotation = 0;
        GetComponent<RectTransform>().Rotate(new Vector3(0, 0, -90));
    }

    public void rotateCClockwise()
    {
        rotation -= 1;
        if (rotation < 0)
            rotation = 3;
        GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 90));
    }
    public void setRotation(int rot)
    {
        rotation = rot;
        GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -rot * 90);
    }

    public bool[,] getPartGrid()
    {
        bool[,] baseline = part.getPartGrid();
        bool[,] calc;
        switch (rotation)
        {
           
            case 1:
                calc = new bool[baseline.GetLength(1), baseline.GetLength(0)];
                for (int j = 0; j < baseline.GetLength(0); j++)
                    for (int i = 0; i < baseline.GetLength(1); i++)
                        calc[i, baseline.GetLength(0) - 1 - j] = baseline[j, i];
                break;

            case 2:
                calc = new bool[baseline.GetLength(0), baseline.GetLength(1)];
                for (int j = 0; j < baseline.GetLength(0); j++)
                    for (int i = 0; i < baseline.GetLength(1); i++)
                        calc[baseline.GetLength(0) - 1 - j, baseline.GetLength(1) - 1 - i] = baseline[j, i];
                break;
            case 3:
                calc = new bool[baseline.GetLength(1), baseline.GetLength(0)];
                for (int j = 0; j < baseline.GetLength(0); j++)
                    for (int i = 0; i < baseline.GetLength(1); i++)
                        calc[baseline.GetLength(1)-1-i, j] = baseline[j, i];
                break;
            default:
                calc = baseline;
                break;
        }
        return calc;
    }

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void toggleTint(bool bol)
    { 
        if (bol)
           image.color = new Color(1, 0, 1, 1);
        else
            image.color= new Color(1, 1, 1, 1);
    }

    //Something is borked, this is now flipped, hopefully it works
    public void setPartSize(int xScale, int yScale)
    {
        Vector2 initialSize = gameObject.GetComponent<RectTransform>().sizeDelta;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(initialSize.x * yScale, initialSize.y * xScale);
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
