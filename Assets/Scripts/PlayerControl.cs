using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {
    Vector3 direction;
    Vector3 reverse;
    public GameObject bullArrow;
    public GameObject hawkArrow;
    public GameObject goatArrow;
    public GameObject[] arrows;
    string[] masks = { "Bull", "Hawk" , "Goat"};
    string curMask;
    int whichMask = 0;
    public BlockControl blockScript;
    public Text bullCount;
    public Text hawkCount;
    public Text goatCount;
    public int BC;
    public int HC;
    public int GC;
    int layer = 0;
    bool hawkOn = false;
    // Use this for initialization
    void Start () {
        InvokeRepeating("move", 0.0f, 0.15f);
        InvokeRepeating("changeMask", 0.0f, 0.15f);
        updateMaskCount();
        arrows = new GameObject[3] {bullArrow, hawkArrow , goatArrow};
        initMask();
	}
    void updateMaskCount()
    {
        bullCount.text = " :" + BC;
        hawkCount.text = " :" + HC;
        goatCount.text = " :" + GC;
    }
    void initMask()
    {
        curMask = masks[whichMask];
        arrows[whichMask].SetActive(true);
    }
    void checkHawk()
    {
        if (curMask.Equals("Hawk") && HC > 0)
        {
            hawkOn = true;
        }
    }
	void move(){
        if (Input.GetKey("w"))
        {
            direction = Vector3.up;
            checkHawk();
        }
        else if (Input.GetKey("a"))
        {
            direction = Vector3.left;
            checkHawk();
        }
        else if (Input.GetKey("s"))
        {
            direction = Vector3.down;
            checkHawk();
        }
        else if (Input.GetKey("d"))
        {
            direction = Vector3.right;
            checkHawk();
        }
        else
        {
            direction = Vector3.zero;
        }
        if (hawkOn == true)
        {
            direction = direction * 2;
            hawkOn = false;
            HC -= 1;
        }
        reverse = direction * -1;
        checkWater(direction);
        
        if (layer == 1)
        {
            foreach (GameObject block in WorldManager.blocksNwalls)
            {
                if (block.transform.position == transform.position)
                {
                    layer = 1;
                    break;
                }
                else
                {
                    layer = 0;
                }
            }
        }
    }
    void checkWater(Vector3 movement)
    {
        bool onFloor = false;
        transform.Translate(movement);
        foreach (GameObject floorTile in WorldManager.floor)
        {
            if (floorTile.transform.position == transform.position)
            {
                onFloor = true;
                break;
            }
        }
        foreach (GameObject barrier in WorldManager.blocksNwalls)
        {
            if (barrier.transform.position == transform.position)
            {
                onFloor = true;
                break;
            }
        }
        if (onFloor == false)
        {
            transform.Translate(movement * -1);
            if (curMask.Equals("Hawk"))
            {
                HC += 1;
            }
        }
    }
    void changeMask()
    {
        if (Input.GetKey("left"))
        {
            arrows[whichMask].SetActive(false);
            whichMask -= 1;
            if (whichMask < 0)
            {
                whichMask = masks.Length - 1;
            }

            initMask();
        }
        if (Input.GetKey("right"))
        {
            arrows[whichMask].SetActive(false);
            whichMask += 1;
            if (whichMask > masks.Length - 1)
            {
                whichMask = 0;
            }
            initMask();
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (layer == 0 && GC > 0 && curMask.Equals("Goat") && (collider.CompareTag("Wall") || collider.CompareTag("Block")))
        {
            layer += 1;
            GC -= 1;
        }
        if (collider.CompareTag("Floor") || collider.CompareTag("Exit"))
        {
            return;
        }
        else if (layer == 0 && collider.CompareTag("Block") && curMask.Equals("Bull") && BC > 0)
        {
            blockScript = collider.GetComponent<BlockControl>();
            blockScript.Push(direction);
            BC -= 1;
        }
        else if (layer == 0)
        {
            transform.Translate(reverse);
            if (curMask.Equals("Hawk"))
            {
                HC += 1;
            }
        }
    }
    public void bump()
    {
        transform.Translate(reverse);
        BC += 1;
    }

    // Update is called once per frame
    void Update()
    {
        updateMaskCount();
    }
}
