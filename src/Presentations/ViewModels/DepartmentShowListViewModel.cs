using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Presentations.ViewModels;
public class DepartmentShowListViewModel
{
    public int? Id {get; set;} 
    public string Name { get; set; } = string.Empty;

    ///コンストラクタ
    public DepartmentShowListViewModel(int? id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
}
