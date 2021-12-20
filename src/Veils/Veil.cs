using System.Linq.Expressions;

namespace Veils;

public class Veil<T> where T : class
{
    private readonly T _origin;
    private bool _pierced;
    private readonly IEnumerable<KeyValuePair<string, object>> _data;

    public Veil(T origin, params KeyValuePair<string, object>[] data)
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
                var item = _data.FirstOrDefault(x => string.Equals(x.Key, propName, StringComparison.Ordinal));
                if (item.Value is not null)
                {
                    return item.Value;
                }
            }
            var propInfo = _origin.GetType().GetProperty(propName) ?? throw new ArgumentException($"Unknow property name: {propName}");
            return propInfo.GetValue(_origin);
        }

        set
        {
            _pierced = true;
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
}
