using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class arrowMove : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public TextMeshProUGUI HeroData;
    private int mode;
    private string modeText;
    private int touchedEnemies;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        // update mode
        if (Input.GetKeyDown(KeyCode.M)) {
            mode = (mode + 1) % 2;
            speed = 20;
        }

        // key mode
        if (mode == 1) {
            modeText = "Key";

            // Change Speed
            speed += Input.GetAxis("Vertical");
            transform.position += transform.up * (speed * Time.smoothDeltaTime) / 7f;

        // mouse mode
        } else {
            modeText = "Mouse";
            Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            p.z = 0f;
            transform.position = p;
        }

        // Change Direction
        transform.Rotate(Vector3.forward, -1f * Input.GetAxis("Horizontal") * (rotateSpeed * Time.smoothDeltaTime));

        // update text
        HeroData.text = "HERO: Drive(" + modeText + ") TouchedEnemy(" + touchedEnemies.ToString() + ")";

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Spawner s = GameObject.FindGameObjectWithTag("spawner").GetComponent<Spawner>();
            s.decrementPlane();
            touchedEnemies++;
            Destroy(other.gameObject);
        }
    }
}
