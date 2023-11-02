using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Transform tf;


    public float maxHealt;
    public float currentHealt;


    public bool imHit;

    // Start is called before the first frame update
    void Start()
    {
        maxHealt = 100f;
        currentHealt = maxHealt;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        tf = gameObject.GetComponent<Transform>();
        imHit = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (imHit)
        {
            Debug.Log("imHit (coglione)");
            currentHealt = currentHealt - 10f;
            Debug.Log(currentHealt);
            imHit = false;
        }
        if (currentHealt == 0f)
        {
            StartCoroutine(killPlayer());
        }
        if (tf.position.y < -20f) 
        {
            currentHealt = 0f;
            StartCoroutine(killPlayer());
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
        rb2D.AddForce(new Vector2(0f, -0.2f), ForceMode2D.Impulse);
        currentHealt = maxHealt;
    }
}
