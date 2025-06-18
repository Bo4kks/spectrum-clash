public struct OnPlayerEarnedCurrencyEvent
{
    public CurrencyTypes CurrencyType { get; private set; }

    public OnPlayerEarnedCurrencyEvent(CurrencyTypes currencyType)
    {
        CurrencyType = currencyType;
    }
}
