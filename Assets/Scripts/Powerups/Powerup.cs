
[System.Serializable]
public class Powerup
{
    // Defines the float and bool of the duration of the powerup through the tank data.
    public float buffDurationMax;
    public float buffDurationCurrent;
    public bool isPerm;

    public virtual void OnActivated(TankData data)
    {

    }
    public virtual void OnDeactivated(TankData data)
    {

    }
}
