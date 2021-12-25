using System.Linq.Expressions;

namespace Veils;

public class Veil<T> where T : class
{
    private readonly T _origin;
    private readonly IEnumerable<(string, object)> _data;
    private bool _pierced;

    public Veil(T origin, params (string, object)[] data)
    {
        _origin = origin;
        _data = data;
    }

    public object? this [string propName]
    {
        get
        {
            if (!_pierced)
            {
                var pair = _data.FirstOrDefault(x => string.Equals(x.Item1, propName, StringComparison.Ordinal));
                if (pair.Item2 is not null)
                {
                    return pair.Item2;
                }
                _pierced = true;
            }
            var propInfo = _origin.GetType().GetProperty(propName) ?? throw new ArgumentException($"Unknow property name: {propName}");
            return propInfo.GetValue(_origin);
        }

        set
        {
            if (!_pierced) _pierced = true;
            var propInfo = _origin.GetType().GetProperty(propName) ?? throw new ArgumentException($"Unknow property name: {propName}");
            propInfo.SetValue(_origin, value);
        }
    }

    public TField Get<TField>(Expression<Func<T, TField>> expr)
    {
        if (expr.Body is not MemberExpression memExpr)
        {
            throw new InvalidOperationException();
        }
        return (TField)(this[memExpr.Member.Name]!);
    }

    public void Set(Action<T> mutate)
    {
        if (!_pierced) _pierced = true;
        mutate(_origin);
    }
}
