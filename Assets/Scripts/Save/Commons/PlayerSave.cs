using System;

namespace Boilerplate.SaveCommons
{
    [Serializable]
    public class PlayerSave
    {
        #region Variables

        private int saveVersion = SaveConsts.SaveVersion;

        #endregion

        #region Properties

        public int SaveVersion => saveVersion;

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}