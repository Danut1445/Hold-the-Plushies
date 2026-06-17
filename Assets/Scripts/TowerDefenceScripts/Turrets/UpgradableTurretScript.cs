using UnityEngine;

public abstract class UpgradableTurretScript : MonoBehaviour
{
    public abstract void UpgradeTurret(int value1, float value2);

    public abstract void RemoveTurret();

    public abstract float GetRange();
}
