﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBInsaat.Core.Entites;
using XBInsaat.Core.Repositories;
using XBInsaat.Data.Datacontext;

namespace XBInsaat.Data.Repositories
{

    public class MidProjectRepository : Repository<MidProject>, IMidProjectRepository
    {
        private readonly DataContext _context;

        public MidProjectRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public MidProject MaxRow(int highProjectId)
        {
            var entityWithMaxRow = _context.MidProjects.Where(x => x.HighProjectId == highProjectId).OrderByDescending(e => e.Row).FirstOrDefault();
            return entityWithMaxRow;


        }
    }
}
