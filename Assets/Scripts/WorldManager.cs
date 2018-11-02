using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour {
    public static List<GameObject> blocksNwalls = new List<GameObject>();
    public static List<GameObject> floor = new List<GameObject>();
    public static GameObject player;
    public static GameObject exit;
    public string nextLVL;
    // Use this for initialization
    void Start () {
        UpdateTags();
        player = GameObject.FindGameObjectWithTag("Player");
        exit = GameObject.FindGameObjectWithTag("Exit");
    }
	public static void UpdateTags()
    {
        blocksNwalls.Clear();
        floor.Clear();
        blocksNwalls.AddRange(GameObject.FindGameObjectsWithTag("Block"));
        blocksNwalls.AddRange(GameObject.FindGameObjectsWithTag("Wall"));
        floor.AddRange(GameObject.FindGameObjectsWithTag("Floor"));
    }
    // Update is called once per frame
    void Update() {
        if (player.transform.position == exit.transform.position)
        {
            //Advance to the next scene
            SceneManager.LoadScene(nextLVL);
        }
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
	}
}
