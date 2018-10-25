public class RefVal<T>
{
    private T backing;
    public T Value { get { return backing; } }
    public RefVal(T reference)
    {
        backing = reference;
    }
}