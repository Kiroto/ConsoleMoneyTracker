﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.model
{
    public interface IIndexable<U>
    {
        public U ID { get; set; }
    }
}
