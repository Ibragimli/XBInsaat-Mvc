﻿using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Services.Services.Interfaces
{
    public interface IEmailServices
    {
        public Task Send(string to, string subject, string html);
        public Task Send(string to, string subject, BodyBuilder html);
    }
}
