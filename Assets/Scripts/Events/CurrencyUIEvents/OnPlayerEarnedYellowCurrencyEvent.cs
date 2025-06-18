public struct OnPlayerEarnedYellowCurrencyEvent
{
    public int CurrencyValue { get; private set; }

    public OnPlayerEarnedYellowCurrencyEvent(int currencyValue)
    {
        CurrencyValue = currencyValue;
    }
}
