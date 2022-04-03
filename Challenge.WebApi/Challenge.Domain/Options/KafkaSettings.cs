﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Domain.Options
{
    public class KafkaSettings
    {
        public string Topic { get; set; }
        public string GroupId { get; set; }
        public string BootstrapServers { get; set; }
    }
}
