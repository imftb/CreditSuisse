using Carga.Generica.Core.Enum;
using Carga.Generica.Core.Interface.Factory;
using Carga.Generica.Core.Interface.Repository;
using Carga.Generica.Core.Model;
using Carga.Generica.Infra.Query;
using Microsoft.Extensions.Configuration;

namespace Carga.Generica.Infra.Repository
{
    public class CAMDEPAssociadoRepository : ICAMDEPAssociadoRepository
    {

        private readonly IConfiguration config;
        private readonly IDataFactory dataFactory;
        private readonly CAMDEPAssociadoQuery query;

        public CAMDEPAssociadoRepository(IConfiguration config, IDataFactory dataFactory, CAMDEPAssociadoQuery query)
        {
            this.config = config;
            this.dataFactory = dataFactory;
            this.query = query;
        }

        public async Task<bool> PostAssociadoAsync(CAMDEPAssociado associado)
        {
            try
            {
                var Id = await dataFactory.GetFirst<int>("SELECT SEQIDASSOCIADO.NEXTVAL FROM DUAL", ProjetosEnum.CONNECTION.CAMDEP);

                var filtro = new CAMDEPAssociadoModel(associado);

                filtro.Id = Id;

                var result = await dataFactory.ExecuteCommand(query.Insert, filtro, ProjetosEnum.CONNECTION.CAMDEP);

                //Digital
                var digital = new CAMDEPDigitalModel();
                digital.IdAssociado = Id;
                digital.Digital = associado.ImgDigital;
                digital.DigitalTmp = associado.ImgDigital;


                result = await dataFactory.ExecuteCommand(query.InsertDigital, digital, ProjetosEnum.CONNECTION.CAMDEP);

                //Assinatura
                var assinatura = new CAMDEPAssinaturaModel();
                assinatura.IdAssociado = Id;
                assinatura.Assinatura = associado.ImgAssinatura;

                result = await dataFactory.ExecuteCommand(query.InsertAssinatura, assinatura, ProjetosEnum.CONNECTION.CAMDEP);
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool> PostAssociadoPriorizadoAsync(CAMDEPAssociadoPriorizado associado)
        {
            try
            {
                var Id = await dataFactory.GetFirst<int>("SELECT SEQIDASSOCIADO.NEXTVAL FROM DUAL", ProjetosEnum.CONNECTION.CAMDEP);

                var filtro = new CAMDEPAssociadoPriorizadoModel(associado);

                filtro.Id = Id;

                var result = await dataFactory.ExecuteCommand(query.InsertPriorizado, filtro, ProjetosEnum.CONNECTION.CAMDEP);

                //Digital
                var digital = new CAMDEPDigitalModel();
                digital.IdAssociado = Id;
                digital.Digital = associado.ImgDigital;

                result = await dataFactory.ExecuteCommand(query.InsertDigital, digital, ProjetosEnum.CONNECTION.CAMDEP);

                //Assinatura
                var assinatura = new CAMDEPAssinaturaModel();
                assinatura.IdAssociado = Id;
                assinatura.Assinatura = associado.ImgAssinatura;

                result = await dataFactory.ExecuteCommand(query.InsertAssinatura, assinatura, ProjetosEnum.CONNECTION.CAMDEP);
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<IEnumerable<CAMDEPAssociado>> GetAssociadoAsync(CAMDEPAssociadoConsulta associado)
        {
            return await dataFactory.Query<CAMDEPAssociado>(query.Get, associado, ProjetosEnum.CONNECTION.CAMDEP);
        }

        public async Task<bool> DeletePriorizadoAsync()
        {
            try
            {
                var result = await dataFactory.ExecuteCommand("DELETE SPIF_PRIORIZACAO", ProjetosEnum.CONNECTION.CAMDEP);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
