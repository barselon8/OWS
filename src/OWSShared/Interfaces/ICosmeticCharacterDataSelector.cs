using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWSShared.Interfaces
{
    public class PRCustomCharacterDataSelector : ICustomCharacterDataSelector
    {
        public bool ShouldExportThisCustomCharacterDataField(string fieldName)
        {
            if (fieldName == "CosmeticCharacterData")
            {
                return true;
            }

            return false;
        }
    }
}
