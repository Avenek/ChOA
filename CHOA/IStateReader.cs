﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHOA
{
    public interface IStateReader
    {
        void LoadFromFileStateOfAlghoritm(string path);
    }
}