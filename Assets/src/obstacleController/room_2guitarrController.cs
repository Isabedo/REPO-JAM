using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_2guitarrController : MonoBehaviour
{
    public int guitarStrings=3; 
    public bool canPiano=false;
    public gameManager gameManager;
    bool done=false;
    public GameObject[] strings;
    public room_2pianoController pianoController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.completed[0] && gameManager.completed[1]&&!done)
        {
            for (int i = 0; i < strings.Length; i++)
            {
                strings[i].SetActive(true);
            }
            done = true;
        }
        if (guitarStrings <= 0)
        {
            canPiano = true;
            pianoController.canPlay = true;
        }
    }
}
