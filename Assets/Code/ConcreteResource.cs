using System;

public class ConcreteResource
{
    private int amount;
    public int Amount
    {
        get { return amount; }
        set
        {
            amount = value;


            if (amount > maxAmount)
                amount = maxAmount;


            AmountChanged?.Invoke(Amount);
        }
    }


    public int maxAmount;
    public int MaxAmount
    {
        get { return maxAmount; }
        set
        {
            maxAmount = value;

            if (amount > maxAmount)
                amount = maxAmount;

            MaxAmountChanged?.Invoke(MaxAmount);
        }
    }


    public StrategicResource resource;

    public ConcreteResource(StrategicResource r, int maxAmount)
    {
        resource = r;
        this.maxAmount = maxAmount;
    }


    public event Action<int> AmountChanged;
    public event Action<int> MaxAmountChanged;
}
