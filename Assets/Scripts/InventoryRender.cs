using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryRender : MonoBehaviour
{

    private InventoryStruct inventoryGrid;
    private float marginX = 16;
    private float marginY = 16;
    private float gridSize = 60;
    private int[] gridDims = { 0, 0 };
    private List<InventoryPart> renderedItems = new List<InventoryPart>();

    [SerializeField] private Image background;
    [SerializeField] private Image itemTemplate;

  

    // Start is called before the first frame update
    void Start()
    {
        AbstractHeart testHeart = new TestHeart();
        inventoryGrid = new InventoryStruct();
        inventoryGrid.setHeart(testHeart);
        gridDims = inventoryGrid.getGridSize();
        resizeBackground(gridDims);
        GenerateEmpties();
        addHeartToGrid(testHeart);
        //printInventoryGrid();

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


    private void resizeBackground(int[] gridDims)
    {
        background.rectTransform.sizeDelta = new Vector2(2 * marginX + gridSize * gridDims[0], 2 * marginY + gridSize * gridDims[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Prints the inventory grid stored in the inventory structure
    public void printInventoryGrid()
    {
        short[,] arr = inventoryGrid.getGrid();
        int rowCount = arr.GetLength(0);
        int colCount = arr.GetLength(1);
        string output = "";
        for (int col = 0; col < colCount; col++)
        {
            for (int row = 0; row < rowCount; row++)
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

    //This method will need revision for rotation later
    private void addPart(int i, int j, AbstractPart part)
    {
    
        bool[,] partSize = part.getPartGrid();
        float x = marginX + i * gridSize;
        float y = marginY + (gridDims[1] - partSize.GetLength(1) - j) * gridSize;
        Image empty = Instantiate(itemTemplate);
        InventoryPart emptyScript = empty.gameObject.GetComponent<InventoryPart>();
        empty.sprite = part.getSprite();
        empty.transform.SetParent(background.transform);
        emptyScript.setVisible();
        emptyScript.setPartSize(partSize.GetLength(0), partSize.GetLength(1));
        empty.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

        inventoryGrid.addPart(new int[] { i, j },part);
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
