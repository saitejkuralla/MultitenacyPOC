using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy
{
    public class TenantResolveResult
    {
        public string TenantIdOrName { get; set; }

        public List<string> AppliedResolvers { get; }

        public TenantResolveResult()
        {
            AppliedResolvers = new List<string>();
        }
    }
}
