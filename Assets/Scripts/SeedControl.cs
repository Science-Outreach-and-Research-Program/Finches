using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SeedControl : MonoBehaviour
{
    public const int IN_BUSH = 0;
    public const int IN_BEAK = 1;
    public const int IN_NEST = 2;
    public const int FALLING = 3;

    public string size;
    public float speed;
    public int timeUntilFalling = 3600;
    public int state;
    private Rigidbody2D rigidbody2D;
    private GameObject finch;
    
    public void SetFinch(GameObject finch)
    {
        this.finch = finch;
        if (finch)
        {
            UpdateState(IN_BEAK, 100);
        }
        else
        {
            UpdateState(FALLING, 0);
        }
    }

    public void UpdateState(int state, int timeUntilFalling)
    {
        this.state = state;
        if (state == IN_BUSH)
        {
            this.timeUntilFalling = timeUntilFalling;
            rigidbody2D.gravityScale = 0;
            rigidbody2D.velocity = Vector2.zero;
        }
        if (state == IN_BEAK)
        {
            this.timeUntilFalling = timeUntilFalling;
            rigidbody2D.gravityScale = 0;
            rigidbody2D.velocity = Vector2.zero;
        }
        if (state == IN_NEST)
        {
            this.timeUntilFalling = timeUntilFalling;
            rigidbody2D.gravityScale = 0;
            rigidbody2D.velocity = Vector2.zero;
        }
        if (state == FALLING)
        {
            rigidbody2D.gravityScale = 1;
        }
        //Debug.Log("UPDATE STATE!\nState " + state + ", timeUntilFalling " + timeUntilFalling);
    }

    

    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        // QualitySettings.vSyncCount = 1;

        rigidbody2D = GetComponent<Rigidbody2D>();
        UpdateState(IN_BUSH, 3600);
        // rigidbody2D.freezePosition = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == FALLING)
        {
            timeUntilFalling--;
            if (timeUntilFalling < 0)
            {
                UpdateState(FALLING, 0);
            }
        }
        
        if (state == IN_BEAK)
        {
            Vector3 finchPos = finch.transform.position;
            transform.position = finchPos - FinchBeakControl.offset;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
