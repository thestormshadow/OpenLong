using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Kernel.Modules.Systems.Events
{
    public interface ILineSkillPK
    {
        public Task ForceStartup();
        public Task ForceEnd();
    }
}
