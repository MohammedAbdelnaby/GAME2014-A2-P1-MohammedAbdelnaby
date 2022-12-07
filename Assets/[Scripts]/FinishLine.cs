using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.GetComponent<Player>().enabled = false;
            StartCoroutine("Win");
        }
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(2.0f);
        
        SceneManager.LoadScene("Win");
    }
}
