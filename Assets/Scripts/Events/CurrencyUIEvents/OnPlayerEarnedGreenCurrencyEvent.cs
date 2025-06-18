public struct OnPlayerEarnedGreenCurrencyEvent
{
    public int CurrencyValue { get; private set; }

    public OnPlayerEarnedGreenCurrencyEvent(int currencyValue)
    {
        CurrencyValue = currencyValue;
    }
}
