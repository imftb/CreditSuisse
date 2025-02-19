using Carga.Generica.Core.Enum;
using Carga.Generica.Core.Interface.Factory;
using Carga.Generica.Core.Interface.Repository;
using Carga.Generica.Core.Model;
using Carga.Generica.Infra.Query;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Carga.Generica.Infra.Repository
{
    public class COFENAssociadoRepository : ICOFENAssociadoRepository
    {

        private readonly IConfiguration config;
        private readonly IDataFactory dataFactory;
        private readonly COFENAssociadoQuery query;

        public COFENAssociadoRepository(IConfiguration config, IDataFactory dataFactory, COFENAssociadoQuery query)
        {
            this.config = config;
            this.dataFactory = dataFactory;
            this.query = query;
        }

        public async Task<bool> PostAssociadoAsync(COFENAssociado associado)
        {
            try
            {
                var Id = await dataFactory.GetFirst<int>("SELECT SEQIDASSOCIADO.NEXTVAL FROM DUAL", ProjetosEnum.CONNECTION.COFEN);

                var parametros = new COFENAssociadoModel(associado);

                parametros.Id = Id;

                var result = await dataFactory.ExecuteCommand(query.InsertAssociado, parametros, ProjetosEnum.CONNECTION.COFEN);

                //Digital
                var digital = new COFENDigitalModel();
                digital.IdAssociado = Id;
                digital.CPF = associado.cpf_numero;
                digital.Digital = associado.digital_portador;

                result = await dataFactory.ExecuteCommand(query.InsertDigital, digital, ProjetosEnum.CONNECTION.COFEN);

                //Priorização
                var priorizacao = new COFENPriorizacaoModel();
                priorizacao.IdAssociado = Id;
                priorizacao.CPF = associado.cpf_numero;
                Guid g = Guid.NewGuid();
                priorizacao.UUID = g.ToString();

                result = await dataFactory.ExecuteCommand(query.InsertPriorizacao, priorizacao, ProjetosEnum.CONNECTION.COFEN);

                //Tipo Gráfico 
                var IdTipoGrafico = await dataFactory.GetFirst<int>("SELECT SEQIDTIPOGRAFICO.NEXTVAL FROM DUAL", ProjetosEnum.CONNECTION.COFEN);

                var NrTipoGrafico = await dataFactory.GetFirst<int>("SELECT MAX(TPG_INT_NR_TIPOGRAFICO) + 1 FROM SPIF_TIPOGRAFICOS ", ProjetosEnum.CONNECTION.COFEN);

                var IdCoren = await dataFactory.GetFirst<int>(String.Format("SELECT COR_INT_ID_COREN FROM SPIF_COREN where COR_STR_DS_UF = '{0}'", associado.uf.ToUpper()), ProjetosEnum.CONNECTION.COFEN);

                var tipoGrafico = new COFENTipoGraficoModel();
                tipoGrafico.IdCoren = IdCoren;
                tipoGrafico.IdTipoGrafico = IdTipoGrafico;
                tipoGrafico.NrTipoGrafico = NrTipoGrafico;

                result = await dataFactory.ExecuteCommand(query.InsertTipoGrafico, tipoGrafico, ProjetosEnum.CONNECTION.COFEN);               

                //Tipo Gráfico Utilizado
                var tipoGraficoUtilizado = new COFENTipoGraficoUtilizadoModel();
                tipoGraficoUtilizado.IdAssociado = Id;
                tipoGraficoUtilizado.CPF = associado.cpf_numero;
                tipoGraficoUtilizado.IdCoren = IdCoren;
                tipoGraficoUtilizado.NrTipoGrafico = NrTipoGrafico;

                result = await dataFactory.ExecuteCommand(query.InsertTipoGraficoUtilizado, tipoGraficoUtilizado, ProjetosEnum.CONNECTION.COFEN);               
               

                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<COFENAssociadoPriorizacaoModel> PostAssociadoPriorizadoAsync(COFENAssociado associado)
        {
            try
            {
                var Id = await dataFactory.GetFirst<int>("SELECT SEQIDASSOCIADO.NEXTVAL FROM DUAL", ProjetosEnum.CONNECTION.COFEN);

                var parametros = new COFENAssociadoModel(associado);

                parametros.Id = Id;

                var result = await dataFactory.ExecuteCommand(query.InsertAssociado, parametros, ProjetosEnum.CONNECTION.COFEN);

                //Digital
                var digital = new COFENDigitalModel();
                digital.IdAssociado = Id;
                digital.CPF = associado.cpf_numero;
                digital.Digital = associado.digital_portador;

                result = await dataFactory.ExecuteCommand(query.InsertDigital, digital, ProjetosEnum.CONNECTION.COFEN);

                ////Priorização
                //var priorizacao = new COFENPriorizacaoModel();
                //priorizacao.IdAssociado = Id;
                //priorizacao.CPF = associado.cpf_numero;
                //Guid g = Guid.NewGuid();
                //priorizacao.UUID = g.ToString();

                //result = await dataFactory.ExecuteCommand(query.InsertPriorizacao, priorizacao, ProjetosEnum.CONNECTION.COFEN);

                //Tipo Gráfico 
                var IdTipoGrafico = await dataFactory.GetFirst<int>("SELECT SEQIDTIPOGRAFICO.NEXTVAL FROM DUAL", ProjetosEnum.CONNECTION.COFEN);

                var NrTipoGrafico = await dataFactory.GetFirst<int>("SELECT MAX(TPG_INT_NR_TIPOGRAFICO) + 1 FROM SPIF_TIPOGRAFICOS ", ProjetosEnum.CONNECTION.COFEN);

                var IdCoren = await dataFactory.GetFirst<int>(String.Format("SELECT COR_INT_ID_COREN FROM SPIF_COREN where COR_STR_DS_UF = '{0}'", associado.uf.ToUpper()), ProjetosEnum.CONNECTION.COFEN);

                var tipoGrafico = new COFENTipoGraficoModel();
                tipoGrafico.IdCoren = IdCoren;
                tipoGrafico.IdTipoGrafico = IdTipoGrafico;
                tipoGrafico.NrTipoGrafico = NrTipoGrafico;

                result = await dataFactory.ExecuteCommand(query.InsertTipoGrafico, tipoGrafico, ProjetosEnum.CONNECTION.COFEN);

                //Tipo Gráfico Utilizado
                var tipoGraficoUtilizado = new COFENTipoGraficoUtilizadoModel();
                tipoGraficoUtilizado.IdAssociado = Id;
                tipoGraficoUtilizado.CPF = associado.cpf_numero;
                tipoGraficoUtilizado.IdCoren = IdCoren;
                tipoGraficoUtilizado.NrTipoGrafico = NrTipoGrafico;

                result = await dataFactory.ExecuteCommand(query.InsertTipoGraficoUtilizado, tipoGraficoUtilizado, ProjetosEnum.CONNECTION.COFEN);

                var retorno = new COFENAssociadoPriorizacaoModel();
                retorno.tipografico = NrTipoGrafico.ToString();
                retorno.QrCode = CriptQrCode(String.Format("www.valid{0}/{1}", NrTipoGrafico,parametros.cpf_numero));
                retorno.numeroRegistro = parametros.inscricao;

                result = await dataFactory.ExecuteCommand(query.UpdateQrCodeAssociado, new { QrCode = retorno.QrCode, IdAssociado = Id }, ProjetosEnum.CONNECTION.COFEN);


                return retorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static string CriptQrCode(string qrCode)
        {
            //if (string.IsNullOrEmpty(qrCode))
            //    throw new BusinessException("Qr code inválido");

            //TripleDESCryptoServiceProvider TripleDes = new();
            //TripleDes.Key = new byte[] { 0x6E, 0x41, 0x3C, 0x74, 0x77, 0x72, 0x61, 0x16, 0x53, 0x49, 0x3, 0x66, 0x76, 0x8, 0x36, 0x4C };
            //TripleDes.Mode = CipherMode.ECB;


            //Criar TripleDESCryptoServiceProvider
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            var Chave = new byte[] { 0x6E, 0x41, 0x3C, 0x74, 0x77, 0x72, 0x61, 0x16, 0x53, 0x49, 0x3, 0x66, 0x76, 0x8, 0x36, 0x4C };
            var IV = CipherMode.ECB;


            byte[] plainByte = Encoding.UTF8.GetBytes(qrCode);
            byte[] keyByte = new byte[] { 0x6E, 0x41, 0x3C, 0x74, 0x77, 0x72, 0x61, 0x16, 0x53, 0x49, 0x3, 0x66, 0x76, 0x8, 0x36, 0x4C };
            // Seta a chave privada
            tdes.Key = keyByte;
            tdes.Mode = IV;
            // Interface de criptografia / Cria objeto de criptografia
            ICryptoTransform cryptoTransform = tdes.CreateEncryptor();
            MemoryStream _memoryStream = new MemoryStream();
            CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Write);
            // Grava os dados criptografados no MemoryStream
            _cryptoStream.Write(plainByte, 0, plainByte.Length);
            _cryptoStream.FlushFinalBlock();
            // Busca o tamanho dos bytes encriptados
            byte[] cryptoByte = _memoryStream.ToArray();
            // Converte para a base 64 string para uso posterior em um xml
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));

            //byte[] encryptedBytes = Convert.FromBase64String(qrCode);

            //MemoryStream ms = new();

            //CryptoStream decStream = new(ms, TripleDes.CreateEncryptor(), CryptoStreamMode.Read);
            //decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            //decStream.FlushFinalBlock();

            //return Encoding.UTF8.GetString(ms.ToArray());
        }

        public async Task<bool> Post2VAsync(COFENAssociadoRenovacao associado)
        {
            try
            {
                var Id = await dataFactory.GetFirst<int>("SELECT SEQIDASSOCIADO.NEXTVAL FROM DUAL", ProjetosEnum.CONNECTION.COFEN);

                var parametros = new COFENAssociadoRenovacaoModel(associado);

                parametros.Id = Id;

                var result = await dataFactory.ExecuteCommand(query.Insert2V, parametros, ProjetosEnum.CONNECTION.COFEN);

                //Digital
                var digital = new COFENDigitalModel();
                digital.IdAssociado = Id;
                digital.CPF = associado.cpf_numero;
                digital.Digital = associado.digital_portador;

                result = await dataFactory.ExecuteCommand(query.InsertDigital, digital, ProjetosEnum.CONNECTION.COFEN);

                //Priorização
                var priorizacao = new COFENPriorizacaoModel();
                priorizacao.IdAssociado = Id;
                priorizacao.CPF = associado.cpf_numero; 
                Guid g = Guid.NewGuid();
                priorizacao.UUID = g.ToString();

                result = await dataFactory.ExecuteCommand(query.InsertPriorizacao, priorizacao, ProjetosEnum.CONNECTION.COFEN);

                //Tipo Gráfico 
                var IdTipoGrafico = await dataFactory.GetFirst<int>("SELECT SEQIDTIPOGRAFICO.NEXTVAL FROM DUAL", ProjetosEnum.CONNECTION.COFEN);

                var NrTipoGrafico = await dataFactory.GetFirst<int>("SELECT MAX(TPG_INT_NR_TIPOGRAFICO) + 1 FROM SPIF_TIPOGRAFICOS ", ProjetosEnum.CONNECTION.COFEN);

                var IdCoren = await dataFactory.GetFirst<int>(String.Format("SELECT COR_INT_ID_COREN FROM SPIF_COREN where COR_STR_DS_UF = '{0}'", associado.uf.ToUpper()), ProjetosEnum.CONNECTION.COFEN);

                var tipoGrafico = new COFENTipoGraficoModel();
                tipoGrafico.IdCoren = IdCoren;
                tipoGrafico.IdTipoGrafico = IdTipoGrafico;
                tipoGrafico.NrTipoGrafico = NrTipoGrafico;

                result = await dataFactory.ExecuteCommand(query.InsertTipoGrafico, tipoGrafico, ProjetosEnum.CONNECTION.COFEN);

                //Tipo Gráfico Utilizado

                var IdTipoGraficoUtilizado = await dataFactory.GetFirst<int>("SELECT SEQIDTIPOGRAFICOUTILIZADO.NEXTVAL FROM DUAL", ProjetosEnum.CONNECTION.COFEN);

                var tipoGraficoUtilizado = new COFENTipoGraficoUtilizadoModel();
                tipoGraficoUtilizado.IdTipograficoUtilizado = IdTipoGraficoUtilizado;
                tipoGraficoUtilizado.IdAssociado = Id;
                tipoGraficoUtilizado.CPF = associado.cpf_numero;
                tipoGraficoUtilizado.IdCoren = IdCoren;
                tipoGraficoUtilizado.NrTipoGrafico = NrTipoGrafico;

                result = await dataFactory.ExecuteCommand(query.InsertTipoGraficoUtilizado, tipoGraficoUtilizado, ProjetosEnum.CONNECTION.COFEN);

                //LogAssociado
                var log = new COFENLogAssociadoModel();
                log.IdAssociado = Id;
                log.CPF = associado.cpf_numero;
                log.Regional = associado.uf;

                DateTime data = Convert.ToDateTime(associado.data_captura == null ? DateTime.Now : associado.data_captura);
                log.DataCaptura = data;

                result = await dataFactory.ExecuteCommand(query.InsertLogAssociado, log, ProjetosEnum.CONNECTION.COFEN);


                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<IEnumerable<COFENAssociado>> GetAssociadoAsync(COFENAssociadoConsulta associado)
        {
            try
            {
                var result = await dataFactory.Query<COFENAssociado>(query.Get, associado, ProjetosEnum.CONNECTION.COFEN);

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

    }
}
