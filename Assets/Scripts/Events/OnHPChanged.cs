public struct OnHPChanged
{
    public float HPValue { get; private set; }

    public OnHPChanged(float value)
    {
        HPValue = value;
    }
}
