﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carga.Generica.Infra.Query
{
    public class COFENAssociadoQuery
    {
        public string Get
        {
            get
            {
				return @"SELECT 
							 A.TIP_INT_ID_TIPOCARTEIRA AS tipo
							,TC.TIP_STR_DS_TIPOCARTEIRA AS descricao_tipo_carteira 
							,A.ASS_STR_DS_NOME1 AS nome_civil_portador1 
							,A.ASS_STR_DS_NOME2 AS nome_civil_portador2 
							,A.ASS_STR_DS_NOMESOCIAL1 AS nome_social_portador1 
							,A.ASS_STR_DS_NOMESOCIAL2 AS nome_social_portador2 
							,A.ASS_STR_DS_SEXO AS sexo
							,A.ASS_DAT_DT_NASCIMENTO AS data_nascimento 
							,A.ASS_STR_NR_CPF AS cpf_numero 
							,A.ASS_STR_DS_RG AS rg_numero 
							,A.ASS_STR_DS_RGORGAO AS rg_orgao_emissor 
							,A.TIP_INT_ID_TIPOCARTEIRA AS tipo_credencial
							,A.ASS_DAT_DT_VALIDADE AS validade 
							,A.ASS_STR_DS_FILIACAOMAE1 AS filiacao_mae 
							,A.ASS_STR_DS_FILIACAOMAE2 AS filiacao_mae2 
							,A.ASS_STR_DS_FILIACAOPAI1 AS filiacao_pai 
							,A.ASS_STR_DS_FILIACAOPAI2 AS filiacao_pai2
							,A.ASS_STR_DS_NATURALIDADE1 AS naturalidade
							,A.ASS_STR_DS_NATURALIDADE2 AS naturalidade_UF
							,A.ASS_STR_DS_NATURALIDADE3 AS nacionalidade
							,TO_CHAR(A.ASS_DAT_DT_EXPEDICAO,'DD') AS emissao_dia
							,TO_CHAR(A.ASS_DAT_DT_EXPEDICAO,'MM') AS emissao_mes 
							,TO_CHAR(A.ASS_DAT_DT_EXPEDICAO,'YYYY') AS emissao_ano
							,A.ASS_BLB_IMG_FOTO AS foto_portador
							,A.ASS_BLB_IMG_ASSINATURA AS ass_portador
							,CH.CHC_BLB_IMG_ASSINATURA AS ass_chancela
							,A.ASS_STR_NR_REGISTRO AS inscricao
							,A.ASS_STR_DS_ESPECIALIDADE1 AS especialidade1 
							,A.ASS_STR_DS_ESPECIALIDADE2 AS especialidade2 
							,CO.COR_STR_DS_UF AS uf
							,D.DIG_BLB_IMG_DIGITAL AS digital_portador 
							,A.ASS_STR_DS_CONTEUDOQRCODECRYPT AS conteudoQRCodeCrypt 
							,TU.TIP_INT_NR_TIPOGRAFICO AS nrTipografico 
							,TU.TPG_INT_NR_DV AS nrDVTipografico
							FROM SPIF_ASSOCIADO A
							INNER JOIN SPIF_DIGITAIS D ON D.ASS_INT_ID_ASSOCIADO = A.ASS_INT_ID_ASSOCIADO
							INNER JOIN SPIF_TIPOGRAFICOUTILIZADO TU ON TU.ASS_INT_ID_ASSOCIADO = A.ASS_INT_ID_ASSOCIADO
							INNER JOIN SPIF_CHANCELA CH ON CH.CHC_INT_ID_CHANCELA = A.CHC_INT_ID_CHANCELA
							INNER JOIN SPIF_COREN CO ON CO.COR_INT_ID_COREN = CH.COR_INT_ID_COREN
							INNER JOIN SPIF_TIPOCARTEIRA TC ON TC.TIP_INT_ID_TIPOCARTEIRA = A.TIP_INT_ID_TIPOCARTEIRA
							INNER JOIN SPIF_PRIORIZACAO P ON P.ASS_INT_ID_ASSOCIADO = A.ASS_INT_ID_ASSOCIADO 
							WHERE A.ASS_INT_ID_STATUS = 5 And NVL(A.ASS_BOL_FL_CERTGERADO, 0) = 0 And D.DIG_INT_ID_DIGITALIMPRESSAO = 1  And TU.TIP_INT_CD_CODIGOUTILIZACAO = 2 And A.ASS_DAT_DT_VALIDADE Is Not NULL
							And A.TIP_INT_ID_TIPOCARTEIRA NOT IN (10, 9, 12, 11)
							And A.ASS_DAT_DT_EXPEDICAO >= to_date('01/01/2017', 'dd/MM/yyyy') And a.ASS_STR_DS_CONTEUDOQRCODECRYPT Is NULL 
							AND a.ASS_INT_ID_CARTEIRACMB IS NULL
							And A.ASS_DAT_DT_VALIDADE > SYSDATE
							and A.ASS_STR_NR_CPF = :CPF AND A.TIP_INT_ID_TIPOCARTEIRA = :IdTipoCarteira AND ASS_STR_CD_REGIONALCOREN = :UF
							ORDER BY P.HORARIO
							 ";
            }
        }

		public string InsertAssociado
		{
			get
			{
				return @"INSERT INTO SPIF_ASSOCIADO
				(ASS_INT_ID_ASSOCIADO, ASS_STR_NR_CPF, 	ASS_STR_NR_REGISTRO, ASS_INT_NR_TIPOINSCRICAO, TIP_INT_ID_TIPOCARTEIRA, 
				ASS_STR_DS_NOME1, ASS_STR_DS_NOME2, ASS_STR_DS_SEXO, ASS_STR_DS_NATURALIDADE1, 	ASS_STR_DS_NATURALIDADE2, 
				ASS_STR_DS_NATURALIDADE3, ASS_DAT_DT_NASCIMENTO, ASS_STR_DS_RG, ASS_STR_DS_RGORGAO, ASS_DAT_DT_RGDATAEMISSAO, 
				ASS_DAT_DT_CONCLUSAOCURSO, ASS_STR_DS_OBS1, ASS_STR_DS_OBS2, ASS_STR_DS_FILIACAOMAE1, ASS_STR_DS_FILIACAOPAI1, 
				ASS_STR_DS_TIPOSANGUINEO, ASS_STR_DS_ESPECIALIDADE1, ASS_STR_DS_ESPECIALIDADE2, ASS_BLB_IMG_FOTO, ASS_BLB_IMG_ASSINATURA, 
				ASS_BLB_IMG_CARTEIRACMB, ASS_INT_ID_STATUS, ASS_INT_CD_TRANSWS, ASS_INT_ID_TIPOCAPTURA, ASS_BOL_FL_SEGUNDAVIA, 
				ASS_STR_DS_OBSBIOMETRIA, ASS_STR_DS_ANOFORMACAO, ASS_DAT_DT_VALIDADE, FAT_INT_ID_FATURAMENTO, ASS_STR_CD_REGIONALCOREN, 
				ASS_INT_ID_TIPOANOMALIADIGITAL, ASS_INT_NR_NATUREZA, MOT_INT_ID_MOTCANCASSOCIADO, ASS_DAT_DT_INICIOMANDATO, ASS_DAT_DT_FINALMANDATO, 
				ASS_STR_DS_NUMEROCONSELHEIRO, ASS_STR_DS_FORMACAO, ASS_STR_DS_NUMEROPRONTUARIO, ASS_STR_DS_REFPRONTUARIO, ASS_INT_ID_NATUREZACMB,
				ASS_INT_ID_TIPOINSCRICAOCMB, ASS_INT_CD_ORIGEMTBLCMB, ASS_DAT_DT_EXPEDICAO, ASS_INT_CD_TIPOORIGEM, ASS_INT_ID_TIPOANOMALIAASS, 
				ASS_STR_DS_FILIACAOMAE2, ASS_STR_DS_FILIACAOPAI2, CHC_INT_ID_CHANCELA, ASS_INT_CD_CHANCELA, ASS_INT_ID_TPCARTEIRACMB, 
				ASS_INT_ID_CARTEIRACMB, ASS_STR_DS_NATURALIDADE3CMB, EIP_INT_ID_ESTACAOIMPRESSAO, ASS_STR_DS_OBSBD, ASS_STR_ID_CORENDESTINO, 
				ASS_DAT_DT_VALIDADEBKP, ASS_DAT_DTEXPEDICAOCMB, ASS_INT_BLN_RENOVACAO, ASS_STR_DS_NOMESOCIAL1, ASS_STR_DS_NOMESOCIAL2, 
				ASS_BOL_FL_PRORROGACAO, ASS_BLB_IMG_DOCIDFRENTE, ASS_BLB_IMG_DOCIDVERSO, ASS_BLB_IMG_COMPRESIDENCIA, ASS_STR_DS_EMAIL, 
				ASS_STR_DS_TELEFONE, ASS_INT_ID_CIDADAO, ASS_BOL_FL_CERTGERADO, ASS_STR_DS_CERTIFICATEURL, ASS_STR_DS_CONTEUDOQRCODECRYPT, 
				ASS_STR_DS_CERTIFICATETCKT, ASS_BOL_FL_DIGITALAUSENTE, ASS_BOL_FL_CHANCELAAUSENTE, ASS_STR_DS_UUID, ID_POSTO_RETIRADA)
				VALUES(:Id, :cpf_numero, :inscricao, 1, :tipo, 
				:nome_civil_portador1, :nome_civil_portador2, :sexo, :naturalidade, :naturalidade_UF, 
				:nacionalidade, :data_nascimento, :rg_numero, :rg_orgao_emissor, SYSDATE, 
				NULL, NULL, NULL,:filiacao_mae, :filiacao_pai, 
				NULL, :especialidade1, :especialidade2, :foto_portador, :ass_portador, 
				NULL, 5, 3, 1, 0, 
				NULL, NULL, ADD_MONTHS(SYSDATE, 100), NULL, :uf, 
				NULL, 1, NULL, NULL, NULL, 
				NULL, NULL, NULL, NULL, NULL, 
				NULL, NULL, SYSDATE, NULL, NULL, 
				:filiacao_mae2, :filiacao_pai2, 68, NULL, NULL, 
				NULL, NULL, NULL, NULL, NULL, 
				NULL, NULL, NULL, :nome_social_portador1, :nome_social_portador2,
				NULL, NULL, NULL, NULL, NULL, 
				NULL, NULL, NULL, NULL, NULL,  
				NULL, NULL, NULL, NULL, NULL)
				";
			}
		}

        public string UpdateQrCodeAssociado
        {
            get
            {
                return @"UPDATE SPIF_ASSOCIADO SET ASS_STR_DS_CONTEUDOQRCODECRYPT = :QrCode where ASS_INT_ID_ASSOCIADO = :IdAssociado";
            }
        }

        public string InsertDigital
        {
            get
            {
                return @"INSERT INTO SPIF_DIGITAIS
							(DIG_INT_ID_DIGITAIS, ASS_INT_ID_ASSOCIADO, ASS_STR_NR_CPF, DIG_INT_ID_DIGITAL, DIG_INT_CD_ANOMALIA, DIG_STR_DS_FORMATO, DIG_INT_ID_DIGITALIMPRESSAO, DIG_BLB_IMG_DIGITAL, DIG_BLB_TMP_DIGITAL)
							VALUES(SEQIDDIGITAL.NEXTVAL, :IdAssociado, :CPF, 0, 0, 'BMP', 1, :Digital, NULL)";
            }
        }

        public string InsertPriorizacao
        {
            get
            {
                return @"INSERT INTO SPIF_PRIORIZACAO
				(PRI_INT_ID_PRIORIZACAO, ASS_INT_ID_ASSOCIADO, PRI_STR_DS_UUID, PRI_DAT_DT_DATAOPERACAO)
				VALUES((SELECT NVL(MAX(PRI_INT_ID_PRIORIZACAO) + 1,1) FROM SPIF_PRIORIZACAO),:IdAssociado, :UUID, SYSDATE)";
            }
        }

        public string InsertLogAssociado
        {
            get
            {
                return @"INSERT INTO SPIF_LOGASSOCIADO
				(LGI_INT_ID_LOGASSOCIADO, LGI_STR_CD_REGIONAL, LGI_STR_DS_MAQUINA, LGI_STR_CD_LOCAL, LGI_STR_TP_OPERACAO, LGI_STR_DS_STATUSOPERACAO, LGI_DAT_DT_DATAHORA, USU_STR_DS_USUARIO, ASS_INT_ID_ASSOCIADO, ASS_STR_NR_CPF, LGI_STR_CD_TIPOORIGEM)
				VALUES(SEQIDLOGASSOCIADO.NEXTVAL, :Regional, 'DEL45ZJ0Y1', NULL, 'CAPTURA', '2 - CAPTURADO', :DataCaptura, 'ERIKA.MORIGUTI', :IdAssociado, :CPF, NULL)";
            }
        }

        public string InsertTipoGrafico
        {
            get
            {
                return @"INSERT INTO SPIF_TIPOGRAFICOS
						(TPG_INT_ID_TIPOGRAFICO, TPG_INT_NR_TIPOGRAFICO, TPG_STR_CD_LOCALESTOQUE, TIP_INT_ID_TIPOCARTEIRA, TPG_INT_CD_CODIGOUTILIZACAO, PED_INT_ID_PEDIDO, PER_INT_ID_PERDA, TIP_INT_ID_TPRUTILIZADO, COR_INT_ID_COREN, TPG_INT_NR_DV, TPG_CHR_DS_FIXO)
						VALUES(:IdTipoGrafico, :NrTipoGrafico, 'N', 1, 2, 1458, NULL, NULL, :IdCoren, 6, 'V')";
            }
        }

        public string InsertTipoGraficoUtilizado
        {
            get
            {
                return @"INSERT INTO SPIF_TIPOGRAFICOUTILIZADO
				(TIP_INT_ID_TPRUTILIZADO, ASS_INT_ID_ASSOCIADO, ASS_STR_NR_CPF, TIP_INT_NR_TIPOGRAFICO, TIP_INT_CD_CODIGOUTILIZACAO, TIP_DAT_DT_OPERACAO, TIP_BOL_FL_PROCESSADO, TIP_STR_DS_MOTIVO, COR_INT_ID_COREN, TPG_INT_NR_DV, TPG_CHR_DS_FIXO, FAT_INT_ID_FATURAMENTO)
				VALUES(:IdTipograficoUtilizado, :IdAssociado, :CPF, :NrTipoGrafico, 2, SYSDATE, 1, '', :IdCoren, 6, 'V', NULL)";
            }
        }

        public string Insert2V
        {
            get
            {
                return @"INSERT INTO SPIF_ASSOCIADO
				(ASS_INT_ID_ASSOCIADO, ASS_STR_NR_CPF, 	ASS_STR_NR_REGISTRO, ASS_INT_NR_TIPOINSCRICAO, TIP_INT_ID_TIPOCARTEIRA, 
				ASS_STR_DS_NOME1, ASS_STR_DS_NOME2, ASS_STR_DS_SEXO, ASS_STR_DS_NATURALIDADE1, 	ASS_STR_DS_NATURALIDADE2, 
				ASS_STR_DS_NATURALIDADE3, ASS_DAT_DT_NASCIMENTO, ASS_STR_DS_RG, ASS_STR_DS_RGORGAO, ASS_DAT_DT_RGDATAEMISSAO, 
				ASS_DAT_DT_CONCLUSAOCURSO, ASS_STR_DS_OBS1, ASS_STR_DS_OBS2, ASS_STR_DS_FILIACAOMAE1, ASS_STR_DS_FILIACAOPAI1, 
				ASS_STR_DS_TIPOSANGUINEO, ASS_STR_DS_ESPECIALIDADE1, ASS_STR_DS_ESPECIALIDADE2, ASS_BLB_IMG_FOTO, ASS_BLB_IMG_ASSINATURA, 
				ASS_BLB_IMG_CARTEIRACMB, ASS_INT_ID_STATUS, ASS_INT_CD_TRANSWS, ASS_INT_ID_TIPOCAPTURA, ASS_BOL_FL_SEGUNDAVIA, 
				ASS_STR_DS_OBSBIOMETRIA, ASS_STR_DS_ANOFORMACAO, ASS_DAT_DT_VALIDADE, FAT_INT_ID_FATURAMENTO, ASS_STR_CD_REGIONALCOREN, 
				ASS_INT_ID_TIPOANOMALIADIGITAL, ASS_INT_NR_NATUREZA, MOT_INT_ID_MOTCANCASSOCIADO, ASS_DAT_DT_INICIOMANDATO, ASS_DAT_DT_FINALMANDATO, 
				ASS_STR_DS_NUMEROCONSELHEIRO, ASS_STR_DS_FORMACAO, ASS_STR_DS_NUMEROPRONTUARIO, ASS_STR_DS_REFPRONTUARIO, ASS_INT_ID_NATUREZACMB,
				ASS_INT_ID_TIPOINSCRICAOCMB, ASS_INT_CD_ORIGEMTBLCMB, ASS_DAT_DT_EXPEDICAO, ASS_INT_CD_TIPOORIGEM, ASS_INT_ID_TIPOANOMALIAASS, 
				ASS_STR_DS_FILIACAOMAE2, ASS_STR_DS_FILIACAOPAI2, CHC_INT_ID_CHANCELA, ASS_INT_CD_CHANCELA, ASS_INT_ID_TPCARTEIRACMB, 
				ASS_INT_ID_CARTEIRACMB, ASS_STR_DS_NATURALIDADE3CMB, EIP_INT_ID_ESTACAOIMPRESSAO, ASS_STR_DS_OBSBD, ASS_STR_ID_CORENDESTINO, 
				ASS_DAT_DT_VALIDADEBKP, ASS_DAT_DTEXPEDICAOCMB, ASS_INT_BLN_RENOVACAO, ASS_STR_DS_NOMESOCIAL1, ASS_STR_DS_NOMESOCIAL2, 
				ASS_BOL_FL_PRORROGACAO, ASS_BLB_IMG_DOCIDFRENTE, ASS_BLB_IMG_DOCIDVERSO, ASS_BLB_IMG_COMPRESIDENCIA, ASS_STR_DS_EMAIL, 
				ASS_STR_DS_TELEFONE, ASS_INT_ID_CIDADAO, ASS_BOL_FL_CERTGERADO, ASS_STR_DS_CERTIFICATEURL, ASS_STR_DS_CONTEUDOQRCODECRYPT, 
				ASS_STR_DS_CERTIFICATETCKT, ASS_BOL_FL_DIGITALAUSENTE, ASS_BOL_FL_CHANCELAAUSENTE, ASS_STR_DS_UUID, ID_POSTO_RETIRADA)
				VALUES(:Id, :cpf_numero, :inscricao, 1, :tipo, 
				:nome_civil_portador1, :nome_civil_portador2, :sexo, :naturalidade, :naturalidade_UF, 
				:nacionalidade, :data_nascimento, :rg_numero, :rg_orgao_emissor, SYSDATE, 
				NULL, NULL, NULL,:filiacao_mae, :filiacao_pai, 
				NULL, :especialidade1, :especialidade2, :foto_portador, :ass_portador, 
				NULL, 5, 3, 1, 1, 
				NULL, NULL, :data_validade, NULL, :uf, 
				NULL, 1, NULL, NULL, NULL, 
				NULL, NULL, NULL, NULL, NULL, 
				NULL, NULL, ADD_MONTHS(:data_validade, -100), NULL, NULL, 
				:filiacao_mae2, :filiacao_pai2, 68, NULL, NULL, 
				NULL, NULL, NULL, NULL, NULL, 
				NULL, NULL, NULL, :nome_social_portador1, :nome_social_portador2,
				NULL, NULL, NULL, NULL, NULL, 
				NULL, NULL, NULL, NULL, NULL,  
				NULL, NULL, NULL, NULL, NULL)
				";
            }
        }


    }
}
