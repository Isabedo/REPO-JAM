using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mask_collision : MonoBehaviour
{
    [Header("Sprite Flip Book file")]
    public SpriteFlipbook flipbook;

    public gameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameManager.completed[0]=true;
            flipbook.Play();
            Destroy(gameObject);

        }
    }
}
