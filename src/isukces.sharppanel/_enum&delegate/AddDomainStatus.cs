using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace isukces.sharppanel
{
    public enum AddDomainStatus
    {
        AlreadyExists,

        UnableToAddAddonDomain,
        AddonDomainAdded,

        UnableToAddSubDomain,
        SubDomainAdded

    }
}
