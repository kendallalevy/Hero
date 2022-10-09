using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class planeBehave : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "egg")
        {
            // destroy
            Destroy(other.gameObject);

            // update alpha
            Renderer r = gameObject.GetComponent<Renderer>();
            Color c = r.material.color;
            c.a = r.material.color.a - 0.25f;

            // if hit 4 times
            if (c.a == 0) {
                // update plane value & destroy
                Spawner s = GameObject.FindGameObjectWithTag("spawner").GetComponent<Spawner>();
                s.decrementPlane();
                Destroy(this.gameObject);
                return;
            }
            r.material.color = c;
        }
        else if (other.gameObject.tag == "bigEgg")
        {
            // destroy
            Destroy(other.gameObject);

            // update plane value & destroy
            Spawner s = GameObject.FindGameObjectWithTag("spawner").GetComponent<Spawner>();
            s.decrementPlane();
            Destroy(this.gameObject);
        }
    }
}
