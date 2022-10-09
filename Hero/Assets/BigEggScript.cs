using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEggScript : MonoBehaviour
{
    public int speed;

    // Update is called once per frame
    void Update()
    {
        // update position
        transform.position += transform.up * Time.smoothDeltaTime * speed;
    }

    void OnBecameInvisible()
    {
        Spawner s = GameObject.FindGameObjectWithTag("spawner").GetComponent<Spawner>();
        s.decrementEgg();
        if (this != null)
        {
            Destroy(this.gameObject);
        }
    }
}
