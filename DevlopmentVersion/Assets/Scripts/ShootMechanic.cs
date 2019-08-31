/*
 * ShootMechanic class
 *
 * Contains possibility to control the canon.
 * 
 * Author: Martin Schuster 
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShootMechanic : MonoBehaviour
{
    [SerializeField] private GameObject canon;
    [SerializeField] private GameObject canonBarrel;
    [SerializeField] [Range(5, 100)] private float forcePercantage;
    [SerializeField] private float maxForce = 50f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] [Range(10, 80)] private float rotateX = 10;
    [SerializeField] [Range(-45, 45)] private float rotateY;
    [SerializeField] private GameObject trajectorieObject;
    [SerializeField] private Slider power;
    private bool reload;
    public ScoreManager scoreManager;
    private void Start()
    {
        canon = GameObject.FindGameObjectWithTag("Canon");
        canon.transform.eulerAngles = new Vector3(0, -90 + rotateY, 0);
        canonBarrel = GameObject.FindGameObjectWithTag("Barrel");
        canonBarrel.transform.eulerAngles = new Vector3(rotateX, -90, 0);
        power.value = forcePercantage;
        scoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !reload)
        {
            ShootProjectile();
            reload = true;
            StartCoroutine(ReloadCanon());
        }

        if (Input.GetKey(KeyCode.E))
        {
            rotateY += 35f * Time.deltaTime;
            rotateY = Mathf.Clamp(rotateY, -45, 45);
            canon.transform.eulerAngles = new Vector3(0, rotateY + -90, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            rotateY -= 35f * Time.deltaTime;
            rotateY = Mathf.Clamp(rotateY, -45, 45);
            canon.transform.eulerAngles = new Vector3(0, rotateY + -90, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rotateX += 25f * Time.deltaTime;
            rotateX = Mathf.Clamp(rotateX, 10, 80);
            canonBarrel.transform.eulerAngles = new Vector3(rotateX, rotateY + -90, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rotateX -= 25f * Time.deltaTime;
            rotateX = Mathf.Clamp(rotateX, 10, 80);
            canonBarrel.transform.eulerAngles = new Vector3(rotateX, rotateY + -90, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            forcePercantage += 30f * Time.deltaTime;
            forcePercantage = Mathf.Clamp(forcePercantage, 5, 100);
            power.value = forcePercantage;
        }

        if (Input.GetKey(KeyCode.S))
        {
            forcePercantage -= 30f * Time.deltaTime;
            forcePercantage = Mathf.Clamp(forcePercantage, 5, 100);
            power.value = forcePercantage;
        }
    }

    private void ShootProjectile()
    {
        var canonBallRotation = projectileSpawnPoint.rotation;
        var canonBall = Instantiate(projectile, projectileSpawnPoint.position, canonBallRotation);
        canonBall.GetComponent<Rigidbody>().AddForce(projectileSpawnPoint.forward * (forcePercantage / 100 * maxForce));
        scoreManager.IncreaseShots();
        GetComponent<AudioSource>().Play();
    }


    private IEnumerator ReloadCanon()
    {
        yield return new WaitForSeconds(3f);
        reload = false;
    }
}