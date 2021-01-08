using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartInventoryList : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private GameObject content;

    public static PartInventoryList defaultInventory;
    // Start is called before the first frame update
    void Start()
    {
        addButton(new TestLimb());
        addButton(new TestLimb2());
        addButton(new TestLimb3());
        addButton(new TestHeart());
        defaultInventory = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void buttonEntered(Button button)
    {

    }

    public void addButton(AbstractPart part)
    {
        GameObject newButton = Instantiate(buttonTemplate);
        newButton.GetComponent<PartButton>().setPart(part);
        newButton.transform.SetParent(content.transform);
        newButton.gameObject.SetActive(true);

    }

}
