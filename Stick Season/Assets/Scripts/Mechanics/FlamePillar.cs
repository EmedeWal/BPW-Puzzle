using UnityEngine;

public class FlamePillar : MonoBehaviour
{
    [SerializeField] private GameObject fire;

    private void Awake()
    {
        fire.SetActive(false);
    }

    public void LightFlame()
    {
        fire.SetActive(true);
    }

    public void ExtinguishFlame()
    {
        fire.SetActive(false);
    }
}
