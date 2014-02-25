using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace isukces.sharppanel
{
    public enum WellKownCpanelProblems
    {
        Unknown,
        LoginError,
        NameResolutionFailure,

        EmptyOrInvalidCPanelURL,
        EmptyUsernameOrPassword
    }
}
