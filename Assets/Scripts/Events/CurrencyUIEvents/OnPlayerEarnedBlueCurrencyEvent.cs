public struct OnPlayerEarnedBlueCurrencyEvent
{
    public int CurrencyValue { get; private set; }

    public OnPlayerEarnedBlueCurrencyEvent(int currencyValue)
    {
        CurrencyValue = currencyValue;
    }
}
