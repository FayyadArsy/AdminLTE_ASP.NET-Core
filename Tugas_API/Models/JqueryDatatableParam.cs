using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Tugas_API.Models
{
    public class JqueryDatatableParam
    {
        public int draw { get; set; }
        public int iDisplayStart { get; set; }
        public int iDisplayLength { get; set; }
        public SearchParam search { get; set; }
        // Anda dapat menambahkan properti lain sesuai kebutuhan
    }

    public class SearchParam
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }

}
