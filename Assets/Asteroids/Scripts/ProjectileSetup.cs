using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSetup", menuName = "ScriptableObjects/ProjectileSetup")]
public class ProjectileSetup : ScriptableObject
{
    public string id;
    public ProjectileType type;
    public AnimationCurve curve;
    public float speed;
    public int damage;
}
