using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class AlienDB_Usuario_Sistema
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Login")]
        public string Nom_Login { get; set; }

        [Display(Name = "Nome completo")]
        public string Nom_Completo { get; set; }

        public int Id_Empresa{ get; set; }

        [ForeignKey("Id_Empresa")]
        public virtual AlienDB_Empresa Empresa { get; set; }

        [Display(Name = "Melhor e-mail")]
        public string Des_Email { get; set; }

        [Display(Name = "Senha")]
        public string Des_Senha { get; set; }

        [NotMapped]
        public string Des_Senha_Criptografada { get; set; }

        [Display(Name = "Inativado em:")]
        public DateTime? Dat_Hora_Fim { get; set; }

        [Display(Name = "Tags")]
        public string Des_Tag { get; set; }

        [Display(Name = "CPF")]
        public string Nro_CPF { get; set; }

        [Display(Name = "RG")]
        public string Nro_RG { get; set; }

        [Display(Name = "Endereço (Rua/Avenida, Nº, Bairro)")]
        public string Des_Endereco { get; set; }

        [Display(Name = "Telefone")]
        public string Des_Telefone { get; set; }

        [Display(Name = "Cidade")]
        public string Des_Cidade { get; set; }

        [Display(Name = "UF")]
        public string Des_UF { get; set; }                
    }
}
