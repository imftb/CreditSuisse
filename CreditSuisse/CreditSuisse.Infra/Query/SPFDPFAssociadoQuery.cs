using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carga.Generica.Infra.Query
{
    public class SPFDPFAssociadoQuery
    {
        public string GetAssociado
        {
            get
            {
				return @"SELECT 
						RNE_STR_URL_QRCODE as qrCode 
						,RNE_STR_DS_NOMELINHA1 as nome 
						,RNE_STR_DS_NOMELINHA2 as sobrenome 
						,RNE_DAT_DT_NASCIMENTO as dataNascimento 
						,RNE_STR_DS_MAE as nomeMae 
						,RNE_STR_DS_PAI as nomePai 
						,RNE_STR_DS_PAISNACIONALIDADE as paisNascionalidade
						,RNE_DAT_DT_VALIDADE as dataValidade
						,RNE_STR_NR_RNE as rne
						,RNE_STR_DS_CONDICOESESPECIAIS as condicaoEspecial
						,RNE_STR_DS_CPF as cpf
						,RNE_STR_DT_PRAZO_RESIDENCIA as prazoresidencia
						,RNE_DAT_DT_EXPEDICAO as dataEmissaoCarteira
						,RNE_STR_DS_ORGAODPF as orgao
						,RNE_STR_DS_OBSERVACAO_AMPARO as amparoLegal
						,RNE_STR_DS_PROTOCOLO as protocolo
						,RNE_STR_CD_OCRLINHA1 as mrz1
						,RNE_STR_CD_OCRLINHA2 as mrz2
						,RNE_STR_CD_OCRLINHA3 as mrz3
						,rne.TIP_INT_ID_TIPOCARTEIRA as idTipoCarteira
						,tc.TIP_STR_DS_TIPOCARTEIRA as tipoCarteira
						,RNE_DBL_NR_ESPELHO as espelho
						,RNE_STR_DS_UUID as uuid
						FROM SPF_RNE rne 
						INNER JOIN SPF_TIPOCARTEIRA tc ON tc.TIP_INT_ID_TIPOCARTEIRA = rne.TIP_INT_ID_TIPOCARTEIRA  
						INNER JOIN SPF_CAIXAS c ON C.CAX_LNG_NR_CAIXA = RNE.CAX_LNG_NR_CAIXA  And C.LOT_INT_NR_ANO = RNE.LOT_INT_NR_ANO AND C.LOT_INT_NR_SEQLOTE = RNE.LOT_INT_NR_SEQLOTE
						WHERE rne.RNE_STR_NR_RNE = :rne And rne.RNE_INT_CD_STATUS = 7 And rne.RNE_BOL_FL_CERT_GERADO = 0 ";
            }
        }

		public string GetImagens
		{
			get
			{
				return @"SELECT 
						im.RNE_DBL_ID_RNE as id, 
						im.IMG_STR_ID_TIPOIMAGEM as tipoImagem, 
						im.IMG_BLB_ID_IMAGEM as imagem, 
						im.IMG_INT_ID_TAMANHOIMAGEM as tamanho
						FROM SPF_IMAGENS im inner join spf_rne rne on im.RNE_DBL_ID_RNE = rne.RNE_DBL_ID_RNE
						WHERE rne.RNE_STR_NR_RNE = :rne ";
			}
		}

		public string InsertLote
		{
			get
			{
				return @"INSERT INTO SPF_LOTES
				(LOT_INT_NR_ANO, LOT_INT_NR_SEQLOTE, LOT_INT_NR_DIAJULIANO, LOT_DAT_DT_LEITURA, LOT_DAT_DT_GUIA, LOT_BOL_FL_PASSIVO, LOT_STR_DS_LOGIN, LOT_STR_DS_MAQUINA, LOT_DAT_DT_PRODUCAO, TIP_INT_ID_TIPOCARTEIRA, TIP_INT_ID_TIPOLOTE)
				VALUES(:AnoLote, :SeqLote, 83, SYSDATE, SYSDATE, 0, 'SPF', 'DELHHDSZ02', SYSDATE, NULL, 1)";

            }
		}

		public string InsertCaixa
		{
			get
			{
				return @"
					INSERT INTO SPF_CAIXAS
					(LOT_INT_NR_ANO, LOT_INT_NR_SEQLOTE, CAX_LNG_NR_CAIXA, CAX_INT_CD_STATUS, CAX_INT_NR_QTDDOCS, CAX_STR_DS_LOGIN, CAX_STR_DS_MAQUINA, CAX_STR_FL_BLOQUEADA)
					VALUES(:AnoLote, :SeqLote,:NrCaixa, 7, 1, 'WS', 'WS', NULL)";
			}
		}

		public string InsertAssociado
		{
			get
			{
				return @"INSERT INTO SPF_RNE ( 
								RNE_DBL_ID_RNE 
								,RNE_STR_URL_QRCODE   
								,RNE_STR_DS_NOMELINHA1   
								,RNE_STR_DS_NOMELINHA2   
								,RNE_STR_DS_NOMESOCIAL
								,RNE_STR_DS_CLASSIFICACAO
								,RNE_DAT_DT_NASCIMENTO   
								,RNE_STR_DS_MAE   
								,RNE_STR_DS_PAI   
								,RNE_STR_DS_PAISNACIONALIDADE  
								,RNE_DAT_DT_VALIDADE  
								,RNE_STR_NR_RNE  
								,RNE_STR_DS_CONDICOESESPECIAIS  
								,RNE_STR_DS_AMPAROLEGAL
								,RNE_STR_DS_CPF  
								,RNE_STR_DT_PRAZO_RESIDENCIA  
								,RNE_DAT_DT_EXPEDICAO  
								,RNE_STR_DS_ORGAODPF  
								,RNE_STR_DS_OBSERVACAO_AMPARO  
								,RNE_STR_DS_PROTOCOLO  
								,RNE_STR_CD_OCRLINHA1  
								,RNE_STR_CD_OCRLINHA2  
								,RNE_STR_CD_OCRLINHA3  
								,TIP_INT_ID_TIPOCARTEIRA 
								,RNE_DBL_NR_ESPELHO 
								,RNE_BOL_FL_CERT_GERADO
								,RNE_INT_CD_STATUS
								,LOT_INT_NR_ANO, LOT_INT_NR_SEQLOTE, CAX_LNG_NR_CAIXA,RNE_INT_NR_VEZESIMPRESSA,RNE_STR_DS_SEXO) 
					VALUES (
								:id,
								:qrCode,
								:nome,
								:sobrenome,
								:nomeSocial,
								:classificacao,
								:dataNascimento,
								:nomeMae,
								:nomePai,
								:paisNascionalidade,
								:DtValidade,
								:rne,
								:condicaoEspecial,
								:amparoLegal,
								:cpf,
								:prazoresidencia,
								:dataEmissaoCarteira,
								:orgao,
								:amparoLegal,
								:protocolo,
								:mrz1,
								:mrz2,
								:mrz3,
								:idTipoCarteira,
								:espelho,
								0,7,:AnoLote,:SeqLote,:NrCaixa,1,:Sexo)";
			}
		}

		public string InsertImagem
		{
			get
			{
				return @"INSERT INTO SPF_IMAGENS
						(RNE_DBL_ID_RNE, IMG_STR_ID_TIPOIMAGEM, IMG_BLB_ID_IMAGEM, IMG_INT_ID_TAMANHOIMAGEM, IMG_INT_ID_DEDO)
						VALUES(:id, :tipoImagem, :imagem, :tamanho, null)	";
			}
		}
	}
}
