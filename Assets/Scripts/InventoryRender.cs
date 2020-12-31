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

    //Part sprites is temporarilly stored here, until a better place is decided
    [SerializeField] private Sprite[] partSprites;

    // Start is called before the first frame update
    void Start()
    {
        inventoryGrid = new InventoryStruct();
        inventoryGrid.setHeart();

 

        gridDims = inventoryGrid.getGridSize();
        resizeBackground(gridDims);
        GenerateEmpties();
        addPartToGrid(0);
        printInventoryGrid();

    }

    //Adds the part to the grid, currently uses cringe shorts, should read the part later
    private void addPartToGrid(short type)
    {
        int[] offset;
        switch (type){
            case 0:
                offset = inventoryGrid.getHeartOffset();
                addPart(offset[0],offset[1],0);
                break;

        }
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
    private void addPart(int i, int j, int k)
    {

        bool[,] partSize = inventoryGrid.parsePartShape();
        float x = marginX + i * gridSize;
        float y = marginY + (gridDims[1] - partSize.GetLength(1) - j) * gridSize;
        Image empty = Instantiate(itemTemplate);
        InventoryPart emptyScript = empty.gameObject.GetComponent<InventoryPart>();
        empty.sprite = partSprites[k];
        empty.transform.SetParent(background.transform);
        emptyScript.setVisible();
        emptyScript.setPartSize(partSize.GetLength(0), partSize.GetLength(1));
        empty.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

        inventoryGrid.addPart(new int[] { i, j },inventoryGrid.parsePartShape());
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
