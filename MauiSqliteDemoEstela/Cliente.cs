using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiSqliteDemoEstela
{
    [Table("Clientes")]
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(150)]
        public string Correo { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;
    }
}
