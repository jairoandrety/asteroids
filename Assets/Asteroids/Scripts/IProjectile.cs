using System.Collections;
using UnityEngine;

public interface IDestroyable
{
    void Destroy();
}

public interface IProjectile
{
    void SetProjectile(ProjectileSetup projectileSetup);
    void Active(Vector3 position, Quaternion rotation);
    void Move();
    IEnumerator Moving();
}
