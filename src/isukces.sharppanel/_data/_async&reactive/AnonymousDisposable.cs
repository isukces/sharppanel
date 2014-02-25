using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace isukces.sharppanel
{
    public class AnonymousDisposable : IDisposable
    {
        #region Constructors

        public AnonymousDisposable(Action onDispose)
        {
            disposeAction = onDispose;
        }

        #endregion Constructors

        #region Methods

        // Public Methods 

        public void Dispose()
        {
            this.disposeAction();
        }

        #endregion Methods

        #region Fields

        private readonly Action disposeAction;

        #endregion Fields
    }

}

