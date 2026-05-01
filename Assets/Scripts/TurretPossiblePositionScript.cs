using UnityEngine;

public class TurretPossiblePositionScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool OnCardDrop(TurretPlushyCardScript turret)
    {
        if (GameObject.FindGameObjectWithTag("Logic").GetComponent<LevelLogicScript>().spendMoney(turret.cost))
        {
            GameObject.Instantiate(turret.turretToSpawn, gameObject.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }
}
