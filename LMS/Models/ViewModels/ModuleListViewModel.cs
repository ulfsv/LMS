﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models.ViewModels
{
    public class ModuleListViewModel
    {
        public Course Course { get; set; }
        public List<Module> ModuleList { get; set; }
    }
}
