using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AlienDB_Imovel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Id_Empresa")] 
        public int Id_Empresa { get; set; }

        public int Id_Tipo_Imovel { get; set; }

        [Required]
        [ForeignKey("Id_Tipo_Imovel")]
        public virtual AlienDB_Tipo_Imovel TipoImovel { get; set; }


        [Required]
        [Display(Name = "Código chave")]
        public int Codigo_chave { get; set; }

   
        [Display(Name = "Endereço")]
        [MaxLength(400, ErrorMessage = "Máximo {1} caracteres permitidos")]
        public string Endereco { get; set; }
        
        [Display(Name = "Complemento")]
        [MaxLength(100, ErrorMessage = "Máximo {1} caracteres permitidos")] 
        public string Complemento { get; set; }

 
        [Display(Name = "Bairro")]
        [MaxLength(400, ErrorMessage = "Máximo {1} caracteres permitidos")] 
        public string Bairro { get; set; }

 
        [Display(Name = "Cidade")]
        [MaxLength(400, ErrorMessage = "Máximo {1} caracteres permitidos")] 
        public string Cidade { get; set; }

  
        [Display(Name = "Quantidade de dormitórios")]        
        public int? Qtd_Dormitorios { get; set; }

        [Display(Name = "Quantidade de sala de estar")]        
        public int? Qtd_Sala_estar { get; set; }

       
        [Display(Name = "Quantidade de sala de jantar")]
        public int? Qtd_Sala_jantar { get; set; }

       
        [Display(Name = "Quantidade de banheiros")]
        public int? Qtd_Banheiro { get; set; }

        [Display(Name = "Quantidade de lavanderias")]        
        public int? Qtd_Lavanderia { get; set; }

   
        [Display(Name = "Quantidade de edículas")]
        public int? Qtd_Edicula { get; set; }

    
        [Display(Name = "Quantidade de suítes")]        
        public int? Qtd_Suite { get; set; }

   
        [Display(Name = "Quantidade de sala de copas")]        
        public int? Qtd_Copa { get; set; }


        [Display(Name = "Quantidade de carros na garagem")]        
        public int? Qtd_Carro_Garagem { get; set; }

   
        [Display(Name = "Quantidade de dispensas")]
        public int? Qtd_Dispensa { get; set; }


        [Display(Name = "Quantidade de quintal")]
        public int? Qtd_Quintal { get; set; }


        [Display(Name = "Qtde Aluguel Advogado")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Qtd_Aluguel_Advogado { get; set; }


        [Display(Name = "Qtde Aluguel Pintura")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Qtd_Aluguel_Pintura { get; set; }

  
        [Display(Name = "A partir de qual parcela a imobiliária recebe?")]
        public decimal? Nro_Parcela_Inicio_Aluguel_Imobiliaria { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }


        [Display(Name = "R$ IPTU")]
        [Column(TypeName = "decimal(18, 2)")]
        public Decimal? Valor_IPTU { get; set; }

        [Display(Name = "R$ Condomínio")]
        [Column(TypeName = "decimal(18, 2)")]
        public Decimal? Valor_condominio { get; set; }
        
 
        [Display(Name = "R$ Administrativa")]
        [Column(TypeName = "decimal(18, 2)")]
        public Decimal? Valor_Taxa_Administrativa { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(18, 2)")]
        public Decimal? Valor_aluguel { get; set; }


        [Display(Name = "Condições do Imovel")]
        public string Condicoes_Imovel { get; set; }

        [Display(Name = "Data de saída da locação")]
        public DateTime? Dat_saida_locacao { get; set; }

        //[Display(Name = "Data de exclusão")]
        //public DateTime? Dat_fim { get; set; }

        [Display(Name = "Data de inclusão")]
        [DefaultValue("DateTime.Now")]
        public DateTime Dat_Inclui { get; set; }



        public int Id_Regiao { get; set; }

        [Required]
        [ForeignKey("Id_Regiao")]
        public virtual AlienDB_Regiao Regiao { get; set; }


        [Display(Name = "Destaque?")]
        public string Flg_destaque { get; set; }

        [Display(Name = "Status?")]
        public string Flg_status { get; set; }        
    }
}
