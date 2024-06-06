using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace WS04.Models
{
    [Table("Paciente")]
    public class Paciente
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nome_Paciente { get; set; }
        [NotNull]
        public string Nome_Doutor { get; set; }
        [NotNull]
        public string Tipo_quarto { get; set; }
        [NotNull]
        public int Quarto { get; set; }

        public Paciente()
        {
            this.Id = 0;
            this.Nome_Paciente = "";
            this.Nome_Doutor = "";
            this.Tipo_quarto = "";
            this.Quarto = 0;
        }
    }
}
