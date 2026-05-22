using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using src.Applications.Domains;

namespace src.Presentations.ViewModel
{
    public class EmployeeViewAllViewModel
    {
        /// <summary>
        /// 社員のリスト
        /// </summary>
        [Display(Name = "社員一覧")]
        public List<Employee>? Employees {get; set;}
    }
}