using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{

    public GameObject playerPrefab;
    public bool spawnOnCurCamera = false;
    public Camera curCamera;

    private void Awake()
    {
        if (playerPrefab == null)
        {
            Debug.LogWarning("No Player Object to Spawn");
        }
        curCamera = Camera.current;
        //Debug.Log(curCamera.transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(playerPrefab, this.transform.position, this.transform.rotation);
        if (spawnOnCurCamera)
        {

        }
        else if (!spawnOnCurCamera)
        {
            
        }
    }
}
