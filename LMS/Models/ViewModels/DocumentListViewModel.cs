using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models.ViewModels
{
    public class DocumentListViewModel
    {
        public string TypeHeader { get; set; }
        public string EmptyMessage { get; set; }
        public List<Document> Documents { get; set; }
    }
}
