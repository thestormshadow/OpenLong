﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Kernel.Modules.Systems.Events
{
    public interface ICaptureTheFlag
    {
        public Task PrepareEventAsync();
        public Task EndEventAsync();
    }
}