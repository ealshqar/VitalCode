using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vital.Business.Shared.DomainObjects.AppImages
{
    public class AppImage : DomainEntity
    {
        public string Property
        {
            get; set;
        }
        
        public byte[] Value
        {
            get; set;
        }
    }
}
