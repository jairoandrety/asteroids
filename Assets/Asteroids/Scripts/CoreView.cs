using UnityEngine;
using UnityEngine.UI;

public class CoreView : MonoBehaviour
{
    public Text points;
    public Text deaths;

    public void SetValues(string points, string deaths)
    {
        this.points.text = points;
        this.deaths.text = deaths;
    }
}
