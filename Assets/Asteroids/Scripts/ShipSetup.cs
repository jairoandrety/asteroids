using UnityEngine;

[CreateAssetMenu(fileName = "ShipSetup", menuName = "ScriptableObjects/ShipSetup")]
public class ShipSetup : ScriptableObject
{
    public string id;
    public MovementType movement;

    public float speed = 0;
}
