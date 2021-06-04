using System;
using System.Linq;

namespace Sinuka.Infrastructure.Utils
{
    public static class ObjectUtils
    {
        // https://stackoverflow.com/questions/8702603/merging-two-objects-in-c-sharp
        public static void CopyValues(object target, object source)
        {
            var properties = source.GetType().GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }
        }
    }
}
