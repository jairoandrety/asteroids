using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private List<ShipSetup> shipSetups;
    [SerializeField] private List<ProjectileSetup> projectileSetups;

    private int shipSelected=0;
    private int projectileSelected = 0;

    [SerializeField] private int deaths = 0;
    [SerializeField] private int points = 0;

    [SerializeField] private Character character;

    [Header("Asteroids")]
    [SerializeField] private List<GameObject> asteroidPoints;
    private List<Asteroid> asteroids = new List<Asteroid>();
    private bool isGenertaing = false;
    private IEnumerator asteroidCoroutine = null;

    [SerializeField] private CoreView view;

    // Start is called before the first frame update
    void Start()
    {
        character.OnDead += AddDeath;

        deaths = 0;
        points = 0;

        SetupShip();
        SendGenerate();
    }
    
    public void SetupShip()
    {
        character.setShip(shipSetups[shipSelected], projectileSetups[projectileSelected]);
    }

    public void SetShip(int value)
    {
        shipSelected = value;
        SetupShip();
    }

    public void SetProjectile(int value)
    {
        projectileSelected = value;
        SetupShip();
    }

    private void AddPoint()
    {
        points++;
        view.SetValues(points.ToString(), deaths.ToString());
    }

    private void AddDeath()
    {
        deaths++;
        view.SetValues(points.ToString(), deaths.ToString());
    }

    #region Asteroids
    public void SendGenerate()
    {
        isGenertaing = true;

        if (asteroidCoroutine != null)
            StopCoroutine(asteroidCoroutine);

        asteroidCoroutine = GenerateAsteroids();
        StartCoroutine(asteroidCoroutine);
    }

    public IEnumerator GenerateAsteroids()
    {
        while (isGenertaing)
        {
            GameObject item = ObjectPooler.Instance.GetPooledObject("asteroid");
            Asteroid asteroid = item.GetComponent<Asteroid>();
            
            if(!asteroids.Exists(i=> i == asteroid))
            {
                asteroids.Add(asteroid);
                asteroid.OnDestroy += AddPoint;
            }

            GameObject selected = asteroidPoints[Random.Range(0, asteroidPoints.Count - 1)];
            Quaternion direction = Quaternion.LookRotation(selected.transform.position - new Vector3(Random.Range(-10, 20), Random.Range(-10, 20), 0));
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            asteroid.CreateAsteroid(selected.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            asteroid.Move();
            yield return new WaitForSeconds (2f);
        }
    }
    #endregion
}
