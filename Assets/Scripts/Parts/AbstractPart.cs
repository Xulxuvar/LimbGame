using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractPart : MonoBehaviour
{
    //Part sprites is temporarilly stored here, until a better place is decided
    private static string[] partSprites =
    {

        "Sprites/GridSprites/heartpiece",
        "Sprites/GridSprites/ArmLimbThing",
    };

    public string partName = "AbstractPart";
    public string partDescription = "You shouldn't be seeing this!\n ELDA TALUTA";
    private bool[,] gridShape;
    int spriteIndex;

    public AbstractPart(int spriteIndex, bool[,] gridShape)
    {
        this.spriteIndex = spriteIndex;
        this.gridShape = gridShape;
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */

    //Returns grid shape for the inventory grid
    public bool[,] getPartGrid()
    {
        return gridShape;
    }
    
    //Returns the associated integer for sprite for the part
    public int getSpriteIndex()
    {
        return spriteIndex;
    }

    public Sprite getSprite()
    {
        string source = partSprites[spriteIndex];
        Debug.Log(source+"\n");
        Texture2D texture = Resources.Load<Texture2D>(source);
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        return sp;
    }


}
