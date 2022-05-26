using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private bool isPowerup;
    public float powerPush = 15.0f;
    public GameObject powerIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed);
        powerIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(7);
        isPowerup = false;
        Debug.Log("Power timer up");
        powerIndicator.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))   // checking if player collides with powerup and destroy the powerup game object on collision
        {
            isPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdown());
            powerIndicator.gameObject.SetActive(true);
        
        }
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        
        Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 powerPushaway = collision.gameObject.transform.position - transform.position;  // putting the equation in brackets doesn't work....whatever!
        if(collision.gameObject.CompareTag("Enemy") && isPowerup)
        {
            Debug.Log("Collided with" + collision.gameObject.name + "and power up is" + isPowerup);
            enemyRb.AddForce(powerPushaway * powerPush, ForceMode.Impulse);
        }
    }

}
