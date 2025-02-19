using System.Collections.Generic;

namespace Carga.Generica.Infra.Query
{
    public class CFMAssociadoQuery
    {
        public string Get
        {
            get
            {
				return @"SELECT	
							ass.ASS_STR_NR_SOLICITACAO as nrSolicitacao,
							ass.ASS_STR_NR_CPF as cpf_numero,
							ass.TIP_INT_ID_TIPOCARTEIRA as tipo_credencial,
							ass.ASS_STR_DS_NOME1 as nome1,
							ass.ASS_STR_DS_NOME2 as nome2,
							ass.ASS_STR_DS_NOMESOCIAL1 as nome_social1,
							ass.ASS_STR_DS_NOMESOCIAL2 as nome_social2,
							ass.ASS_STR_DS_NOMESOCIAL3 as nome_social3,
							ass.ASS_STR_DS_ORGAOEXPRGUF as rg_orgao_emissor,
							ass.ASS_STR_NR_CRM as crm_numero,
							ass.ASS_STR_DS_NOMEMAE1 as filiacao_mae,
							ass.ASS_STR_DS_NOMEMAE2 as filiacao_mae2,
							ass.ASS_STR_DS_NomePai1 as filiacao_pai,
							ass.ASS_STR_DS_NomePai2 as filiacao_pai2,
							ass.ASS_STR_DS_NATURALIDADE as naturalidade,
							ass.ASS_STR_NR_RG as rg_numero,
							ass.ASS_DAT_DT_NASCIMENTO as data_nascimento,
							ass.ASS_DAT_DT_INSCRICAO as data_inscricao,
							ass.ASS_DAT_DT_ADMISSAO as data_admissao,
							ass.URE_INT_ID_UNIDADEREGIONAL as unidade_regional,
							ass.ASS_STR_DS_CARGO as cargo,
							ass.ASS_DAT_DT_VALIDADE as data_validade,
							ASSINATURA.ASS_BLB_IMG_ASSINATURA as ass_portador,
							D.DIG_BLB_IMG_DIGITAL as digital_portador,
							ass.ASS_BLB_IMG_FOTO as foto_portador,
							ass.ASS_STR_DS_TITELEITORSEC as tit_eleitor_secao, 
							ass.ASS_STR_DS_TITELEITORZONA as tit_eleitor_zona, 
							ass.ASS_STR_NR_TITELEITOR as tit_eleitor_numero,
							LI.ASS_STR_DS_UFCRM as crm_estado
						FROM 
							SPIF_ASSOCIADO ass
						INNER JOIN SPIF_TIPOCARTEIRA 	 tpc ON ass.TIP_INT_ID_TIPOCARTEIRA	= tpc.TIP_INT_ID_TIPOCARTEIRA
						INNER JOIN SPIF_LOGIMPORTASSOCIADO LI ON LI.ASS_INT_ID_ASSOCIADO = ass.ASS_INT_ID_ASSOCIADO
						INNER JOIN SPIF_DIGITAIS D ON D.ASS_INT_ID_ASSOCIADO = ass.ASS_INT_ID_ASSOCIADO AND D.ASS_STR_NR_CPF = ass.ASS_STR_NR_CPF 
						INNER JOIN SPIF_ASSINATURA ASSINATURA ON ASSINATURA.ASS_INT_ID_ASSOCIADO  = ass.ASS_INT_ID_ASSOCIADO AND ASSINATURA.ASS_STR_NR_CPF = ass.ASS_STR_NR_CPF 
						WHERE ass.ASS_INT_ID_STATUS = 4 AND ass.ASS_STR_NR_CPF = :CPF AND ass.TIP_INT_ID_TIPOCARTEIRA = :TipoCarteira 
						ORDER BY ass.TIP_INT_ID_TIPOCARTEIRA";
            }
        }

