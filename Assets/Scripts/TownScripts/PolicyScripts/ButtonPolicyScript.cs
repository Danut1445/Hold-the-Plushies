using UnityEngine;

public class ButtonPolicyScript : MonoBehaviour
{
    public GameObject description;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        description.SetActive(false);
    }

    private void OnMouseEnter()
    {
        description.SetActive(true);
    }

    private void OnMouseExit()
    {
        description.SetActive(false);
    }
}
