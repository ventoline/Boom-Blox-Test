using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Tower : MonoBehaviour
{

    public int height = 8;
    public int width = 4;
    //public int depth; //keeping the tower square based for now
     public float scale = 1; // change the blox sizes

    public GameObject block;
    public Text scoreText;



    private GameObject[] blocks;
    public void Awake()
    {
        blocks = new GameObject[height * width * width];


        for (int i = 0; i < height; i++)
        {

            for (int j = 0; j < width; j++)
            {

                for (int k = 0; k < width; k++)
                {

                    // create new block 
                    GameObject brick = Instantiate(block, new Vector3(j * 1.0001f - (width / 2) + (scale / 2), (i * 1.0001f), k * 1.0001f - (width / 2) + (scale / 2)), Quaternion.identity);
                    brick.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                    // add it to the tower
                    brick.transform.parent = transform;


                    // keep track of it
                    blocks[(i * width * width) + (j * width) + k] = brick;

                }

            }

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject brick in blocks)
        {
            brick.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //the score count blox that have fallen from the platform
        int score = 0;
        foreach (GameObject brick in blocks)
        {
            if (brick.transform.position.y < GameObject.Find("floor").transform.position.y)
            {
                score++;
                // display score
                scoreText.text = "Score: " + score + "/" + width * width * height;
            }
        }


    }
}
