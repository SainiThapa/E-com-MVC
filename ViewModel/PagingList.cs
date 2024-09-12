using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomMVC.ViewModel
{
    public class PagingList<T> where T : class
    {
        // public PagingList()
        // {

        // }
        public List<T> Data { get; set; }
        public int TotalItems {get; set;} 
        public int PageNumber {get; set;}
        public int PageSize {get; set;} 


    }
}