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
    // Start is called before the first frame update
    void Start()
    {
        addButton(new TestLimb());
        addButton(new TestLimb());
        addButton(new TestLimb());
        addButton(new TestHeart());
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
