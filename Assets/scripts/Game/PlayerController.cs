using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private Transform tf;
    private GameObject dummy;


    public float speed;
    public float jumpForce;
    private float moveHorizontal;
    private float moveVertical;
    public float punchCooldown;
    private float nextPunch;
    private float pyDirection;
    public float knockback;
    public float maxHealt;
    public float currentHealt;


    private int jumpLeft;


    private bool isGrounded;
    private bool isPressingE;
    private bool isJumping;


    private RaycastHit2D ray;

    void Start()
    {
        maxHealt = 100f;
        currentHealt = maxHealt;
        dummy = GameObject.Find("Dummy");
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        tf = gameObject.GetComponent<Transform>();
        jumpLeft = 0;
        isJumping = false;
        isGrounded = false;
        nextPunch = 0f;
        pyDirection = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        isPressingE = Input.GetKey(KeyCode.E);

    }

    private void FixedUpdate() 
    {
        if (Mathf.Abs(moveHorizontal) > 0.1f) 
        {
            rb2D.AddForce(new Vector2(moveHorizontal * speed, 0f), ForceMode2D.Impulse);
            if (Mathf.Abs(rb2D.velocity.x) > 10f) 
            {
                rb2D.velocity = new Vector2(rb2D.velocity.normalized.x * 10f, rb2D.velocity.y);
            }
        }
        else
        {
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
        }

        if (moveVertical > 0.1f) 
        {
            if (jumpLeft > 0 && !isJumping) 
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
                rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                jumpLeft--;
            }

            isJumping = true;  
                 
        }
        else
        {
            isJumping = false;
            rb2D.AddForce(new Vector2(0f, -0.2f), ForceMode2D.Impulse);
        }

        if (moveHorizontal > 0.1f) 
        {
            pyDirection = 1f;
        }
        
        if (moveHorizontal < -0.1f)
        {
            pyDirection = -1f;
        }


        ray = Physics2D.Raycast(transform.position, Vector2.down, 1.1f);
        if (ray.collider != null && !isJumping) 
        {
            isGrounded = true;
            jumpLeft = 2;
        }
        else
        {
            isGrounded = false;
        }

        if (tf.position.y < -20f) 
        {
            currentHealt = 0f;
            StartCoroutine(killPlayer());
        }

        if (isPressingE && Time.time > nextPunch)
        {
            Debug.Log("Pressing E");
            Debug.Log(pyDirection);
            LayerMask mask = LayerMask.GetMask("new");
            if (Physics2D.Raycast(transform.position, Vector2.right * new Vector2(pyDirection, 0f), 2f, mask)) 
            {
                Punch(mask);
                Debug.Log("Punch");
            }
            nextPunch = Time.time + punchCooldown;
        }
    }

    private IEnumerator killPlayer() 
    {
        tf.position = new Vector3(3f, 3f, 0f);
        rb2D.constraints = RigidbodyConstraints2D.FreezePosition;
        Debug.Log("Freeze");
        yield return new WaitForSeconds(3);
        rb2D.constraints = RigidbodyConstraints2D.None;
        rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        currentHealt = maxHealt;
    }

    private void Punch(LayerMask mask)
    {
        dummy.GetComponent<DummyController>().imHit = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * new Vector2(pyDirection, 0f), 2f, mask);
        GameObject Dummy = hit.transform.gameObject;
        Rigidbody2D rbDummy = Dummy.GetComponent<Rigidbody2D>();
        Transform tfDummy = Dummy.GetComponent<Transform>();
        rbDummy.velocity = new Vector2(knockback * pyDirection, rbDummy.velocity.y);

    }

}