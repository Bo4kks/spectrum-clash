public struct OnPlayerEarnedRedCurrencyEvent 
{
    public int CurrencyValue { get; private set; }

    public OnPlayerEarnedRedCurrencyEvent(int currencyValue)
    {
        CurrencyValue = currencyValue;
    }
}
