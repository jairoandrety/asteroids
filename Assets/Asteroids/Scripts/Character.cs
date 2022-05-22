using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    None = 0,
    Automatic = 1,
    Normal = 2    
}

public class Character : MonoBehaviour, IDestroyable
{
    public Action OnDead;

    [SerializeField] private ShipSetup shipSetup;
    [SerializeField] private ProjectileSetup ProjectileSetup;
    [SerializeField] private LayerMask layer;

    [SerializeField] private CharacterMovement movement;
    [SerializeField] private CharacterInput input;

    [SerializeField] private GameObject graphic;
    [SerializeField] private Collider2D collider;

    [SerializeField] private Transform shootingPoint;
    [SerializeField] private IProjectile projectile;

    public List<GameObject> projectiles;

    private IEnumerator respawnCoroutine = null;

    #region Unity Behavior
    void Start()
    {
        input.OnMovement += movement.SetDirection;
        input.OnShoot += Shoot;
        ActiveShip(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((layer.value & (1 << collision.gameObject.layer)) > 0)
        {
            Destroy();
        }
    }
    #endregion

    public void setShip(ShipSetup shipSetup, ProjectileSetup projectileSetup)
    {
        this.shipSetup = shipSetup;
        this.ProjectileSetup = projectileSetup;
    }

    private void Shoot()
    {
        GameObject item = ObjectPooler.Instance.GetPooledObject("projectile");
        projectile = item.GetComponent<Projectile>();
        projectile.SetProjectile(ProjectileSetup);
        projectile.Active(shootingPoint.transform.position, transform.rotation);
        projectile.Move();
    }

    public void ActiveShip(bool value)
    {
        graphic.SetActive(value);
        movement.ActiveMovement(value);
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        collider.enabled = value;
    }

    public void Destroy()
    {
        ActiveShip(false);
        OnDead?.Invoke();

        if (respawnCoroutine != null)
            StopCoroutine(respawnCoroutine);

        respawnCoroutine = Respawn();
        StartCoroutine(respawnCoroutine);
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);
        ActiveShip(true);

        yield return null;
    }


}
//000000001 <- 