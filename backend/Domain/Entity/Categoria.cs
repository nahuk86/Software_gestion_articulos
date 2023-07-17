﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity;
public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; }


    public DateTime FechaCreacion { get; set; }

}
