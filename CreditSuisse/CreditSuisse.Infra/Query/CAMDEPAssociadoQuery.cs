using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carga.Generica.Infra.Query
{
    public class CAMDEPAssociadoQuery
    {
        public string Get
        {
            get
            {
				return @"SELECT
							ass.ASS_STR_NR_CPF					AS CPF,
							ass.ASS_INT_ID_TIPOCARTEIRA			AS IdTipoCarteira,
							ass.ASS_STR_DS_NOME1				AS Nome1,
							ass.ASS_STR_DS_NOME2				AS Nome2,
							ass.ASS_STR_DS_NOMEPARLAMENTAR		AS NomeParlamentar,
							ass.ASS_STR_DS_FILIACAO1			AS Filiacao1,
							ass.ASS_STR_DS_FILIACAO2			AS Filiacao2,
							ass.ASS_STR_DS_NATURALIDADE			AS Naturalidade,
							ass.ASS_DAT_DT_NASCIMENTO			AS DataNascimento,
							ass.ASS_BLB_IMG_FOTO				AS ImgFoto,
							ass.ASS_DAT_DT_EMISSAO				AS DataEmissao,
							ass.ASS_DAT_DT_VALIDADE				AS DataValidade,
							ass.ASS_STR_DS_LOCALEXPEDICAO		AS LocalExpedicao,
							ass.ASS_STR_DS_RG					AS RG,
							ass.ASS_STR_DS_GRUPOSANGUINEO		AS GrupoSanguineo,
							ass.ASS_STR_DS_LEGISLATURA			AS CodigoLegislatura,
							ass.ASS_STR_DS_SEXO					AS Sexo,
							ass.ASS_STR_DS_CARGO                AS Cargo,
							NVL(ass.ASS_BOL_FL_PORTEARMAS, 0)			AS PorteArmas,
							ass.ASS_STR_NR_MATRICULA			AS Matricula,
							NVL(ass.ASS_INT_ID_SITUACAO, 0)				AS Aposentado
						FROM 
							SPIF_ASSOCIADO ass
						INNER JOIN SPIF_TIPOCARTEIRA 	 tpc ON ass.ASS_INT_ID_TIPOCARTEIRA	= tpc.TIP_INT_ID_TIPOCARTEIRA
						INNER JOIN SPIF_CHANCELA 		 cha ON ass.ASS_INT_ID_TIPOCARTEIRA = cha.TIP_INT_ID_TIPOCARTEIRA
						LEFT JOIN SPIF_DIGITAL 			 dig ON ass.ASS_INT_ID_ASSOCIADO = dig.ASS_INT_ID_ASSOCIADO
						LEFT JOIN SPIF_TIPOGRAFICO 		 tpg ON ass.ASS_INT_ID_ASSOCIADO = tpg.ASS_INT_ID_ASSOCIADO
						WHERE ass.ASS_INT_ID_STATUS = 8 AND ass.ASS_STR_NR_CPF= :CPF AND ass.ASS_INT_ID_TIPOCARTEIRA = :IdTipoCarteira 
						ORDER BY ass.ASS_INT_ID_TIPOCARTEIRA ";
            }
        }

		public string Insert
		{
			get
			{
                return @"INSERT INTO SPIF_ASSOCIADO
						(
						ASS_INT_ID_ASSOCIADO 		
						,ASS_INT_ID_STATUS			
						,ASS_STR_NR_CPF				
						,ASS_INT_ID_TIPOCARTEIRA		
						,ASS_STR_DS_NOME1			
						,ASS_STR_DS_NOME2			
						,ASS_STR_DS_NOMEPARLAMENTAR	
						,ASS_STR_DS_FILIACAO1		
						,ASS_STR_DS_FILIACAO2		
						,ASS_STR_DS_NATURALIDADE		
						,ASS_DAT_DT_NASCIMENTO		
						,ASS_BLB_IMG_FOTO		
						,ASS_DAT_DT_EMISSAO			
						,ASS_DAT_DT_VALIDADE	
						,ASS_STR_DS_LOCALEXPEDICAO	
						,ASS_STR_DS_RG			
						,ASS_STR_DS_GRUPOSANGUINEO	
						,ASS_STR_DS_LEGISLATURA		
						,ASS_STR_DS_SEXO	
						,ASS_STR_DS_CARGO
						,ASS_BOL_FL_PORTEARMAS
						,ASS_STR_NR_MATRICULA
						,ASS_INT_ID_SITUACAO
						,ASS_STR_DS_QRCODE
						)VALUES(
						:Id,
						 8,
						:CPF,
						:IdTipoCarteira,
						:Nome1,
						:Nome2,
						:NomeParlamentar,
						:Filiacao1,
						:Filiacao2,
						:Naturalidade,
						:DataNascimento,
						:ImgFoto,
						:DataEmissao,
						:DataValidade,
						:LocalExpedicao,
						:RG,
						:GrupoSanguineo,
						:CodigoLegislatura,
						:Sexo,
						:Cargo,
						:isPorteArmas,
						:Matricula,
						:isAposentado,
						:QRCODE
						)";
            }
		}

        public string InsertPriorizado
        {
            get
            {
                return @"INSERT INTO SPIF_ASSOCIADO
						(
						ASS_INT_ID_ASSOCIADO 		
						,ASS_INT_ID_STATUS			
						,ASS_STR_NR_CPF				
						,ASS_INT_ID_TIPOCARTEIRA		
						,ASS_STR_DS_NOME1			
						,ASS_STR_DS_NOME2			
						,ASS_STR_DS_NOMEPARLAMENTAR	
						,ASS_STR_DS_FILIACAO1		
						,ASS_STR_DS_FILIACAO2		
						,ASS_STR_DS_NATURALIDADE		
						,ASS_DAT_DT_NASCIMENTO		
						,ASS_BLB_IMG_FOTO		
						,ASS_DAT_DT_EMISSAO			
						,ASS_DAT_DT_VALIDADE	
						,ASS_STR_DS_LOCALEXPEDICAO	
						,ASS_STR_DS_RG			
						,ASS_STR_DS_GRUPOSANGUINEO	
						,ASS_STR_DS_LEGISLATURA		
						,ASS_STR_DS_SEXO	
						,ASS_STR_DS_CARGO
						,ASS_BOL_FL_PORTEARMAS
						,ASS_STR_NR_MATRICULA
						,ASS_INT_ID_SITUACAO
						,ASS_STR_DS_QRCODE
						)VALUES(
						:Id,
						 8,
						:CPF,
						:IdTipoCarteira,
						:Nome1,
						:Nome2,
						:NomeParlamentar,
						:Filiacao1,
						:Filiacao2,
						:Naturalidade,
						:DataNascimento,
						:ImgFoto,
						:DataEmissao,
						:DataValidade,
						:LocalExpedicao,
						:RG,
						:GrupoSanguineo,
						:CodigoLegislatura,
						:Sexo,
						:Cargo,
						:isPorteArmas,
						:Matricula,
						:isAposentado,
						:QRCODE
						)";
            }
        }

        public string InsertAssinatura
        {
            get
            {
                return @"INSERT INTO SPIF_ASSINATURA
						(
						ASS_INT_ID_ASSOCIADO 		
						,ASI_BLB_IMG_ASSINATURA			
						,ASI_INT_ID_ANOMALIA
						)VALUES(
						:IdAssociado,
						:Assinatura,
						0
						)";
            }
        }

        public string InsertDigital
        {
            get
            {
                return @"INSERT INTO SPIF_DIGITAL
						(ASS_INT_ID_ASSOCIADO, DIG_INT_ID_DEDO, DIG_BLB_IMG_DIGITAL, DIG_BLB_IMG_TEMPLATE, DIG_INT_ID_QUALIDADE, DIG_INT_ID_ANOMALIA, DIG_BOL_FL_DIGITALIMPRESSAO, DIG_STR_DS_FORMATO)
						VALUES(:IdAssociado, 1,:Digital, :DigitalTmp, 1, 0, 1,NULL)";
            }
        }
    }
}
