using UnityEngine;

public class FlamePillar : MonoBehaviour
{
    public GameObject fire;

    private void Awake()
    {
        fire.SetActive(false);
    }
}
