namespace Memo;

public class Address
{
    public Int32 Value { get; }
    public Address(Int32 _Value)
    {
        Value = _Value;
    }
    public static implicit operator Address(Int32 _Value)
    {
        return new Address(_Value);
    }
    public static implicit operator Int32(Address _address)
    {
        return _address.Value;
    }
    public static Address operator +(Address a, Int32 b)
    {
        return new Address(a.Value + b);
    }
    public static Address operator -(Address a, Int32 b)
    {
        return new Address(a.Value - b);
    }
    public static bool operator >(Address a, Int32 b)
    {
        return a.Value > b;
    }
    public static bool operator <(Address a, Int32 b)
    {
        return a.Value < b;
    }
    public static bool operator ==(Address a, Int32 b)
    {
        return a.Value == b;
    }
    public static bool operator >=(Address a, Int32 b)
    {
        return a.Value >= b;
    }
    public static bool operator <=(Address a, Int32 b)
    {
        return a.Value <= b;
    }
    public static bool operator !=(Address a, Int32 b)
    {
        return a.Value != b;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string? ToString()
    {
        return $"Address: 0x{Value:X}";
    }
    public override bool Equals(object? obj)
    {
        return false;
    }
}