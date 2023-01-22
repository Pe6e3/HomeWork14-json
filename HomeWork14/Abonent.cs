using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork14;

internal class Abonent
{
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Tel { get; set; }

    public Abonent(string? surname, string? name, string? tel)
    {
        Surname = surname == null ? "" : surname;
        Name = name == null ? "" : name;
        Tel = tel == null ? "" : tel;
    }

}
