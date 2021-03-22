using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinchBeakControl : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite BeakClosedSprite;
    public Sprite BeakOpenSprite;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //BeakClosedSprite = Resources.Load("Sprites/large_beak_close_cutout.png") as Sprite;
        //BeakOpenSprite = Resources.Load("Sprites/large_beak_open_cutout.png") as Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed.");
            spriteRenderer.sprite = BeakClosedSprite;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Space key was released.");
            spriteRenderer.sprite = BeakOpenSprite;
        }
    }
}
