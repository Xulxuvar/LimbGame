using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartButton : MonoBehaviour
{
    public AbstractPart part;
    private bool dragging = false;
    private static float mouseInertia = 0.1f;
    public static PartButton draggingButton = null;

    [SerializeField]
    private GameObject port;
    [SerializeField]
    private GameObject nonPort;
    [SerializeField]
    private Text buttonText;

    private PartInventoryList listScript;

    Vector2 dragOffset = Vector2.zero;

    public void mousePressed()
    {
        draggingButton = this;
        dragging = true;
        dragOffset = new Vector2(transform.position.x,transform.position.y) - new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    public void mouseReleased()
    {
        draggingButton = null;
        listScript.addButton(part);
        Destroy(gameObject);
    }
    
    //This method is run whenever a part button is duplicated and given a part for reference, if Images are ever added to the part button, add them here :)
    public void setPart(AbstractPart part)
    {
        this.part = part;
        buttonText.text = part.partName;
    }

    private void debugPrintPart()
    {
        Debug.Log(part);
    }

    void Start()
    {
        listScript = nonPort.gameObject.GetComponent<PartInventoryList>();
    }
    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            transform.SetParent(nonPort.transform);
            transform.position = Vector2.Lerp(transform.position, new Vector2(Input.mousePosition.x, Input.mousePosition.y) + dragOffset, mouseInertia);
        }
    }

}
