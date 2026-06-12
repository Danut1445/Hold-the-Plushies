using UnityEngine;

public class RequirementScript : MonoBehaviour
{
    public Sprite requirementNotMet;
    public Sprite requirementMet;
    public GameObject destinationPolicy;

    private bool active;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        active = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = requirementNotMet;
    }

    public void Activate()
    {
        active = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = requirementMet;
        destinationPolicy.GetComponent<PolicyBasicScript>().CheckIfChoseable();
    }

    public bool IsActive()
    {
        return active;
    }
}
