public struct OnUIHPChanged
{
    public float HPValue { get; private set; }

    public OnUIHPChanged(float value)
    {
        HPValue = value;
    }
}
