using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryRender : MonoBehaviour
{

    public InventoryStruct inventoryGrid;
    private float marginX = 16;
    private float marginY = 16;
    private float gridSize = 60;
    private int[] gridDims = { 0, 0 };

    [SerializeField] public Image background;
    [SerializeField] public Image itemTemplate;

    public AbstractInventoryState state;

    public bool mouseHeld = false;

    //Variables used for state stuffz
    public int[] savedIndex;
    public int[] mouseIndex = { -1, -1 };
    public InventoryPart selected = null;
    public int[] savedOffsetIndex;
    public int savedRot;

    // Start is called before the first frame update
    void Start()
    {
        state = new PlayerActiveInventory();
        AbstractHeart testHeart = new TestHeart();
        inventoryGrid = new InventoryStruct();
        inventoryGrid.setHeart(testHeart);
        gridDims = inventoryGrid.getGridSize();
        resizeBackground(gridDims);
        GenerateEmpties();
        addHeartToGrid(testHeart);
        addPartToGrid(new TestLimb3(), new int[] { 2, 0 });
        printInventoryGrid();

    }

    private void addHeartToGrid(AbstractHeart heart)
    {
        int[] offset = inventoryGrid.getHeartOffset(heart);
            addPartToGrid(heart, offset);
    }


    //Adds the part to the grid, currently uses cringe shorts, should read the part later
    private void addPartToGrid(AbstractPart part, int[] offset)
    {
       
                addPart(offset[0],offset[1],part);
      
    }

    public void checkMouseRelease()
    {
        if(!Input.GetMouseButton(0))
        {
            mouseHeld = false;
            mouseReleased();
        }
    }

    public void mousePressed()
    {
        state.mousePress(this);
        mouseHeld = true;
    }

    public void mouseEntered()
    {
        state.mouseEntered(this);
    }

    public void mouseReleased()
    {
        state.mouseRelease(this);
    }


    private void resizeBackground(int[] gridDims)
    {
        background.rectTransform.sizeDelta = new Vector2(2 * marginX + gridSize * gridDims[0], 2 * marginY + gridSize * gridDims[1]);
    }

    // Update is called once per frame, if there are performance issues, make the highlighted check only run when mouse entered
    void Update()
    {
        int[] newMouse = getGridIndex(Input.mousePosition.x, Input.mousePosition.y);
        if (newMouse[0] != mouseIndex[0] || newMouse[1] != mouseIndex[1])
        {
            mouseIndex = newMouse;
            state.highlightedChanged(this);
        }

        if (Input.GetKeyDown("space"))
        {
            state.spacebarPressed(this);
        }

        if (mouseHeld)
            checkMouseRelease();

    }

    public int[] getOriginArray(int[] mouseIndex, int[] offsetIndex, InventoryPart selected)
    {
        int[] savedOffsetIndex = new int[] { mouseIndex[0] - offsetIndex[0], mouseIndex[1] - offsetIndex[1] };
        int[] origin;
        int[] partLimits = new int[] { selected.part.getPartGrid().GetLength(0), selected.part.getPartGrid().GetLength(1) };
      //  Debug.Log("indexes" + mouseIndex[0] + " " + mouseIndex[1] + "\ngridDims " + savedOffsetIndex[0] + " " + savedOffsetIndex[1] + "\npartSize" + partLimits[0] + " " + partLimits[1] + "\n");
        switch (selected.rotation)
        {
            case 1:
                origin = new int[] {partLimits[0]-1-( mouseIndex[1] - savedOffsetIndex[1]), mouseIndex[0] - savedOffsetIndex[0] };
                break;
            case 2:
                origin = new int[] { partLimits[1] - 1 - (mouseIndex[0] - savedOffsetIndex[0]), partLimits[0] - 1 - (mouseIndex[1] - savedOffsetIndex[1]) };
                break;
            case 3:
                origin = new int[] { mouseIndex[1] - savedOffsetIndex[1] , partLimits[1] - 1 - (mouseIndex[0] - savedOffsetIndex[0]) };
                break;
            default:
                origin = new int[] { mouseIndex[0] - savedOffsetIndex[0], mouseIndex[1] - savedOffsetIndex[1] };
                break;
        }
     //   Debug.Log(origin[0]+" "+origin[1]+"\n");
        return origin;
    }

    //Prints the inventory grid stored in the inventory structure
    public void printInventoryGrid()
    {
        short[,] arr = inventoryGrid.getGrid();
        int rowCount = arr.GetLength(0);
        int colCount = arr.GetLength(1);
        string output = "";
        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
                output = output + arr[row,col]+" ";
            output = output + "\n";
        }
        Debug.Log(output);
    }

    private void GenerateEmpties()
    {
        short[,] grid = inventoryGrid.getGrid();

        for(int i = 0; i< grid.GetLength(0); i++)
            for(int j = 0; j< grid.GetLength(1); j++)
                if(grid[i,j] == -2)
                    addEmpty(i, j);

    }

    public float[] getCoordsFromIndex(int[] index,InventoryPart part)
    {
        bool[,] partSize = part.part.getPartGrid();
        float x;
        float y;
        //index = part.topLeftIndex;

       // Debug.Log("indexes" + index[0] + " " + index[1] + "\ngridDims " + gridDims[0] + " " + gridDims[1] + "\npartSize" + partSize.GetLength(0) + " " + partSize.GetLength(1) + "\n");

        switch(part.rotation)
        {
            case 1:
                x = marginX + index[0] * gridSize;
                y = marginY + (gridDims[1]  - index[1]) * gridSize;
                break;
            case 2:
                x = marginX + (index[0] + partSize.GetLength(1)) * gridSize;
                y = marginY + (gridDims[1] - index[1]) * gridSize;
                break;
            case 3:
                x = marginX + (index[0] + partSize.GetLength(0)) * gridSize;
                y = marginY + (gridDims[1] - partSize.GetLength(1) - index[1]) * gridSize;
                break;

            default:
                x = marginX + index[0] * gridSize;
                y = marginY + (gridDims[1] - partSize.GetLength(0) - index[1]) * gridSize;
                break;
    }

        return new float[] { x, y };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("u got a collision :)");
    }

    //This method will need revision for rotation later

    //This method might need a bit of clean up, with more happening in inventorystruct/inventoryPart
    private void addPart(int i, int j, AbstractPart part)
    {
    
        bool[,] partSize = part.getPartGrid();
        float x = marginX + i * gridSize;
        float y = marginY + (gridDims[1] - partSize.GetLength(0) - j) * gridSize;
        Image empty = Instantiate(itemTemplate);
        InventoryPart emptyScript = empty.gameObject.GetComponent<InventoryPart>();
        empty.sprite = part.getSprite();
        empty.transform.SetParent(background.transform);
        emptyScript.setVisible();
        emptyScript.part = part;
        emptyScript.topLeftIndex = new int[]{i,j};
        emptyScript.setPartSize(partSize.GetLength(0), partSize.GetLength(1));
        empty.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

        inventoryGrid.addPart(new int[] { i, j },emptyScript);
    }

    public void rotatePart(int rot, InventoryPart part, int[] rotationCenter)
    {
        part.rotateClockwise() ;
        int[] cornerOffset = getOriginArray(mouseIndex, savedOffsetIndex, selected);
        int[] index = new int[] { rotationCenter[0] - cornerOffset[0], rotationCenter[1] - cornerOffset[1] };
        float[] position = getCoordsFromIndex(index, part);
        part.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(position[0], position[1]);
    }


    private void addPart(int i, int j, InventoryPart iPart)
    {

        bool[,] partSize = iPart.part.getPartGrid();
        float x = marginX + i * gridSize;
        float y = marginY + (gridDims[1] - partSize.GetLength(0) - j) * gridSize;
        Image empty = iPart.GetComponent<Image>();
        empty.transform.SetParent(background.transform);
        iPart.setVisible();
        iPart.topLeftIndex = new int[] { i, j };
        iPart.setPartSize(partSize.GetLength(0), partSize.GetLength(1));
        empty.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

        inventoryGrid.inventoryList.Add(iPart);

        inventoryGrid.addPart(new int[] { i, j }, iPart);
    }


    public int[] getGridIndex(float x, float y)
    {
        float px = x - transform.position.x+background.rectTransform.sizeDelta.x/2;
        float py = y - transform.position.y + background.rectTransform.sizeDelta.y/2;

         if (px-marginX < 0 || py-marginY < 0 || px+marginX > background.rectTransform.rect.width || py+marginY > background.rectTransform.rect.height)
             return new int[] { -1, -1 };

        int[] dims = inventoryGrid.getGridSize();
        int ix = (int)((px - marginX) / gridSize);
        int iy = dims[1]-1-(int)((py - marginY) / gridSize);
        return new int[] { ix, iy };
    }


    private void addEmpty(int i, int j)
    {
        float x = marginX+i*gridSize;
        float y = marginY+(gridDims[1]-1-j)*gridSize;
        Image empty = Instantiate(itemTemplate);
        InventoryPart emptyScript = empty.gameObject.GetComponent<InventoryPart>();
        empty.transform.SetParent(background.transform);
        emptyScript.setVisible();
        empty.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
    }

}
