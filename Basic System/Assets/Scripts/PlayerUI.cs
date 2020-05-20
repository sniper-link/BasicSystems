using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject playerUICanvas;
    public GameObject interactionPanel;
    public GameObject pickupScroll;
    public float pickupDisappearTime;
    // need the panel to spawn the icon and amount at

    private void Awake()
    {
        if (!playerUICanvas)
        {
            Debug.Log("Need Player Canvas");
        }
        ShowInteractButton(false);
    }

    public void PickupUpdate(Sprite itemIcon, int itemAmount)
    {
        // spawn the icon and the amount under the panel
        // create using new GameObject will already instantiate the itemscroll
        GameObject newItemScroll = new GameObject("Item Scroll", typeof(RectTransform), typeof(Image));
        newItemScroll.GetComponent<Image>().sprite = itemIcon;
        //Instantiate(newItemScroll);
        Debug.Log("spawn scroll item");
        StartCoroutine(TimeToDisappear(pickupDisappearTime, newItemScroll));
    }

    IEnumerator TimeToDisappear(float waitTime, GameObject itemToDestroy)
    {
        Debug.Log("scrol start");
        yield return new WaitForSeconds(waitTime);
        // Destroy instanite object after wait time
        Debug.Log("scroll end");
        Destroy(itemToDestroy);
    }

    public void ShowInteractButton(bool wayToPlay)
    {
        interactionPanel.SetActive(wayToPlay);
        /*if (wayToPlay)
        {
            
        }
        else if (!wayToPlay)
        {

        }*/
    }
}
