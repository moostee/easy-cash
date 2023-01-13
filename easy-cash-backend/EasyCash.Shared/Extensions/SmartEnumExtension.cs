using Ardalis.SmartEnum;
using EasyCash.Shared.Exceptions;

namespace EasyCash.Shared
{
    public static class SmartEnumExtension
    {
        public static TEnum GetOrValidate<TEnum>(this string nameOrValue) where TEnum : SmartEnum<TEnum, string>
        {
            #region
            var list = new List<string>();
            var names = SmartEnum<TEnum, string>.List.Select(c => c.Name.ToUpperInvariant()).ToList();
            var values = SmartEnum<TEnum, string>.List.Select(c => c.Value.ToUpperInvariant()).ToList();

            list.AddRange(names);
            list.AddRange(values);

            if (!list.Contains(nameOrValue.ToUpperInvariant()))
            {
                throw new EasyCashException($"Invalid {typeof(TEnum).Name} value, Supported values include:" +
                    $" {string.Join(',', SmartEnum<TEnum, string>.List.Select(n => $"{n.Name}"))}");
            }
            #endregion
            nameOrValue.TryFind<TEnum>(out TEnum @enum);

            return @enum;
        }



 
        public static bool TryFind<TEnum>(this string nameOrValue, out TEnum @enumValue) where TEnum : SmartEnum<TEnum, string>
        {
            SmartEnum<TEnum, string>.TryFromName(nameOrValue, true, out TEnum @name);
            @enumValue = @name;

            SmartEnum<TEnum, string>.TryFromValue(nameOrValue, out TEnum @value);
            @enumValue = @value;


            return @enumValue is not null;
        }

    }
}
