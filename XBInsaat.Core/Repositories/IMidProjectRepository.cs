﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;

namespace XBInsaat.Core.Repositories
{
    public interface IMidProjectRepository : IRepository<MidProject>
    {
        MidProject MaxRow(int highProjectId);
    
    }
}
