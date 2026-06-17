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
            GameObject spawnedTurret = GameObject.Instantiate(turret.turretToSpawn, gameObject.transform.position, Quaternion.identity);
            spawnedTurret.GetComponent<TurretLogicScript>().setCosts(turret.cost, gameObject, GameObject.FindGameObjectWithTag("Logic"));
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }
}