		public string InsertAssociado
		{
			get
			{
				return @"INSERT INTO SPIF_ASSOCIADO
					   (ASS_INT_ID_ASSOCIADO, 		
						ASS_INT_ID_STATUS,	
						ASS_STR_NR_SOLICITACAO,
						ASS_STR_NR_CPF,
						TIP_INT_ID_TIPOCARTEIRA,
						ASS_STR_DS_NOME1,
						ASS_STR_DS_NOME2,
						ASS_STR_DS_NOMESOCIAL1,
						ASS_STR_DS_NOMESOCIAL2,
						ASS_STR_DS_NOMESOCIAL3,
						ASS_STR_DS_ORGAOEXPRGUF,
						ASS_STR_NR_CRM,
						ASS_STR_DS_NOMEMAE1,
						ASS_STR_DS_NOMEMAE2,
						ASS_STR_DS_NomePai1,
						ASS_STR_DS_NomePai2,
						ASS_STR_DS_NATURALIDADE,
						ASS_STR_NR_RG,
						ASS_DAT_DT_NASCIMENTO,
						ASS_DAT_DT_INSCRICAO,
						ASS_DAT_DT_ADMISSAO,
						URE_INT_ID_UNIDADEREGIONAL,
						ASS_STR_DS_CARGO,						
						ASS_DAT_DT_VALIDADE,
						TPR_INT_ID_TIPOREQUISICAO,
						ASS_BLB_IMG_FOTO,
						ASS_STR_DS_TITELEITORSEC, 
						ASS_STR_DS_TITELEITORZONA, 
						ASS_STR_NR_TITELEITOR
						)VALUES(
						:Id,
						 4,
						:nrSolicitacao,
						:cpf_numero,
						:tipo_credencial,
						:nome1,
						:nome2,
						:nome_social1,
						:nome_social2,
						:nome_social3,
						:rg_orgao_emissor,
						:crm_numero,
						:filiacao_mae,
						:filiacao_mae2,
						:filiacao_pai,
						:filiacao_pai2,
						:naturalidade,
						:rg_numero,
						:data_nascimento,
						:data_inscricao,
						:data_admissao,
						:unidade_regional,
						:cargo,
						:data_validade,1,:foto_portador, :tit_eleitor_secao, :tit_eleitor_zona, :tit_eleitor_numero)"
                ;
            }
        }

        public string InsertLogImportAssociado
        {
            get
            {
                return @"INSERT INTO SPIF_LOGIMPORTASSOCIADO
					   (LIA_INT_ID_LOGIMPORTASSOCIADO,
						ASS_STR_DS_UFCRM,
						ASS_INT_ID_ASSOCIADO, 
						LIA_DAT_DT_IMPORTACAO,
						ASS_STR_NR_CPF,
						ASS_STR_DS_NOME1,
						ASS_STR_DS_NOME2,
						ASS_STR_DS_NOMESOCIAL1,
						ASS_STR_DS_NOMESOCIAL2,
						ASS_STR_DS_NOMESOCIAL3,
						ASS_STR_DS_ORGAOEXPRGUF,
						ASS_STR_NR_CRM,
						ASS_STR_DS_NOMEMAE1,
						ASS_STR_DS_NOMEMAE2,
						ASS_STR_DS_NomePai1,
						ASS_STR_DS_NomePai2,
						ASS_STR_DS_NATURALIDADE,
						ASS_STR_NR_RG,
						ASS_DAT_DT_NASCIMENTO,
						ASS_DAT_DT_INSCRICAO,
						ASS_DAT_DT_ADMISSAO,
						ASS_STR_DS_CARGO,
						ASS_DAT_DT_VALIDADE,LIA_INT_ID_CODVALIDACAO
						)VALUES(
						:IdLog,
						:crm_estado,
						:Id,
						SYSDATE,
						:cpf_numero,
						:nome1,
						:nome2,
						:nome_social1,
						:nome_social2,
						:nome_social3,
						:rg_orgao_emissor,
						:crm_numero,
						:filiacao_mae,
						:filiacao_mae2,
						:filiacao_pai,
						:filiacao_pai2,
						:naturalidade,
						:rg_numero,
						:data_nascimento,
						:data_inscricao,
						:data_admissao,
						:cargo,
						:data_validade,1)"
                ;
            }
        }

        public string InsertAssinatura
        {
            get
            {
                return @"INSERT INTO SPIF_ASSINATURA
						(ASS_INT_ID_ASSOCIADO, ASS_STR_NR_CPF, ASS_BLB_IMG_ASSINATURA, ASS_INT_CD_ANOMALIA)
						VALUES(:Id, :cpf_numero, :ass_portador, 0)";
                
            }
        }

        public string InsertDigital
        {
            get
            {
                return @"INSERT INTO SPIF_CFM.SPIF_DIGITAIS
						(DIG_INT_ID_DIGITAIS, ASS_INT_ID_ASSOCIADO, DIG_INT_CD_DEDO, DIG_BLB_IMG_DIGITAL, DIG_BLB_TMP_DIGITAL, DIG_INT_CD_ANOMALIA, ASS_STR_NR_CPF, DIG_BOL_FL_WSQVALIDO, DIG_BOL_FL_DIGITALIMPRESSAO, DIG_STR_DS_MOTIVOANOMALIA)
						VALUES(:IdDigital, :Id, 1, :digital_portador, :digital_tmp, 0, :cpf_numero, 1, 0, '')"
                ;
            }
        }
    }
}
