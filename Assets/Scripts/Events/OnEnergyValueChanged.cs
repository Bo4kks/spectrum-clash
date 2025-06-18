public struct OnEnergyValueChanged
{
    public float EnergyValue { get; private set; }

    public OnEnergyValueChanged(float value)
    {
        EnergyValue = value;
    }
}
