using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy.Extentions
{
    public static class Check
    {
        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>(
            T value,
            [InvokerParameterName][NotNull] string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }
    }
}
