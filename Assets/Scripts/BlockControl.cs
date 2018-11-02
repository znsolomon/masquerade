using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour {
    public PlayerControl playerScript;
    bool bumped = false;
    // Use this for initialization
    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Push(Vector3 direction)
    {
        foreach (GameObject block in WorldManager.blocksNwalls)
        {
            if ((transform.position + direction) == block.transform.position)
            {
                playerScript.bump();
                bumped = true;
                break;
            }
        }
        if ((transform.position + direction) == WorldManager.exit.transform.position)
        {
            playerScript.bump();
            bumped = true;
        }
        if (bumped == false)
        {
            bool onFloor = false;
            transform.Translate(direction);
            foreach (GameObject floorTile in WorldManager.floor)
            {
                if (floorTile.transform.position == transform.position)
                {
                    onFloor = true;
                    break;
                }
            }
            if (onFloor == false)
            {
                gameObject.tag = "Floor";
                WorldManager.UpdateTags();
            }
        }
        bumped = false;
    }
}
